namespace Ground.Core.Domain.Exceptions
{
    /// <summary>
    /// This exception is thrown when an Entity is in an invalid state and cannot perform the requested operation.
    /// </summary>
    public class InvalidEntityStateException : DomainStateException
    {
        /// <summary>
        /// The exceptions related to invalid state of an Entity is thrown by this class.
        /// </summary>
        /// <param name="message">String message or Message Pattern</param>
        /// <param name="parameters">the parameters of message patterns</param>
        public InvalidEntityStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}
