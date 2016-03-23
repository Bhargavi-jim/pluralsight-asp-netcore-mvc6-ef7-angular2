using System.Collections.Generic;
using TheWorld.Data.Models;

namespace MyWorld.Data.Repository
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetAllTripsWithStops();
    }
}