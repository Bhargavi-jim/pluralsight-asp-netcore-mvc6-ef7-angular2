using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using MyWorld.Data.Models;

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

        public void AddStop(string tripName, Stop newStop, string username)
        {
            var trip = GetStopsByTripName(tripName, username);
            
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
                return _context.Trips.Include(t => t.Stops).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get all trips with stops", ex);
                throw;
            }
        }

        public Trip GetStopsByTripName(string tripName, string username)
        {
            if(!String.IsNullOrWhiteSpace(tripName) && !String.IsNullOrWhiteSpace(username))
            {
                return _context.Trips
                               .Include(t => t.Stops)
                               .Where(t => tripName.Equals(t.Name) && username.Equals(t.UserName))
                               .FirstOrDefault();
            }
            return null;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;    // Returns number of records changed
        }

        public IEnumerable<Trip> GetUserTrips(string username)
        {
            if(string.IsNullOrWhiteSpace(username))
            {
                return Enumerable.Empty<Trip>();                
            }
            
            try
            {
                var trips = _context.Trips.Where(t => t.UserName.Equals(username));
                return trips;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get all trips", ex);
                throw;
            }
        }
    }
}