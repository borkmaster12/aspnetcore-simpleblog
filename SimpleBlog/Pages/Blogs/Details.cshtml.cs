using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;

        public DetailsModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

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
            else
            {
                Blog = blog;
            }
            return Page();
        }
    }
}
