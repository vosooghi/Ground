using Ground.Core.Contracts.Data.Commands;
using Ground.Samples.Core.Domain.People.Entities;

namespace Ground.Samples.Core.Contracts.People.Commands
{
    public interface IPersonCommandRepository : ICommandRepository<Person,long>
    {
        
    }
}
