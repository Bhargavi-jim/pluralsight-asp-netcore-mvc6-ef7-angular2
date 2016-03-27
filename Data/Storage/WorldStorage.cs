using System;
using System.Collections.Generic;
using System.Linq;
using TheWorld.Data.Models;

namespace MyWorld.Data.Storage
{
    public class WorldStorage : IWorldStorage
    {
        private Dictionary<string, Trip> trips;
        private int tripId;
        private int stopId;
        
        public WorldStorage()
        {
            trips = new Dictionary<string, Trip>();
            tripId = 0;
            stopId = 0;
        }

        public void AddStop(string tripName, Stop stop)
        {            
            var trip = GetByKey(tripName);
            if (trip == null)
            {
                throw new InvalidOperationException("Tried to add stops to a non-existent trip: ${tripName}");
            }

            stop.Id = ++stopId;
            stop.Arrival = DateTime.Now;
            stop.Longitude = 1.24;  // Sample Longitude
            stop.Latitude = 4.25;   // Sample Latitude
            
            if(trip.Stops == null)
            {
                trip.Stops = new List<Stop>();
            }
            
            trip.Stops.Add(stop);
            AddTrip(trip);
        }

        public void AddTrip(Trip trip)
        {
            trip.Id = ++tripId;
            var result = GetByKey(trip.Name);
            if (result != null)
            {
                trips.Remove(trip.Name);                
            }
            trips.Add(trip.Name, trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return trips.Select(t => new Trip
            {
                Id = t.Value.Id,
                Name = t.Value.Name,
                Created = t.Value.Created,
                UserName = t.Value.UserName,
            });
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            return trips.Select(t => new Trip
            {
                Id = t.Value.Id,
                Name = t.Value.Name,
                Created = t.Value.Created,
                UserName = t.Value.UserName,
                Stops = t.Value.Stops
            });
        }

        public Trip GetByKey(string tripName)
        {
            Trip trip = null;
            trips.TryGetValue(tripName, out trip);
            return trip;
        }
    }
}