using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleAPiController : ControllerBase
    {
        [HttpGet]
        public string Get() {
            return "data";
        }
    }
}
