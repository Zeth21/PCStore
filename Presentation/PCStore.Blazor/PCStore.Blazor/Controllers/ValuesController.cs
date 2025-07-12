using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PCStore.Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            var data = new[]
            {
                new {
                name = "Patrick",
                surname = "God",
                age = "32"
                },
                new {
                name = "Zeyitcan",
                surname = "DAŞDEMİR",
                age = "23"
                },
                 new {
                name = "Daniel",
                surname = "Richards",
                age = "32"
                },
                new {
                name = "Mustafa",
                surname = "Kemal",
                age = "21"
                }
        };
            return Ok(data);
        }
    }
}
