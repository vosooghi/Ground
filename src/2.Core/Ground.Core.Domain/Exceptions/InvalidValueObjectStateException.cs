namespace Ground.Core.Domain.Exceptions
{
    /// <summary>
    /// This exception is thrown when a ValueObject is in an invalid state and cannot perform the requested operation.
    /// </summary>
    public class InvalidValueObjectStateException : DomainStateException
    {
        /// <summary>
        /// The exceptions related to invalid state of a ValueObject.
        /// </summary>
        /// <param name="message">String message or Message Pattern</param>
        /// <param name="parameters">the parameters of message patterns</param>
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message, parameters)
        {
        }
    }
}
