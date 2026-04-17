using Microsoft.AspNetCore.Mvc;
using ShoppingUser.Services;

namespace ShoppingUser.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = _userService.GetAllShoppingUsers();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Couldn't fetch the users: " + ex);
            }
            
        }

    }
}
