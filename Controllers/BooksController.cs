using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .OrderBy(b => b.Title)
                .ToListAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null) return NotFound();
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "Name");
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Year,Description,AuthorId")] Book book, int[] selectedCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                foreach (var catId in selectedCategories)
                {
                    _context.BookCategories.Add(new BookCategory { BookId = book.BookId, CategoryId = catId });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.Categories = _context.Categories.ToList();
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null) return NotFound();

            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SelectedCategories = book.BookCategories.Select(bc => bc.CategoryId).ToList();
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Year,Description,AuthorId")] Book book, int[] selectedCategories)
        {
            if (id != book.BookId) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(book);

                // Update categories
                var existing = _context.BookCategories.Where(bc => bc.BookId == id);
                _context.BookCategories.RemoveRange(existing);
                foreach (var catId in selectedCategories)
                {
                    _context.BookCategories.Add(new BookCategory { BookId = id, CategoryId = catId });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Authors = new SelectList(_context.Authors, "AuthorId", "Name", book.AuthorId);
            ViewBag.Categories = _context.Categories.ToList();
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null) return NotFound();
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCategories = _context.BookCategories.Where(bc => bc.BookId == id);
            _context.BookCategories.RemoveRange(bookCategories);

            var book = await _context.Books.FindAsync(id);
            if (book != null) _context.Books.Remove(book);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
