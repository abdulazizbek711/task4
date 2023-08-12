using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project4.Data;
using project4.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using project4.Models.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace YourProjectName.Controllers
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
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> BlockUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Status = UserStatus.Blocked;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UnblockUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Status = UserStatus.Active;
            await _context.SaveChangesAsync();
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
                if (model.Password != null)
                {
                    // Generate a random salt
                    byte[] salt = new byte[128 / 8];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }

                    // Hash the password
                    string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: model.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                    // Create a new user with hashed password and active status
                    var newUser = new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PasswordHash = hashedPassword,
                        Status = UserStatus.Active
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Login", "Account"); // Redirect to login or another page
                }
            }

            // If registration fails, show errors
            return View(model);
        }

        // Other actions...

    }
}




