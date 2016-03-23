using Microsoft.Data.Entity;
using TheWorld.Data.Models;

namespace MyWorld.Data
{
    public class WorldContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
        
    }
}