using Ground.Infra.Data.Sql.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Infra.Data.Sql.Commands.Common
{
    public class SampleCommandDbContext : BaseCommandDbContext
    {
        public SampleCommandDbContext(DbContextOptions<SampleCommandDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            //at first, call this
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            //then base config
            base.OnModelCreating(builder);            
        }
    }
}
