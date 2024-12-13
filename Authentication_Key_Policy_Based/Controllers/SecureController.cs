using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Key_Policy_Based.Controllers
{
    [Route("api/secure")]
    [ApiController]
    public class SecureController : ControllerBase
    {
        [Route("home")]
        [HttpGet]
        [Authorize(Policy = "ApiKeyPolicy")]
        public IActionResult Home()
        {
            return Ok(new { message = "Welcome to Home" });
        }
    }
}
