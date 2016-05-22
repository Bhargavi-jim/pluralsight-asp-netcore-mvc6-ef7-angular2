using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using MyWorld.Data.Models;

namespace MyWorld.Data
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {
        public WorldContext() 
        {
            Database.EnsureCreated();
            
        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }        
    }
}