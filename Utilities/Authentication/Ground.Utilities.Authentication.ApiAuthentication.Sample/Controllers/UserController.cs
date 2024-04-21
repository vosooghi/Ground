using Ground.Utilities.Authentication.ApiAuthentication.Sample.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ground.Utilities.Authentication.ApiAuthentication.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet("[action]")]
        public IEnumerable<UserClaim> GetClaims()
            => _httpContextAccessor?.HttpContext?.User.Claims
            .Select(claim => new UserClaim
            {
                Type = claim.Type,
                Value = claim.Value
            }).ToList() ?? [];
    }
}
