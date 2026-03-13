namespace Ground.Extensions.ObjectMappers.AutoMapper.Options
{
    /// <summary>
    /// Provides configuration options for AutoMapper integration in the Ground.Extensions.ObjectMappers library.
    /// </summary>
    public class AutoMapperOption
    {
        /// <summary>
        /// Gets or sets the list of assembly names used for loading profiles.
        /// </summary>
        public string AssmblyNamesForLoadProfiles { get; set; } = string.Empty;
    }
}
