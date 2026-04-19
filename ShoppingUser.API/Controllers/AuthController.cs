using System.Threading.Tasks;
using FluentValidation;
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
        private IValidator<UserDto> _validator;
        public AuthController(AuthService authService, IValidator<UserDto> validator)
        {
            authService1 = authService;
            _validator = validator;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignupUser([FromBody] UserDto user)
        {
            var validationResult = await _validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            authService1.SignupUser(user);
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
