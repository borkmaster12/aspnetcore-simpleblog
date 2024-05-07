using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Extensions;

namespace SimpleBlog.Pages.Blogs
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly SimpleBlog.Data.SimpleBlogContext _context;

        public EditModel(SimpleBlog.Data.SimpleBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public OutputModel Output { get; set; } = default!;

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

            if (blog.AuthorId != User.GetUserId())
            {
                return Forbid();
            }

            Output = new OutputModel
            {
                Id = blog.Id,
                CreatedDate = blog.CreatedDate,
                LastUpdatedDate = blog.LastUpdatedDate,
            };

            Input = new InputModel { Title = blog.Title, Content = blog.Content };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
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

            if (blog.AuthorId != User.GetUserId())
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            blog.Title = Input!.Title;
            blog.Content = Input!.Content;
            blog.LastUpdatedDate = DateTimeOffset.Now;

            _context.Attach(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(blog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }

        public class InputModel
        {
            [Required]
            public string Title { get; set; } = default!;

            [Required]
            public string Content { get; set; } = default!;
        }

        public class OutputModel
        {
            public int Id { get; set; }

            [Display(Name = "Created")]
            public DateTimeOffset CreatedDate { get; set; }

            [Display(Name = "Updated")]
            public DateTimeOffset? LastUpdatedDate { get; set; }
        }
    }
}
