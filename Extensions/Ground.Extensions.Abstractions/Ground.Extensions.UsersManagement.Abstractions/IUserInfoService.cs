namespace Ground.Extensions.UsersManagement.Abstractions
{
    /// <summary>
    /// Provides a contract for accessing information about the current user.
    /// </summary>    
    public interface IUserInfoService
    {
        string GetUserAgent();
        string GetUserIp();
        string UserId();
        string GetFirstName();
        string GetLastName();
        string GetUsername();
        string? GetClaim(string claimType);
        bool IsCurrentUser(string userId);
        string UserIdOrDefault();
        string UserIdOrDefault(string defaultValue);
    }
}
