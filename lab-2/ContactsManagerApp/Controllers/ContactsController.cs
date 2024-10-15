using ContactsManagerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsManagerApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contacts = _context.Contacts
                .AsNoTracking()
                .Include(p => p.Category)
                .Select(contact => new ContactViewModel
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    CategoryName = contact.Category != null ? contact.Category.Name : "[no categoty]",
                    CreatedAt = contact.CreatedAt.ToLongDateString()
                })
                .ToList();

            return View(contacts);
        }
    }
}
