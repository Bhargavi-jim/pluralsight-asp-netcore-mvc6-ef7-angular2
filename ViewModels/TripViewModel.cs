using System;
using System.ComponentModel.DataAnnotations;

namespace MyWorld.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(225, MinimumLength = 5)]
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}