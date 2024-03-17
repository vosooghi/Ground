using Ground.Core.Domain.Entities;
using Ground.Core.Domain.Exceptions;
using Ground.Samples.Core.Domain.People.Events;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Ground.Samples.Core.Domain.Shared;

namespace Ground.Samples.Core.Domain.People.Entities
{
    public class Person:AggregateRoot
    {
        #region Properties
        public FirstName FirstName { get;private set; }
        public LastName LastName { get; private set; }
        #endregion
        public Person(FirstName firstName,LastName lastName)
        {
            
            FirstName = firstName;
            LastName = lastName;
            
            AddEvent(new PersonCreated(BusinessId.Value, firstName.Value, lastName.Value));
        }
    }
}
