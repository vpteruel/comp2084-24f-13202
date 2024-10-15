using System.ComponentModel.DataAnnotations;

namespace ContactsManagerApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        public string? CreatedAt { get; set; }

        public string? ModifiedAt { get; set; }
    }
}
