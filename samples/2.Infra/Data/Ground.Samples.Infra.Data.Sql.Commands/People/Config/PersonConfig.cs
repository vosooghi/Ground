using Ground.Samples.Core.Domain.People.Entities;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Infra.Data.Sql.Commands.People.Config
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //value Objects
            builder.Property(c => c.FirstName).HasConversion(c => c.Value, c => new FirstName(c));
            builder.Property(c => c.LastName).HasConversion(c => c.Value, c => new LastName(c));
        }
    }
}
