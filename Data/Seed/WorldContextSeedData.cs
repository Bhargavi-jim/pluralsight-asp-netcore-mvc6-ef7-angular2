using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyWorld.Data.Models;

namespace MyWorld.Data.Seed
{
    public class WorldContextSeedData
    {
        private readonly WorldContext _context;
        private readonly UserManager<WorldUser> _userManager;
        
        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task EnsureSeedDataAsync() 
        {
            if(_userManager.FindByEmailAsync("vincent@myworld.com.au") == null)
            {
                var user = new WorldUser()
                {
                    UserName = "vincent",
                    Email = "vincent@myworld.com.au"
                };
                
                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }
            
            // Seed some data if none exist.
            if(!_context.Trips.Any())
            {
                
            }
        }
    }
}