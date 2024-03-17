using Ground.Infra.Data.Sql.Commands;
using Ground.Samples.Core.Contracts.People.Commands;
using Ground.Samples.Core.Domain.People.Entities;
using Ground.Samples.Infra.Data.Sql.Commands.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Infra.Data.Sql.Commands.People
{
    public class PersonCommandRepository : BaseCommandRepository<Person, SampleCommandDbContext>, IPersonCommandRepository
    {
        public PersonCommandRepository(SampleCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
