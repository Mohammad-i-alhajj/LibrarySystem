using LibrarySystem.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibrarySystemDbContext _DbContext;

        public HomeController(LibrarySystemDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IActionResult Index()
        {

            ViewBag.TotalBooks = _DbContext.Books.Count();
            ViewBag.TotalAuthors = _DbContext.Authors.Count();
            ViewBag.LatestBook = _DbContext.Books
                                         .OrderByDescending(b => b.CreatedAt)
                                         .Select(b => b.Title)
                                         .FirstOrDefault() ?? "No Books Yet";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
