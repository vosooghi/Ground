using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ground.Extensions.UsersManagement.Abstractions;
using Ground.Extensions.UsersManagement.Options;
using Ground.Extensions.UsersManagement.Extensions;

namespace Ground.Extensions.UsersManagement.Services
{
    public class WebUserInfoService : IUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManagementOptions _configuration;

        public WebUserInfoService(IHttpContextAccessor httpContextAccessor, IOptions<UserManagementOptions> configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration.Value;
        }

        public string GetUserAgent()
        => _httpContextAccessor?.HttpContext?.Request?.Headers["User-Agent"] ?? _configuration.DefaultUserAgent;

        public string GetUserIp()
        => _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? _configuration.DefaultUserIp;

        public string UserId()
        => _httpContextAccessor?.HttpContext?.User?.GetClaim(_configuration.DefaultUserIdClaim) ?? string.Empty;

        public string GetUsername()
        => _httpContextAccessor.HttpContext?.User?.GetClaim(_configuration.DefaultUserNameClaim) ?? _configuration.DefaultUsername;

        public string GetFirstName()
        => _httpContextAccessor.HttpContext?.User?.GetClaim(_configuration.DefaultFirstNameClaim) ?? _configuration.DefaultFirstName;

        public string GetLastName()
        => _httpContextAccessor.HttpContext?.User?.GetClaim(_configuration.DefaultLastNameClaim) ?? _configuration.DefaultLastName;

        public bool IsCurrentUser(string userId)
        {
            return string.Equals(UserId().ToString(), userId, StringComparison.OrdinalIgnoreCase);
        }

        public string? GetClaim(string claimType)
        => _httpContextAccessor.HttpContext?.User?.GetClaim(claimType);

        public string UserIdOrDefault() => UserIdOrDefault(_configuration.DefaultUserId);

        public string UserIdOrDefault(string defaultValue)
        {
            string userId = UserId();
            return string.IsNullOrEmpty(userId) ? defaultValue : userId;
        }
    }
}
