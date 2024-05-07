using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Extensions;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Blogs
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly SimpleBlog.Data.SimpleBlogContext _context;

        public DeleteModel(SimpleBlog.Data.SimpleBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            if (blog.AuthorId != User!.GetUserId())
            {
                return Forbid();
            }

            Blog = blog;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            if (blog.AuthorId != User.GetUserId())
            {
                return Forbid();
            }

            Blog = blog;
            _context.Blogs.Remove(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
