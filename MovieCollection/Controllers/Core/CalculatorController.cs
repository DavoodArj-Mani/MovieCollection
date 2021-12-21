using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MovieCollection.Controllers.Core
{
    [Authorize]
    [Route("api/{controller}")]
    [ApiController]
    public class CalculatorController : Controller
    {
        [HttpPost]
        [Route("SumValue")]
        public IActionResult sum([FromQuery (Name = "Value1")] int value1, [FromQuery(Name = "Value2")] int value2)
        {
            var result = value1 + value2;
            return Ok(result);
        }
    }
}
