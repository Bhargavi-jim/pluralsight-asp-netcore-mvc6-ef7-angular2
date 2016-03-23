using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using TheWorld.Data.Models;

namespace MyWorld.Data.Repository
{
    public class WorldRepository : IWorldRepository
    {
        //private readonly WorldContext _context;        
        private readonly ILogger<WorldRepository> _logger;
        public WorldRepository(ILogger<WorldRepository> logger)
        {
            //_context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                var trips = new List<Trip>
                {
                    new Trip
                    {
                        Id = 1,
                        Name = "Holiday",
                        Created = new DateTime(),
                        UserName = "Ironman",
                    }
                };
                return trips.ToList();
                //return _context.Trips.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get all trips", ex);
                throw;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                var trips = new List<Trip>
                {
                    new Trip
                    {
                        Id = 1,
                        Name = "Holiday",
                        Created = new DateTime(),
                        UserName = "Ironman",
                        Stops = new List<Stop>
                        {
                            new Stop
                            {
                                
                            }
                        }
                    }
                };
                return trips.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get all trips with stops", ex);
                throw;
            }
        }
    }
}