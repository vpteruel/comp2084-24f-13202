using ContactsManagerApp.Entities;
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

        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts
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
                    CreatedAt = contact.CreatedAt.ToLongDateString(),
                    ModifiedAt = contact.ModifiedAt.ToShortDateString()
                })
                .ToListAsync();

            return View(contacts);
        }

        public IActionResult Details()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var categories = await GetCategoriesAsync();

            var contact = new ContactViewModel { Categories = categories };

            return View(nameof(Details), contact); // using the same view for Add/Edit
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var contact = new ContactEntity
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Phone = viewModel.Phone,
                    Email = viewModel.Email,
                    CategoryId = viewModel.CategoryId
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Details), viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categories = await GetCategoriesAsync();

            var contact = await _context.Contacts
                .AsNoTracking()
                .Select(contact => new ContactViewModel
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    CategoryId = contact.CategoryId,
                    Categories = categories
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (contact == null)
                return NotFound();

            return View(nameof(Details), contact); // using the same view for Add/Edit
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var contact = await _context.Contacts.FindAsync(id);
                if (contact == null)
                    return NotFound();

                contact.FirstName = viewModel.FirstName;
                contact.LastName = viewModel.LastName;
                contact.Phone = viewModel.Phone;
                contact.Email = viewModel.Email;
                contact.CategoryId = viewModel.CategoryId;
                contact.ModifiedAt = DateTime.Now;

                try
                {
                    _context.Contacts.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return NotFound();

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(p => p.Id == id);
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync() 
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
                })
                .ToListAsync();
        }
    }
}
