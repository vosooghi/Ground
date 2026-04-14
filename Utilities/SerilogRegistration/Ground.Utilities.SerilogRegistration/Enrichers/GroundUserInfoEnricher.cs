using Ground.Extensions.UsersManagement.Abstractions;
using Serilog.Core;
using Serilog.Events;

namespace Ground.Utilities.SerilogRegistration.Enrichers
{
    /// <summary>
    /// Add user/request identity context to each LogEvent using the <inheritdoc cref="IUserInfoService"/>
    /// </summary>
    public class GroundUserInfoEnricher : ILogEventEnricher
    {
        private readonly IUserInfoService _userInfoService;

        public GroundUserInfoEnricher(IUserInfoService userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
        {
            var userName = _userInfoService.GetUsername() ?? "Unknown";
            var userId = _userInfoService.UserIdOrDefault() ?? "Unknown";
            var userIp = _userInfoService.GetUserIp() ?? "Unknown";
            var clientId = _userInfoService.GetClaim("client_id") ?? "Unknown";

            var userNameProperty = factory.CreateProperty("UserName", userName);
            var userIdProperty = factory.CreateProperty("UserId", userId);
            var userIpProperty = factory.CreateProperty("UserIp", userIp);
            var clientIdProperty = factory.CreateProperty("ClientId", clientId);

            logEvent.AddPropertyIfAbsent(userNameProperty);
            logEvent.AddPropertyIfAbsent(userIdProperty);
            logEvent.AddPropertyIfAbsent(userIpProperty);
            logEvent.AddPropertyIfAbsent(clientIdProperty);
        }
    }
}
