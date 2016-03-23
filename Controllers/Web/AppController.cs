using System.Linq;
using Microsoft.AspNet.Mvc;
using MyWorld.Data.Repository;
using MyWorld.Services.Interfaces;
using MyWorld.ViewModels;

namespace MyWorld.Controllers.Web 
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IAppSettingService _appSetting;        
        private readonly IWorldRepository _worldRepository;
        public AppController(IAppSettingService appSetting, IMailService mailService, IWorldRepository worldRepository)
        {
            _mailService = mailService;
            _appSetting = appSetting;
            _worldRepository = worldRepository;
        }
        
        public IActionResult Index()
        {
            var trips = _worldRepository.GetAllTrips().OrderBy(t => t.Name).ToList();
            return View(trips);
        }
        
        public IActionResult About() 
        {
            return View();
        }
        
        public IActionResult Contact() 
        {
            return View();
        }
        
        public IActionResult Particle() 
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                var email = _appSetting.GetAppSetting(Common.Constants.SiteEmailAddress);
                if(string.IsNullOrWhiteSpace(email))
                {
                    // First param (key) is the name of the property, or empty for object level error.
                    ModelState.AddModelError("", "Could not send email, configuration problem.");
                }
            
                if(_mailService.SendMail(email, email, $"Contact Page from {model.Name} ({model.Email})", model.Message))
                {
                    ModelState.Clear(); // Clear the form so we have clean form when sending back the View (in case people click send many times, validation will fire on the clean form.)
                    ViewBag.Message = "Email sent. Thanks!";
                }                
            }
            return View();
        }
    }
}