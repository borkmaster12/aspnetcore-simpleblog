using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly SimpleBlog.Data.SimpleBlogContext _context;

        public IndexModel(SimpleBlog.Data.SimpleBlogContext context)
        {
            _context = context;
        }

        public IList<OutputModel> Blogs { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Blogs = await _context
                .Blogs.Take(10)
                .Select(blog => new OutputModel
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    AuthorName = blog.Author!.UserName ?? string.Empty,
                    CreatedDate = blog.CreatedDate,
                    LastUpdatedDate = blog.LastUpdatedDate,
                })
                .ToListAsync();
        }

        public class OutputModel
        {
            public int Id { get; set; }

            [Display(Name = "Author")]
            public string AuthorName { get; set; } = default!;

            [Required]
            public string Title { get; set; } = default!;

            [Display(Name = "Created")]
            public DateTimeOffset CreatedDate { get; set; }

            [Display(Name = "Updated")]
            public DateTimeOffset? LastUpdatedDate { get; set; }
        }
    }
}
