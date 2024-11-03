using Gift_Of_The_Givers_Web_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Gift_Of_The_Givers_Web_App.Data;
using Gift_Of_The_Givers_Web_App.Services;
using Gift_Of_The_Givers_Web_App.ViewModels;

namespace Gift_Of_The_Givers_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly GiftOfTheGiversContext _context;
        private readonly IIncidentReportService _incidentReportService;
        private readonly IVolunteerService _volunteerService;
        private readonly IDonationService _donationService;
        private readonly IUserService _userService;
        private UserManager<User> object1;
        private SignInManager<User> object2;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager, GiftOfTheGiversContext context, IIncidentReportService incidentReportService, IVolunteerService volunteerService, IDonationService donationService, IUserService userService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _incidentReportService = incidentReportService;
            _volunteerService = volunteerService;
            _donationService = donationService;
            _userService = userService;

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        // POST: Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Role = model.Role,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);


        }

        // GET: Home/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize] 
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Return the user profile view
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserID.ToString());
                if (user == null)
                {
                    return NotFound();
                }

                user.Username = model.Username;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model); // Return to the view with model errors
        }

        // GET: Home/MakeADifference
        public IActionResult MakeADifference()
        {
            return View();
        }

        [Authorize] // Ensure only authenticated users can access this
        public IActionResult DisasterIncidentReporting()
        {
            return View(new IncidentReport());
        }

        // POST: Submit Incident Report
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitIncidentReport(IncidentReport model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the UserID from claims if not passed in the model
                model.UserID = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                // Create an instance of IncidentReport to save
                var incidentReport = new IncidentReport
                {
                    UserID = model.UserID,
                    Location = model.Location,
                    Description = model.Description,
                    Severity = model.Severity,
                    IncidentDate = model.IncidentDate
                };

                // Call the service to add the incident report
                await _incidentReportService.AddIncidentReportAsync(incidentReport);

                // Redirect to a confirmation page or back to the reporting page
                return RedirectToAction("Index"); // Adjust to your actual action
            }

            // If the model is not valid, return the view with the current model
            return View(model);
        }



        // POST: ResourceDonation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourceDonation(Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.Date = DateTime.Now; // Set current date
                donation.Status = "Pending"; // Set default status
                _context.Donation.Add(donation); 
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to an appropriate action
            }
            return View(donation);
        }

        // Confirmation page after successful donation
        public IActionResult DonationConfirmation()
        {
            return View();
        }



        [Authorize]
        public async Task<IActionResult> VolunteerManagement()
        {
            var availableTasks = await _volunteerService.GetAvailableTasksAsync();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get the logged-in user ID
            var userContributions = await _volunteerService.GetUserContributionsAsync(userId);

            var model = new VolunteerManagementViewModel
            {
                AvailableTasks = availableTasks,
                UserContributions = userContributions
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignUpForTask(int TaskID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get the logged-in user ID
            await _volunteerService.SignUpForTaskAsync(userId, TaskID);

            return RedirectToAction("VolunteerManagement");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterVolunteer(VolunteerRegistration model)
        {
            if (ModelState.IsValid)
            {
                await _volunteerService.RegisterVolunteerAsync(model);
                return RedirectToAction("VolunteerManagement");
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult SecretAction()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
