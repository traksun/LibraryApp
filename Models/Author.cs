using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Име на автора")]
        public string Name { get; set; } = "";

        [Display(Name = "Биография")]
        public string? Bio { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}
