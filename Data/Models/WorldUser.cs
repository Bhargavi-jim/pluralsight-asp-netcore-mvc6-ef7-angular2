using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyWorld.Data.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}