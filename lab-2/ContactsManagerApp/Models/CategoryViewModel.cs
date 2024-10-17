using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        [BindNever]
        public string? Slug => Name.ToLower();
    }
}
