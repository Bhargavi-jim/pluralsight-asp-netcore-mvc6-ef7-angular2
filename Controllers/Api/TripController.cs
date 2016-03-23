using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using MyWorld.Data.Repository;
using MyWorld.ViewModels;
using TheWorld.Data.Models;

namespace MyWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        IWorldRepository _worldRepository;

        public TripController(IWorldRepository worldRepository)
        {
            _worldRepository = worldRepository;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var result = _worldRepository.GetAllTrips();
            var viewModel = Mapper.Map<IEnumerable<TripViewModel>>(result);
            return Json(viewModel);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(viewModel);  // Do this in the business class
                
                // Save to database.. call the facade => business => repository.
                
                Response.StatusCode = (int) HttpStatusCode.Created;
                return Json(Mapper.Map<TripViewModel>(newTrip));
            }
            
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}