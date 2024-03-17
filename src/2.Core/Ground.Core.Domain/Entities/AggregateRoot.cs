using Ground.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Core.Domain.Entities
{
    /// <summary>
    /// AggregateRoot pattern
    /// https://martinfowler.com/bliki/DDD_Aggregate.html
    /// It can be used with both State-based or Event-Driven apps.
    /// </summary>
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : struct,
              IComparable,
              IComparable<TId>,
              IConvertible,
              IEquatable<TId>,
              IFormattable
    {
        /// <summary>
        /// Events list
        /// </summary>
        private readonly List<IDomainEvent> _events;
        protected AggregateRoot() => _events = new();

        /// <summary>
        /// Create an aggregate from events.
        /// </summary>
        /// <param name="events">if there is events list, use this parameter</param>
        public AggregateRoot(IEnumerable<IDomainEvent> events)
        {
            if (events == null || !events.Any()) return;
            foreach (var @event in events)
            {
                Mutate(@event);
            }
        }

        protected void Apply(IDomainEvent @event)
        {
            Mutate(@event);
            AddEvent(@event);
        }

        private void Mutate(IDomainEvent @event)
        {
            //using Reflection to call private methods
            var onMethod = this.GetType().GetMethod("On", BindingFlags.Instance | BindingFlags.NonPublic,new Type[] { @event.GetType()});// [@event.GetType()] C#12
            onMethod.Invoke(this, new[] { @event });
        }

        /// <summary>
        /// Add new event to the aggregate
        /// It should be protected or private.
        /// </summary>
        /// <param name="event"></param>
        protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

        /// <summary>
        /// Returns a readonly list of the aggregate events
        /// </summary>
        /// <returns>لیست Eventها</returns>
        public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();

        /// <summary>
        /// It clears aggregate's events
        /// </summary>
        public void ClearEvents() => _events.Clear();
    }



    public abstract class AggregateRoot : AggregateRoot<long>
    {

    }
}
