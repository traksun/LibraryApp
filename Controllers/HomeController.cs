using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.BookCount = await _context.Books.CountAsync();
            ViewBag.AuthorCount = await _context.Authors.CountAsync();
            ViewBag.CategoryCount = await _context.Categories.CountAsync();
            ViewBag.RecentBooks = await _context.Books
                .Include(b => b.Author)
                .OrderByDescending(b => b.BookId)
                .Take(4)
                .ToListAsync();
            return View();
        }
    }
}
