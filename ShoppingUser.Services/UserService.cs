using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingUser.API;
using System.Text.Json;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace ShoppingUser.Services
{
    public class UserService
    {
        private UserContext _userContext;
        private IDistributedCache _distributedCache;
        ILogger<UserService> _logger;
        public UserService(UserContext userContext, IDistributedCache distributedCache, ILogger<UserService> logger)
        {
            _userContext = userContext;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public IEnumerable<ShoppingUserModel> GetAllShoppingUsers()
        {
            _logger.LogInformation("Fetching all users info!"); // in built logger
            Log.Information("writing in a text file"); // Serilog
            var users = _distributedCache.GetString("Users");
            if(users == null)
            {
                _logger.LogWarning("Fetching from DB!");
                var userFromDB = _userContext.ShoppingUser;
                var distributedCacheOption = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)).SetSlidingExpiration(TimeSpan.FromMinutes(1));
                _distributedCache.SetString("Users", JsonSerializer.Serialize(userFromDB), distributedCacheOption);
                return userFromDB;
            }
            _logger.LogWarning("Fetched from Redis!");
            return JsonSerializer.Deserialize<IEnumerable<ShoppingUserModel>>(users);
        }

        public async Task<bool> IsEmailIdUnique(string email, CancellationToken ct)
        {
            return !await _userContext.ShoppingUser.AnyAsync(u=> u.Email == email, ct); // return true or false
        }
    }
}