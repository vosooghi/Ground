using Ground.Endpoints.WebApi.Controllers;
using Ground.Samples.Core.Contracts.People.Commands;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Ground.Samples.Endpoints.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : BaseController
    {
        [HttpGet("CheckValueObject")]
        public IActionResult CheckFirstNameValueObject(string firstname)
        {
            FirstName fname =new FirstName( firstname);
            return Ok();
        }
        [HttpPost("CreatePerson")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePerson createPerson)
        {
           //var result= CommandDispatcher.Send<CreatePerson,long>(createPerson);
           //we can use our create method in framework
           return await Create<CreatePerson,long>(createPerson);
        }
    }
}
