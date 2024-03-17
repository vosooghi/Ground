using Ground.Core.Contracts.Data.Commands;
using Ground.Core.Domain.ValueObjects;
using Ground.Samples.Core.Domain.People.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Core.Contracts.People.Commands
{
    public interface IPersonCommandRepository : ICommandRepository<Person,long>
    {
        
    }
}
