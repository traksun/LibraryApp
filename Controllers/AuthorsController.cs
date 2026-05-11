using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .OrderBy(a => a.Name)
                .ToListAsync();
            return View(authors);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var author = await _context.Authors
                .Include(a => a.Books)
                    .ThenInclude(b => b.BookCategories)
                        .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.AuthorId == id);

            if (author == null) return NotFound();
            return View(author);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Bio")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,Name,Bio")] Author author)
        {
            if (id != author.AuthorId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null) _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
