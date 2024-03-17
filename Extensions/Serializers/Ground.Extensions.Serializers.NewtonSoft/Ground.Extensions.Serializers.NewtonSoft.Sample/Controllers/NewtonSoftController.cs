using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ground.Extensions.Serializers.Abstractions;
using Ground.Extensions.Serializers.NewtonSoft;
using System;
using Ground.Extensions.Serializers.NewtonSoft.Sample.Models;

namespace Ground.Extensions.Serializers.NewtonSoft.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewtonSoftController : ControllerBase
    {
        private readonly IJsonSerializer _jsonSerializer;
        public NewtonSoftController(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        [HttpGet("Serilize")]
        public IActionResult Serilize([FromQuery] Person person) => Ok(_jsonSerializer.Serialize(person));

        [HttpGet("Deserialize")]
        public IActionResult Deserialize([FromQuery] string input) => Ok(_jsonSerializer.Deserialize<Person>(input));
    }
}
