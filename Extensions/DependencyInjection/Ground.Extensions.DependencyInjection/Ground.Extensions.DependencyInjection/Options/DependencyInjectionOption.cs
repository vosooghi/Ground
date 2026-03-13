namespace Ground.Extensions.DependencyInjection.Options
{
    /// <summary>
    /// The options for dependency injection.
    /// </summary>
    public class DependencyInjectionOption
    {
        /// <summary>
        /// Gets or sets a comma-separated list of assembly names to be loaded at runtime.
        /// </summary>
        public string AssmblyNamesForLoad { get; set; } = string.Empty;
    }
}
