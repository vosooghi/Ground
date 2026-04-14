namespace Ground.Utilities.Authentication.ApiAuthentication.Options
{
    /// <summary>
    /// Represents the type of token used for authentication.
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Specifies that the authentication token format is JSON Web Token (JWT).
        /// </summary>
        Jwt = 1,
        /// <summary>
        /// Indicates that the authentication token is a reference token.
        /// </summary>
        Reference = 2
    }
}
