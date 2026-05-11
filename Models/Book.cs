using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Заглавието е задължително")]
        [Display(Name = "Заглавие")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Годината е задължителна")]
        [Range(1000, 2100, ErrorMessage = "Невалидна година")]
        [Display(Name = "Година")]
        public int Year { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Изберете автор")]
        [Display(Name = "Автор")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public List<BookCategory> BookCategories { get; set; } = new();
    }
}
