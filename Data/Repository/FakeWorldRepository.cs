using System;
using System.Collections.Generic;
using MyWorld.Data.Storage;
using TheWorld.Data.Models;

namespace MyWorld.Data.Repository
{
    public class FakeWorldRepository : IWorldRepository
    {
        private readonly IWorldStorage _storage;

        public FakeWorldRepository(IWorldStorage storage)
        {
            _storage = storage;
        }
        public void AddStop(string tripName, Stop newStop)
        {
            _storage.AddStop(tripName, newStop);            
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

        public Trip GetStopsByTripName(string name)
        {
            return _storage.GetByKey(name);
        }

        public bool SaveAll()
        {
            return true;
        }
    }
}