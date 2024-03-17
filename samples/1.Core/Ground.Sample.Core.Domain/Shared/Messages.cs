using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Samples.Core.Domain.Shared
{
    public class Messages
    {
        public static string InvalidStringLength = "The length of {0} must be between {1}-{2}";
        public static string InvalidNullValue = "{0} should not be Null";
        public static string InvalidNumberValueRange = "The value of {0} should not be less than {1}";
        public static string FirstName = nameof(FirstName);
        public static string Id = nameof(Id);
    }
}
