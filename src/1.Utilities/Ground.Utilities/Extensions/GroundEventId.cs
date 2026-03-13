namespace Ground.Utilities.Extensions
{
    /// <summary>
    /// Defines a set of constant integer values that represent unique identifiers for various events and exceptions in the Ground framework.    
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
