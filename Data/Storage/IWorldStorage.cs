using System.Collections.Generic;
using TheWorld.Data.Models;

namespace MyWorld.Data.Storage
{
    public interface IWorldStorage 
    {
        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop stop);
        Trip GetByKey(string tripName);
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        
    }
}