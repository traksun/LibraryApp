using Microsoft.EntityFrameworkCore;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite PK for many-to-many
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            // Seed data
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Иван Вазов", Bio = "Патриарх на българската литература" },
                new Author { AuthorId = 2, Name = "Алеко Константинов", Bio = "Основател на туристическото движение в България" },
                new Author { AuthorId = 3, Name = "Христо Ботев", Bio = "Поет, публицист и революционер" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Роман" },
                new Category { CategoryId = 2, Name = "Поезия" },
                new Category { CategoryId = 3, Name = "Разказ" },
                new Category { CategoryId = 4, Name = "Пътепис" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Под игото", Year = 1894, AuthorId = 1, Description = "Роман за борбата на българския народ" },
                new Book { BookId = 2, Title = "Чичовци", Year = 1885, AuthorId = 1, Description = "Сатирична повест" },
                new Book { BookId = 3, Title = "До Чикаго и назад", Year = 1894, AuthorId = 2, Description = "Известният пътепис на Щастливеца" },
                new Book { BookId = 4, Title = "Стихотворения", Year = 1875, AuthorId = 3, Description = "Сборник стихотворения" }
            );

            modelBuilder.Entity<BookCategory>().HasData(
                new BookCategory { BookId = 1, CategoryId = 1 },
                new BookCategory { BookId = 2, CategoryId = 3 },
                new BookCategory { BookId = 3, CategoryId = 4 },
                new BookCategory { BookId = 4, CategoryId = 2 }
            );
        }
    }
}
