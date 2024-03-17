using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Core.Domain.Exceptions
{
    /// <summary>
    /// A microtype to identify ValueObject Invalid State Exception.
    /// </summary>
    public class InvalidValueObjectStateException : DomainStateException
    {
        /// <summary>
        /// the exceptions related to invalid state of an Entity is thrown by this class.
        /// </summary>
        /// <param name="message">String message or Message Pattern</param>
        /// <param name="parameters">the parameters of message patterns</param>
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message, parameters)
        {
        }
    }
}
