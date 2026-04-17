using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingUser.Domain;
using ShoppingUser.Services;

namespace ShoppingUser.API.Controllers
{
    [ApiController()]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService1;
        public AuthController(AuthService authService)
        {
            authService1 = authService;
        }

        [HttpPost("/signup")]
        public IActionResult SignupUser([FromBody] UserDto user)
        {
            var result =  authService1.SignupUser(user);
            return Created();
        }

        [HttpGet("signin")]
        public IActionResult SigninUser(string EmailId, string Password)
        {
            try
            {
                var result = authService1.SigninUser(EmailId, Password);
                return Ok("Logged in successfully");
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("Email/Password does not match!");
            }
        }
    }
}
