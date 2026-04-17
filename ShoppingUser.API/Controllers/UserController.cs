using Microsoft.AspNetCore.Mvc;
using ShoppingUser.Services;

namespace ShoppingUser.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private AuthService authService1;
        public UserController(AuthService authService)
        {
            authService1 = authService;
        }

        [HttpGet("/users")]
        public IActionResult GetAllUsers()
        {
            return Ok();
        }

    }
}
