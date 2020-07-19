using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorBookPage.Models;

namespace RazorBookPage.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _dbContext.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var bookItem = await _dbContext.Books.FindAsync(id);

            if (bookItem == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(bookItem);

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("Index");

        }
    }
}