using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LoginAndReg.Models;
namespace LoginAndReg.Controllers;
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext db;
    public UserController(ILogger<UserController> logger, MyContext context)    
    {
        _logger = logger;
        db = context;
    }
    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {        
        if(ModelState.IsValid)
        {
            // Initializing a PasswordHasher object, providing our User class as its type
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            // Updating our newUser's password to a hashed version
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            //Save your user object to the database 
            Console.WriteLine("Adding user to database");
            db.Add(newUser);
            db.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Dashboard", "Home");
        } else {
            return View("../Home/Index");
        }
    }

    [HttpPost("users/login")]
    public IActionResult Login(LoginUser userSubmission)
    {    
        if(ModelState.IsValid)    
        {        
            // If initial ModelState is valid, query for a user with the provided email        
            User? userInDb = db.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);        
            // If no user exists with the provided email        
            if(userInDb == null)        
            {            
                // Add an error to ModelState and return to View!            
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");            
                return View("../Home/Index");        
            }   
            // Otherwise, we have a user, now we need to check their password                 
            // Initialize hasher object        
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();                    
            // Verify provided password against hash stored in db        
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
            // Result can be compared to 0 for failure        
            if(result == 0)        
            {            
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");            
                // Handle failure (this should be similar to how "existing email" is handled)        
                return View("../Home/Index");
            } 
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return RedirectToAction("Dashboard", "Home");
        } else {
            return View("../Home/Index");
        }
    }

    [HttpGet("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}