using Ground.Core.Domain.Exceptions;
using Ground.Core.Domain.ValueObjects;
using Ground.Samples.Core.Domain.Shared;
using Ground.Utilities.Extensions;

namespace Ground.Samples.Core.Domain.People.ValueObjects
{
    public class FirstName : BaseValueObject<FirstName>
    {
        public string Value { get;private set; }
        public FirstName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidValueObjectStateException(Messages.InvalidNullValue, Messages.FirstName);
            value = value.Trim();            
            if (!value.IsLengthBetween(2,50)) throw new InvalidValueObjectStateException(Messages.InvalidStringLength,Messages.FirstName, "2", "50");
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
