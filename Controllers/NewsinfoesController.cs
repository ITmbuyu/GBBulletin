using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GBBulletin.Data;
using GBBulletin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using GBBulletin.ViewModels;

namespace GBBulletin.Controllers
{
    public class NewsinfoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public NewsinfoesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> UserManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = UserManager;
        }

        // GET: Newsinfoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Newsinfo.Include(n => n.NewsGenre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Newsinfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //select all the genres from the database and pass it to the HomeViewModel
            var genres = _context.NewsGenres.ToList();




            if (id == null)
            {
                return NotFound();
            }

            var newsinfo = await _context.Newsinfo
                .Include(n => n.NewsGenre)
                .FirstOrDefaultAsync(m => m.NewsinfoId == id);
            if (newsinfo == null)
            {
                return NotFound();
            }

            //pass all the data to the NewsDetailsViewModel
            NewsDetailsViewModel newsViewModel = new NewsDetailsViewModel()
            {
                NewsGenres = genres,
                Newsinfo = newsinfo
                
            };

            return View(newsViewModel);
        }

        // GET: Newsinfoes/Create
        public IActionResult Create()
        {
            ViewData["NewsGenreId"] = new SelectList(_context.NewsGenres, "NewsGenreId", "Genre");
            return View();
        }

        // POST: Newsinfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsinfoId,Title,Description,ArticlePicture,Newsarticle,Author,NewsGenreId")] Newsinfo newsinfo, IFormFile ArticlePicture)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            newsinfo.Author = _userManager.GetUserName(User);
            // Check if an image was uploaded
            if (ArticlePicture != null && ArticlePicture.Length > 0)
            {
                // Generate a unique file name for the image (you can customize this logic)
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ArticlePicture.FileName;

                // Define the path to save the image in the wwwroot/uploads folder
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Create the uploads folder if it doesn't exist
                Directory.CreateDirectory(uploadsFolder);

                // Save the uploaded image to the file system
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ArticlePicture.CopyToAsync(stream);
                }

                // Save the image file path to the database
                newsinfo.ArticlePicture = "/uploads/" + uniqueFileName; // Relative path to the image

                //var user = await _userManager.GetUserAsync(User);
                //var email = user.Email;

                //bool isAdmin = currentUser.IsInRole("Admin");
                //var userId = _userManager.GetUserId(User); // Get user ID
            }

            if (ModelState.IsValid)
            {
                
                _context.Add(newsinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log model state errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                // Check if error is not null before accessing its properties
                if (error != null)
                {
                    // Log or print the error message
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }

            ViewData["NewsGenreId"] = new SelectList(_context.NewsGenres, "NewsGenreId", "Genre", newsinfo.NewsGenreId);
            return View(newsinfo);
        }

        // GET: Newsinfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsinfo = await _context.Newsinfo.FindAsync(id);
            if (newsinfo == null)
            {
                return NotFound();
            }
            ViewData["NewsGenreId"] = new SelectList(_context.NewsGenres, "NewsGenreId", "NewsGenreId", newsinfo.NewsGenreId);
            return View(newsinfo);
        }

        // POST: Newsinfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsinfoId,Title,Description,ArticlePicture,Newsarticle,Author,NewsGenreId")] Newsinfo newsinfo)
        {
            if (id != newsinfo.NewsinfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsinfoExists(newsinfo.NewsinfoId))
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
            ViewData["NewsGenreId"] = new SelectList(_context.NewsGenres, "NewsGenreId", "NewsGenreId", newsinfo.NewsGenreId);
            return View(newsinfo);
        }

        // GET: Newsinfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsinfo = await _context.Newsinfo
                .Include(n => n.NewsGenre)
                .FirstOrDefaultAsync(m => m.NewsinfoId == id);
            if (newsinfo == null)
            {
                return NotFound();
            }

            return View(newsinfo);
        }

        // POST: Newsinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsinfo = await _context.Newsinfo.FindAsync(id);
            if (newsinfo != null)
            {
                _context.Newsinfo.Remove(newsinfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsinfoExists(int id)
        {
            return _context.Newsinfo.Any(e => e.NewsinfoId == id);
        }
    }
}
