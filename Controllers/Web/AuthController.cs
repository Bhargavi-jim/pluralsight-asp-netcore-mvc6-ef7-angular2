/*
 *  Identity - How they work together: 
 *  SignInManager<type> : Used to clear/ track signin state. Essentially manages the identity user entity. 
 *  UserManager: Used to create an IdentityUser
 *  IdentityUser: User entity used in Identity. Has properties to facilitate authentication like username, password.
 *  IdentityDbContext: Entity Framework to work with Identity.
 *  AddIdentity service: To set up identity for password length, cookie settings .etc
 *  UseIdentity middleware: Specify to use Identity
 *  [Auhorize] attribute: Mark the method to protect.
 *  Run the command dnx ef migrations add IdentityEntities
 */
 
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using MyWorld.Data.Models;
using MyWorld.ViewModels;

namespace MyWorld.Controllers.Web
{
    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> _signInManager;
        
        public AuthController (SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        /*
         * If the user is already logged in, bring the user to the trips page.
         * Otherwise, bring the user to the Login view.
         */
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }
            return View();
        }
        
        /*
         * When user is not already logged in, verify that this is a valid login when the user posts the login information.
         */
         [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, true, false);
                
                if(signInResult.Succeeded)
                {
                    if(string.IsNullOrWhiteSpace(returnUrl))
                    {
                        RedirectToAction("Trips", "App");                        
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View();
        }
        
        public async Task<ActionResult> Logout()
        {   
            if(User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            
            return RedirectToAction("Index", "App");
        }
    }
}