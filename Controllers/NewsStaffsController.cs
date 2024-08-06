using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GBBulletin.Data;
using GBBulletin.Models;
using Microsoft.AspNetCore.Identity;

namespace GBBulletin.Controllers
{
    public class NewsStaffsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsStaffsController(ApplicationDbContext context, UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager, RoleManager<IdentityRole> RoleManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this.RoleManager = RoleManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: NewsStaffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsStaffs.ToListAsync());
        }

        // GET: NewsStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsStaff = await _context.NewsStaffs
                .FirstOrDefaultAsync(m => m.NewsStaffId == id);
            if (newsStaff == null)
            {
                return NotFound();
            }

            return View(newsStaff);
        }

        // GET: NewsStaffs/Create
        public IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            return View();
        }

        // POST: NewsStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsStaffId,NewsStaffName,NewsStaffSurname,staffBio,staffpicture,staffemail,staffpassword,stafftitle,staffrole")] NewsStaff newsStaff, IFormFile staffpicture)
        {
            if (ModelState.IsValid)
            {
                if (staffpicture != null && staffpicture.Length > 0)
                {
                    // Generate a unique file name for the image (you can customize this logic)
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + staffpicture.FileName;

                    // Define the path to save the image in the wwwroot/uploads folder
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Create the uploads folder if it doesn't exist
                    Directory.CreateDirectory(uploadsFolder);

                    // Save the uploaded image to the file system
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await staffpicture.CopyToAsync(stream);
                    }

                    // Save the image file path to the database
                    newsStaff.staffpicture = "/uploads/" + uniqueFileName; // Relative path to the image
                }

                var user = new IdentityUser
                {
                    UserName = newsStaff.NewsStaffName,
                    Email = newsStaff.staffemail,
                    EmailConfirmed = true
                };

                var result = await UserManager.CreateAsync(user, newsStaff.staffpassword);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, newsStaff.staffrole);


                    _context.Add(newsStaff);
                    await _context.SaveChangesAsync();

                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // If creation of the user account fails, add model errors for the failed identity validation errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(newsStaff);
        }

        // GET: NewsStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsStaff = await _context.NewsStaffs.FindAsync(id);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            if (newsStaff == null)
            {
                return NotFound();
            }
            return View(newsStaff);
        }

        // POST: NewsStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsStaffId,NewsStaffName,NewsStaffSurname,staffBio,staffpicture,staffemail,staffpassword,stafftitle,staffrole")] NewsStaff newsStaff, IFormFile staffpicture)
        {
            if (id != newsStaff.NewsStaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (staffpicture != null && staffpicture.Length > 0)
                    {
                        // Generate a unique file name for the image (you can customize this logic)
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + staffpicture.FileName;

                        // Define the path to save the image in the wwwroot/uploads folder
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Create the uploads folder if it doesn't exist
                        Directory.CreateDirectory(uploadsFolder);

                        // Save the uploaded image to the file system
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await staffpicture.CopyToAsync(stream);
                        }

                        // Save the image file path to the database
                        newsStaff.staffpicture = "/uploads/" + uniqueFileName; // Relative path to the image

                        _context.Update(newsStaff);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    //_context.Update(newsStaff);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsStaffExists(newsStaff.NewsStaffId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsStaff);
        }

        // GET: NewsStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsStaff = await _context.NewsStaffs
                .FirstOrDefaultAsync(m => m.NewsStaffId == id);
            if (newsStaff == null)
            {
                return NotFound();
            }

            return View(newsStaff);
        }

        // POST: NewsStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsStaff = await _context.NewsStaffs.FindAsync(id);
            if (newsStaff != null)
            {
                _context.NewsStaffs.Remove(newsStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsStaffExists(int id)
        {
            return _context.NewsStaffs.Any(e => e.NewsStaffId == id);
        }
    }
}
