using Ground.Core.Domain.Exceptions;
using Ground.Core.Domain.ValueObjects;
using Ground.Samples.Core.Domain.Shared;

namespace Ground.Samples.Core.Domain.People.ValueObjects
{
    public class LastName : BaseValueObject<FirstName>
    {
        public string Value { get; private set; }
        public LastName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidValueObjectStateException(Messages.InvalidNullValue, Messages.FirstName);
            value = value.Trim();            
            if (value.Length < 2 || value.Length > 50) throw new InvalidValueObjectStateException(Messages.InvalidStringLength, "2", "50");
            Value = value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
