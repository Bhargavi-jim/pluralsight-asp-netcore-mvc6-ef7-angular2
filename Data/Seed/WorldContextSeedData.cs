using System;
using System.Collections.Generic;
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
            if(await _userManager.FindByEmailAsync("vincent@myworld.com.au") == null)
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
                var usTrip = new Trip
                {
                    Name = "US Trip",
                    Created = DateTime.Now,
                    UserName = string.Empty,
                    Stops = new List<Stop>
                    {
                        new Stop{ Name = "Atlanta, GA", Arrival = new DateTime(2014, 2, 3), Latitude =  33.748995, Longitude = -84.387982, Order = 0},
                        new Stop{ Name = "New York, NY", Arrival = new DateTime(2015, 6, 28), Latitude =  40.712784, Longitude = -74.005941, Order = 1}
                    }
                };
                
                var worldTrip = new Trip
                {
                    Name = "World Trip",
                    Created = DateTime.Now,
                    UserName = string.Empty,
                    Stops = new List<Stop>
                    {
                        new Stop{ Name = "Penang, MY", Arrival = new DateTime(2015, 12, 4), Latitude =  33.748995, Longitude = -84.387982, Order = 0},
                    }
                };
                
                _context.Add(usTrip);
                _context.AddRange(usTrip.Stops);
                _context.Add(worldTrip);
                _context.AddRange(worldTrip.Stops);
                
                _context.SaveChanges();
            }            
        }
    }
}