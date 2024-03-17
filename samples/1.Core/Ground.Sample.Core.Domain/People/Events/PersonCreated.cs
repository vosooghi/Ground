using Ground.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Core.Domain.People.Events
{
    public class PersonCreated:IDomainEvent
    {
        public Guid  BusinessId { get; }
        public  string FirstName { get; }
        public  string LastName { get; }
        public PersonCreated(Guid businessId,string firstname,string lastname)
        {
            BusinessId = businessId;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
