using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Blogs
{
    public class CreateModel : PageModel
    {
        private readonly SimpleBlog.Data.SimpleBlogContext _context;

        public CreateModel(SimpleBlog.Data.SimpleBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Blogs.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
