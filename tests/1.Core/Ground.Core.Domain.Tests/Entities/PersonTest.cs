using Ground.Core.Domain.Entities;
using Ground.Samples.Core.Domain.People.Entities;
using Ground.Samples.Core.Domain.People.Events;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Shouldly;

namespace Ground.Core.Domain.Tests.Entities
{
    [Trait("Category","AggregateRoot")]
    public class PersonTest
    {
        [Fact]
        public void Should_AuditableEntity_When_CreatePerson()
        {
            //Arrange
            FirstName firstName = new("Abbas");
            LastName lastName = new("Vosoughi");
            Person person = new(firstName, lastName);

            //Act

            //Assert
            person.ShouldBeAssignableTo<IAuditableEntity>();
        }

        [Fact]        
        public void Should_RaiseCreatedEvent_When_CreatePerson()
        {
            //Arrange            
            FirstName firstName = new("Abbas");
            LastName lastName = new("Vosoughi");
            
            //Act
            Person person = new (firstName, lastName);
            PersonCreated personCreatedEvent = new(person.BusinessId.Value, firstName.Value, lastName.Value);
            var events = person.GetEvents().ToList();
            
            //Assert
            events.ShouldNotBeEmpty();
            Should.Equals(events[0] as PersonCreated, personCreatedEvent);
        }
    }
}
