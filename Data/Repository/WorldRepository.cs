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
        private readonly WorldContext _context;        
        private readonly ILogger<WorldRepository> _logger;
        public WorldRepository(ILogger<WorldRepository> logger, WorldContext context)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetStopsByTripName(tripName);
            
            newStop.Order = trip.Stops.Max(s => s.Order) + 1;
            
            _context.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
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
                return _context.Trips.ToList();
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

        public Trip GetStopsByTripName(string name)
        {
            if(!String.IsNullOrWhiteSpace(name))
            {
                return _context.Trips
                               .Include(t => t.Stops)
                               .Where(t => name.Equals(t.Name))
                               .FirstOrDefault();
            }
            return null;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;    // Returns number of records changed
        }
    }
}