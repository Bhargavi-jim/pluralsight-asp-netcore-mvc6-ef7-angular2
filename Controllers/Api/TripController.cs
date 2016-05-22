using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using MyWorld.Data.Repository;
using MyWorld.ViewModels;
using MyWorld.Data.Models;

namespace MyWorld.Controllers.Api
{
    //[Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        IWorldRepository _repository;
        ILogger<TripController> _logger;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        [HttpGet("")]
        public JsonResult Get()
        {
            _logger.LogInformation("Attempting to get Trips from database");
            
            var result = _repository.GetUserTrips(User.Identity.Name);
            var viewModel = Mapper.Map<IEnumerable<TripViewModel>>(result);
            
            return Json(viewModel);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(viewModel);  // Do this in the business class
                newTrip.UserName = User.Identity.Name;
                
                // Save to database.. call the facade => business => repository.
                _logger.LogInformation("Attempting to save Trips to database");
                _repository.AddTrip(newTrip);
                
                if(_repository.SaveAll())  // EF saves and pushes changes back into newTrip
                {
                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return Json(Mapper.Map<TripViewModel>(newTrip));                    
                }                
            }
            
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}