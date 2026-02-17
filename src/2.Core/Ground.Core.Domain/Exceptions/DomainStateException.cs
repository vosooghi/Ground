namespace Ground.Core.Domain.Exceptions
{
    /// <summary>
    /// Provides a base class for exceptions that occur due to invalid state in the domain layer.
    /// </summary>
    public class DomainStateException : Exception
    {
        /// <summary>
        /// the paramters to be sent as exception message.
        /// </summary>
        public string[] Parameters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainStateException"/> class with a specified error message and optional parameters.
        /// </summary>
        /// <param name="message">The error message or message pattern.</param>
        /// <param name="parameters">The parameters for the message pattern.</param>
        public DomainStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;

        }
        /// <summary>
        /// If there is some parameters, it returns message as a patterns, else returns Message.
        /// </summary>
        /// <returns>String Message or Message Pattern</returns>
        public override string ToString()
        {
            if (Parameters?.Length < 1)
            {
                return Message;
            }

            string result = Message;

            for (int i = 0; i < Parameters?.Length; i++)
            {
                string placeHolder = $"{{{i}}}";
                result = result.Replace(placeHolder, Parameters[i]);
            }

            return result;
        }
    }
}
