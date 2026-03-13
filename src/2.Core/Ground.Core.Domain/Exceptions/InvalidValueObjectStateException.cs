namespace Ground.Core.Domain.Exceptions
{
    /// <summary>
    /// The exception related to invalid state of a ValueObject. It is thrown when the state of a ValueObject is invalid.
    /// Such as when the value of a property is null or empty, or when the value of a property does not meet certain criteria.
    /// </summary>
    public class InvalidValueObjectStateException : DomainStateException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidValueObjectStateException"/> class with a specified error message and parameters.
        /// </summary>
        /// <param name="message">String message or Message Pattern</param>
        /// <param name="parameters">The parameters of message patterns</param>
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message, parameters)
        {
        }
    }
}
