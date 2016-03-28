using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using MyWorld.Data.Models;
using TheWorld.Data.Models;

namespace MyWorld.Data
{
    public class WorldContext : IdentityDbContext<WorldUser>
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
        
    }
}