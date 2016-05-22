using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using MyWorld.Data.Repository;
using MyWorld.ViewModels;
using MyWorld.Data.Models;

namespace MyWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<StopController> _logger;
        public StopController(IWorldRepository repository, ILogger<StopController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        [HttpGet("")]
        public JsonResult GetStopsByTripName(string tripName)
        {
            _logger.LogInformation($"Getting stop by trip name: {tripName}.");
            var result = _repository.GetStopsByTripName(tripName, User.Identity.Name);

            if (result == null || result.Stops != null && !result.Stops.Any())
            {
                return Json(null);
            }

            return Json(Mapper.Map<ICollection<StopViewModel>>(result.Stops.OrderBy(s => s.Order)));
        }

        //http://stackoverflow.com/questions/23016057/web-api-best-approach-for-returning-httpresponsemessage
        // public IHttpActionResult Get()
        // {
        //     Object obj = new Object();
        //     if (obj == null)
        //         return NotFound();
        //     return Ok(obj);
        // }
        
        [HttpPost("")]
        public JsonResult Post(string tripName, [FromBody] StopViewModel viewModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _logger.LogInformation($"Attempting to save stops to trip: {tripName}");
                    
                    var newStop = Mapper.Map<Stop>(viewModel);
                    _repository.AddStop(tripName, newStop, User.Identity.Name);
                    
                    if(_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch(Exception ex)            
            {
                _logger.LogError("Failed to save new Stop", ex);
                throw;                
            }
                     
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed to save new Stop");   
        }
    }
}