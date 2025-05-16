using Ground.Extensions.Events.Abstractions;
using Ground.Extensions.Events.Outbox.Dal.EF.Configs;
using Ground.Extensions.Events.Outbox.Dal.EF.Interceptors;
using Ground.Infra.Data.Sql.Commands;
using Microsoft.EntityFrameworkCore;

namespace Ground.Extensions.Events.Outbox.Dal.EF
{
    public abstract class BaseOutboxCommandDbContext : BaseCommandDbContext
    {
        #region Properties
        /// <summary>
        /// Provides a static instance of the <see cref="AddOutBoxEventItemInterceptor"/> class.
        /// </summary>
        /// <remarks>This instance is intended for use as a shared interceptor to handle outbox event item
        /// operations.</remarks>
        private static readonly AddOutBoxEventItemInterceptor _addOutBoxEventItemInterceptor = new AddOutBoxEventItemInterceptor();
        #endregion
        public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }

        public BaseOutboxCommandDbContext(DbContextOptions options) : base(options)
        {

        }

        protected BaseOutboxCommandDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(_addOutBoxEventItemInterceptor);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new OutBoxEventItemConfig());
        }


    }
}
