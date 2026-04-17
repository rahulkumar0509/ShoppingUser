using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using ShoppingUser.API;
using System.Text.Json;

namespace ShoppingUser.Services
{
    public class UserService
    {
        private UserContext _userContext;
        private IDistributedCache _distributedCache;
        public UserService(UserContext userContext, IDistributedCache distributedCache)
        {
            _userContext = userContext;
            _distributedCache = distributedCache;
        }

        public IEnumerable<ShoppingUserModel> GetAllShoppingUsers()
        {
            var users = _distributedCache.GetString("Users");
            if(users == null)
            {
                var userFromDB = _userContext.ShoppingUser;
                var distributedCacheOption = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)).SetSlidingExpiration(TimeSpan.FromMinutes(3));
                _distributedCache.SetString("", JsonSerializer.Serialize(userFromDB), distributedCacheOption);
                return userFromDB;
            }
            return JsonSerializer.Deserialize<IEnumerable<ShoppingUserModel>>(users);
        }
    }
}