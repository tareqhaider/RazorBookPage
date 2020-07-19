using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBookPage.Models;

namespace RazorBookPage.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGet(int id)
        {
            Book = await _dbContext.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookItem = await _dbContext.Books.FindAsync(Book.Id);

                bookItem.Name = Book.Name;
                bookItem.Author = Book.Author;
                bookItem.ISBN = Book.ISBN;

                await _dbContext.SaveChangesAsync();

                return RedirectToPage("Index");

            }

            return RedirectToPage();
        }
    }
}