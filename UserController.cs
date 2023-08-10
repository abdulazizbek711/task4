using Microsoft.AspNetCore.Mvc;
using project4.Data;
using project4.Models;
using project4.Models.ViewModels;
using BCrypt.Net;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace project4.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.Find(_ => true).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> BlockUser(string id)
        {
            var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null) return NotFound();

            user.Status = UserStatus.Blocked;
            await _context.Users.ReplaceOneAsync(u => u.Id == id, user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnblockUser(string id)
        {
            var user = await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null) return NotFound();

            user.Status = UserStatus.Active;
            await _context.Users.ReplaceOneAsync(u => u.Id == id, user);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);


                // Create a new user with hashed password and active status
                var newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    Status = UserStatus.Active
                };

                await _context.Users.InsertOneAsync(newUser);

                return RedirectToAction("Login", "Account"); // Redirect to login or another page
            }

            // If registration fails, show errors
            return View(model);
        }

        // Other actions...
    }
}




