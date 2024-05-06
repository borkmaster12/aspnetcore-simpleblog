using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly SimpleBlog.Data.SimpleBlogContext _context;

        public IndexModel(SimpleBlog.Data.SimpleBlogContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Blog = await _context.Blogs.ToListAsync();
        }
    }
}
