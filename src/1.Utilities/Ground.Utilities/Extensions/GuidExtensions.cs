namespace Ground.Utilities.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Guid"/> struct, allowing for easy checks of null or empty GUIDs.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Determines whether the specified GUID is null or empty.
        /// </summary>
        /// <param name="guid">The GUID to check.</param>
        /// <returns><c>true</c> if the GUID is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this Guid? guid) => guid == null || guid == default;

        /// <summary>
        /// Determines whether the specified GUID is empty.
        /// </summary>
        /// <param name="guid">The GUID to check.</param>
        /// <returns><c>true</c> if the GUID is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty(this Guid guid) => guid == default;
    }
}
