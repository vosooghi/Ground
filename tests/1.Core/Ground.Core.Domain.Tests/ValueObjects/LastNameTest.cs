using Ground.Core.Domain.Exceptions;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Shouldly;

namespace Ground.Core.Domain.Tests.ValueObjects
{
    [Trait("Category","ValueObject")]
    public class LastNameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("012345678901234567890123456789012345678901234567890")]
        public void Should_ThrowInvalidValueObjectStateException_When_InputIsInvalid(string inputData)
        {
            //Arrange

            LastName lastName;
            //Act

            //Assert
            Should.Throw<InvalidValueObjectStateException>(() => lastName = new LastName(inputData));
        }
    }
}
