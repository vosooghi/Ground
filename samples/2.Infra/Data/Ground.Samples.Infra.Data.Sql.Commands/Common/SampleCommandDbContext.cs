using Ground.Infra.Data.Sql.Commands;
using Microsoft.EntityFrameworkCore;

namespace Ground.Samples.Infra.Data.Sql.Commands.Common
{
    public class SampleCommandDbContext : BaseCommandDbContext
    {
        public SampleCommandDbContext(DbContextOptions<SampleCommandDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Set to false to disable the default auditing behavior in SaveChanges, which is handled by AddAuditDataInterceptor in this project. 
        /// This avoids double execution of audit data population.
        /// </summary>
        protected override bool UseAuditingSaveChangesHook => false;

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            //at first, call this
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            //then base config
            base.OnModelCreating(builder);            
        }
    }
}
