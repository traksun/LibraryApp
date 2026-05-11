using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Категория")]
        public string Name { get; set; } = "";

        public List<BookCategory> BookCategories { get; set; } = new();
    }
}
