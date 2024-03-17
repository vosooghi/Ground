using Ground.Extensions.Events.Abstractions;
using Ground.Extensions.Events.Outbox.Dal.EF.Configs;
using Ground.Extensions.Events.Outbox.Dal.EF.Interceptors;
using Ground.Infra.Data.Sql.Commands;
using Microsoft.EntityFrameworkCore;

namespace Ground.Extensions.Events.Outbox.Dal.EF
{
    public abstract class BaseOutboxCommandDbContext : BaseCommandDbContext
    {
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
            optionsBuilder.AddInterceptors(new AddOutBoxEventItemInterceptor());
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new OutBoxEventItemConfig());
        }


    }
}
