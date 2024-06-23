using Ground.Core.Domain.Exceptions;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Shouldly;

namespace Ground.Core.Domain.Tests.ValueObjects
{
    [Trait("Category", "ValueObject")]
    public class FirstNameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("012345678901234567890123456789012345678901234567890")]
        public void Should_ThrowInvalidValueObjectStateException_When_InputIsInvalid(string inputData)
        {
            //Arrange

            FirstName firstName;
            //Act

            //Assert
            Should.Throw<InvalidValueObjectStateException>(()=> firstName = new FirstName(inputData));
        }
        [Fact]
        public void Should_ReturnTrue_When_CheckEqualityWithSameValue()
        {
            //Arrange
            FirstName firstName = new("Abbas");
            LastName lastName = new("Abbas");
            //Act

            //Assert
            Should.Equals(firstName, lastName);
        }
    }
}
