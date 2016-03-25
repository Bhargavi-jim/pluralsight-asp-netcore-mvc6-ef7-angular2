using System.Collections.Generic;
using TheWorld.Data.Models;

namespace MyWorld.Data.Repository
{
    public interface IWorldRepository
    {
        void AddTrip(Trip newTrip);
        Trip GetStopsByTripName(string name);
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        bool SaveAll();
        void AddStop(string tripName, Stop newStop);
    }
}