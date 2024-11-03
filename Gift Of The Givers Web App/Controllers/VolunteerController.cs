using Microsoft.AspNetCore.Mvc;
using Gift_Of_The_Givers_Web_App.Models;
using Gift_Of_The_Givers_Web_App.Data;

namespace Gift_Of_The_Givers_Web_App.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly GiftOfTheGiversContext _context;

        public VolunteerController(GiftOfTheGiversContext context)
        {
            _context = context;
        }

        // GET: Volunteer/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Volunteer/Register
        [HttpPost]
        public async Task<IActionResult> Register(VolunteerRegistration volunteerRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.VolunteerRegistration.Add(volunteerRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to a suitable page
            }
            return View(volunteerRegistration);
        }

        // GET: Volunteer/Tasks
        public IActionResult Tasks()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        // POST: Volunteer/SignUpForTask
        [HttpPost]
        public async Task<IActionResult> SignUpForTask(int taskId)
        {
            // Logic to sign up the volunteer for the task
            return RedirectToAction("Tasks");
        }

        // GET: Volunteer/Contributions
        public IActionResult Contributions()
        {
            // Fetch contributions for the logged-in volunteer
            return View();
        }
    }
}
