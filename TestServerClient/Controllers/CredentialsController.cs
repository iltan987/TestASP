using Microsoft.AspNetCore.Mvc;

namespace TestServerClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] UserCredentials credentials)
        {
            if (credentials.Username == "iltan" && credentials.Password == "admin")
            {
                return Ok("Login successful");
            }
            else
            {
                return Unauthorized("Incorrect username or password");
            }
        }
    }
}