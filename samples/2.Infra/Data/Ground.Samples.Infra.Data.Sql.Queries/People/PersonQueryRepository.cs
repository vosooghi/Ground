using Ground.Infra.Data.Sql.Queries;
using Ground.Samples.Core.Contracts.People.Commands;
using Ground.Samples.Core.Contracts.People.Queries;
using Ground.Samples.Core.Domain.People.Entities;
using Ground.Samples.Infra.Data.Sql.Queries.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Infra.Data.Sql.Queries.People
{
    public class PersonQueryRepository : BaseQueryRepository<SampleQueryDbContext>,IPersonQueryRepository
    {
        public PersonQueryRepository(SampleQueryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
