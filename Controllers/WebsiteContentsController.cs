using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GBBulletin.Data;
using GBBulletin.Models;

namespace GBBulletin.Controllers
{
    public class WebsiteContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WebsiteContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WebsiteContents
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebsiteContents.ToListAsync());
        }

        // GET: WebsiteContents/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteContent = await _context.WebsiteContents
                .FirstOrDefaultAsync(m => m.WebsiteContentId == id);
            if (websiteContent == null)
            {
                return NotFound();
            }

            return View(websiteContent);
        }

        // GET: WebsiteContents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebsiteContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebsiteContentId")] WebsiteContent websiteContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(websiteContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(websiteContent);
        }

        // GET: WebsiteContents/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteContent = await _context.WebsiteContents.FindAsync(id);
            if (websiteContent == null)
            {
                return NotFound();
            }
            return View(websiteContent);
        }

        // POST: WebsiteContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WebsiteContentId")] WebsiteContent websiteContent)
        {
            if (id != websiteContent.WebsiteContentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(websiteContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteContentExists(websiteContent.WebsiteContentId))
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
            return View(websiteContent);
        }

        // GET: WebsiteContents/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var websiteContent = await _context.WebsiteContents
                .FirstOrDefaultAsync(m => m.WebsiteContentId == id);
            if (websiteContent == null)
            {
                return NotFound();
            }

            return View(websiteContent);
        }

        // POST: WebsiteContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var websiteContent = await _context.WebsiteContents.FindAsync(id);
            if (websiteContent != null)
            {
                _context.WebsiteContents.Remove(websiteContent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteContentExists(string id)
        {
            return _context.WebsiteContents.Any(e => e.WebsiteContentId == id);
        }
    }
}
