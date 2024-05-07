using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Extensions;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Blogs
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [BindProperty]
        public Blog Blog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _blogRepository.GetByIdAsync(id.Value);

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

            var blog = await _blogRepository.GetByIdAsync(id.Value);

            if (blog == null)
            {
                return NotFound();
            }

            if (blog.AuthorId != User.GetUserId())
            {
                return Forbid();
            }

            Blog = blog;
            await _blogRepository.DeleteAsync(Blog);

            return RedirectToPage("./Index");
        }
    }
}
