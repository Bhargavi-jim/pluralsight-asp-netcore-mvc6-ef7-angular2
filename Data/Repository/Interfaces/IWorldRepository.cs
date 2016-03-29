using System.Collections.Generic;
using TheWorld.Data.Models;

namespace MyWorld.Data.Repository
{
    public interface IWorldRepository
    {
        void AddTrip(Trip newTrip);
        void AddStop(string tripName, Stop newStop, string username);
        Trip GetStopsByTripName(string tripName, string username);
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        IEnumerable<Trip> GetUserTrips(string name);
        bool SaveAll();
    }
}