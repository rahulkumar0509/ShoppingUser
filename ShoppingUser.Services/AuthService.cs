using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using ShoppingUser.API;
using ShoppingUser.Domain;
namespace ShoppingUser.Services
{
    public class AuthService
    {
        private UserContext _userContext;
        private IMemoryCache _memoryCache;
        public AuthService(UserContext userContext, IMemoryCache memoryCache)
        {
            _userContext = userContext;
            _memoryCache = memoryCache;
        }

        public string SignupUser(UserDto user)
        {
            try
            {
                var shoppingUser = new ShoppingUserModel
                {
                    // Map properties from UserDto to ShoppingUser
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password,
                    Phone = user.Phone
                };
                _userContext.ShoppingUser.Add(shoppingUser);
                _userContext.SaveChanges();
                return shoppingUser.Email;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool SigninUser(string EmailId, string Password)
        {
            try
            {
                var result = _memoryCache.GetOrCreate("userInfo", entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return _userContext.ShoppingUser.Single(u => u.Email == EmailId && u.Password == Password);
                });
                if (result != null)
                {
                    return true;
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

    }
}
