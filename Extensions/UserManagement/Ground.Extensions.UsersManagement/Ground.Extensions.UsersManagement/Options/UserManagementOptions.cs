using System.Security.Claims;

namespace Ground.Extensions.UsersManagement.Options
{
    public sealed class UserManagementOptions
    {
        public string DefaultUserId { get; set; } = "1";
        public string DefaultUserIdClaim { get; set; } = ClaimTypes.NameIdentifier;
        public string DefaultUserAgent { get; set; } = "Unknown";
        public string DefaultUserIp { get; set; } = "0.0.0.0";

        public string DefaultUsername { get; set; } = "UnknownUserName";
        public string DefaultUserNameClaim { get; set; } = ClaimTypes.Name;
        public string DefaultFirstName { get; set; } = "UnknownFirstName";
        public string DefaultFirstNameClaim { get; set; } = ClaimTypes.GivenName;
        public string DefaultLastName { get; set; } = "UnknownLastName";
        public string DefaultLastNameClaim { get; set; } = ClaimTypes.Surname;

    }
}
