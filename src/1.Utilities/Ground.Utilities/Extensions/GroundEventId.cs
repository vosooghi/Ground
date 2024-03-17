using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ground.Utilities.Extensions
{
    /// <summary>
    /// for categorizing events
    /// </summary>
    public class GroundEventId
    {
        public const int PerformanceMeasurement = 1001;

        public const int DomainValidationException = 1010;

        public const int CommandValidation = 1011;

        public const int QueryValidation = 1012;

        public const int EventValidation = 1013;
    }
}
