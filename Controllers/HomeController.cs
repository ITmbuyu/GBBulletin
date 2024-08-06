using GBBulletin.Data;
using GBBulletin.Models;
using GBBulletin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GBBulletin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //select all the genres from the database and pass it to the HomeViewModel
            var genres = _context.NewsGenres.ToList();
            
            var newsinfos = _context.Newsinfo.Include(n => n.NewsGenre).ToList();

            // select all the news from newsinfo table where BreakingNews is true
            var breakingNews = _context.Newsinfo.Include(m => m.NewsGenre).Where(x => x.BreakingNews == true).ToList();

            // select all the news from newsinfo table where TrendingNow is true
            var trendingNow = _context.Newsinfo.Include(m => m.NewsGenre).Where(x => x.TrendingNow == true).ToList();

            // select all the news from newsinfo table where PickOfMonth is true
            var pickOfMonth = _context.Newsinfo.Include(m => m.NewsGenre).Where(x => x.PickOfMonth == true).ToList();

            // select all the news from newsinfo table where EditorsPick is true
            var editorsPick = _context.Newsinfo.Include(m => m.NewsGenre).Where(x => x.EditorsPick == true).ToList();

            //select all the news from newsinfo table grouped by genre
            var newsByGenre = _context.Newsinfo.Include(m => m.NewsGenre).GroupBy(m => m.NewsGenre.Genre).ToList();

            //pass all the data to the HomeViewModel
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                NewsGenres = genres,
                Newsinfos = newsinfos,
                BreakingNews = breakingNews,
                TrendingNow = trendingNow,
                PickOfMonth = pickOfMonth,
                EditorsPick = editorsPick,
                NewsByGenre = newsByGenre
            };





            return View(homeViewModel);
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
