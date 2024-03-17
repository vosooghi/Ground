using Ground.Infra.Data.Sql.Commands;
using Ground.Samples.Core.Contracts.Common;

namespace Ground.Samples.Infra.Data.Sql.Commands.Common
{
    public class SampleUnitOfWork : BaseEntityFrameworkUnitOfWork<SampleCommandDbContext>, ISampleUnitOfWork
    {
        public SampleUnitOfWork(SampleCommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
