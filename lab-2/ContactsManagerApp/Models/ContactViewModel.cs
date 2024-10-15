using System.ComponentModel.DataAnnotations;

namespace ContactsManagerApp.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Las name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Slug => $"{FirstName}-{LastName}".ToLower();

        public string CreatedAt { get; set; }
    }
}
