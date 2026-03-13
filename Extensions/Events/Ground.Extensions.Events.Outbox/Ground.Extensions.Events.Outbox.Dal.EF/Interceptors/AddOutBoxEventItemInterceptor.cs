using Ground.Core.Domain.Entities;
using Ground.Extensions.Events.Abstractions;
using Ground.Extensions.Serializers.Abstractions;
using Ground.Extensions.UsersManagement.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ground.Extensions.Events.Outbox.Dal.EF.Interceptors
{
    /// <summary>
    /// Intercepts save operations to automatically add outbox event items for domain events raised by aggregate roots
    /// during changes in the DbContext.
    /// </summary>
    public class AddOutBoxEventItemInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AddOutbox(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            AddOutbox(eventData);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// Adds outbox event items to the context for each domain event found in changed aggregate roots within the
        /// specified DbContext event data.
        /// </summary>
        /// <remarks>This method inspects all aggregate root entities tracked by the context that have
        /// domain events, and creates corresponding outbox event items for each event. The outbox items include user
        /// information, event metadata, and tracing identifiers if available. This enables reliable event publishing in
        /// distributed systems by persisting events as part of the current transaction.</remarks>
        /// <param name="eventData">The event data containing the DbContext and change tracking information.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventData.Context"/> is null.</exception>
        private static void AddOutbox(DbContextEventData eventData)
        {
            if (eventData.Context == null)
            {
                throw new ArgumentNullException(nameof(eventData.Context), "DbContext cannot be null.");
            }

            List<dynamic> changedAggregates = eventData.Context.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.State != EntityState.Detached)
                .Select(c => c.Entity as dynamic)
                .Where(c => c.GetEvents() != null && c.GetEvents().Count > 0)
                .ToList();

            if (changedAggregates is null || !changedAggregates.Any())
            {
                return;
            }

            var userInfoService = eventData.Context.GetService<IUserInfoService>();
            var serializer = eventData.Context.GetService<IJsonSerializer>();
            string traceId = string.Empty;
            string spanId = string.Empty;

            if (Activity.Current != null)
            {
                traceId = Activity.Current.TraceId.ToHexString();
                spanId = Activity.Current.SpanId.ToHexString();
            }

            foreach (var aggregate in changedAggregates)
            {
                var events = aggregate.GetEvents();
                foreach (object @event in events)
                {
                    eventData.Context.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),
                        AccuredByUserId = userInfoService.UserIdOrDefault(),
                        AccuredOn = DateTime.Now,
                        AggregateId = aggregate.BusinessId.ToString(),
                        AggregateName = aggregate.GetType().Name,
                        AggregateTypeName = aggregate.GetType().FullName ?? aggregate.GetType().Name,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName ?? @event.GetType().Name,
                        EventPayload = serializer.Serialize(@event),
                        TraceId = traceId,
                        SpanId = spanId,
                        IsProcessed = false
                    });
                }
            }
        }

    }
}
