using System.Collections.Generic;
using System.Linq;
using MyWorld.Data.Storage;
using MyWorld.Data.Models;
using System;

namespace MyWorld.Data.Repository
{
    public class FakeWorldRepository : IWorldRepository
    {
        private readonly IWorldStorage _storage;

        public FakeWorldRepository(IWorldStorage storage)
        {
            _storage = storage;
        }
        public void AddStop(string tripName, Stop newStop, string username)
        {
            _storage.AddStop(tripName, newStop, username);            
        }

        public void AddTrip(Trip newTrip)
        {
            _storage.AddTrip(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _storage.GetAllTrips();
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            return _storage.GetAllTripsWithStops();
        }

        public Trip GetStopsByTripName(string tripName, string username)
        {
            var trip = _storage.GetByKey(tripName, username);
            if(trip.UserName.Equals(username))
            {
                return trip;
            }
            return null;
        }

        public IEnumerable<Trip> GetUserTrips(string username)
        {
            return _storage.GetAllTrips().Where(t => t.UserName.Equals(username));   // Remember to make t.Username not nullable in EF. 
        }

        public bool SaveAll()
        {
            return true;
        }
    }
}