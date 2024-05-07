using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Utilities;

namespace SimpleBlog.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly SimpleBlog.Data.SimpleBlogContext _context;

        public IndexModel(SimpleBlog.Data.SimpleBlogContext context)
        {
            _context = context;
        }

        public PaginatedList<OutputModel> Blogs { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            var blogsQuery = _context.Blogs.Select(blog => new OutputModel
            {
                Id = blog.Id,
                AuthorId = blog.AuthorId,
                Title = blog.Title,
                AuthorName = blog.Author!.UserName ?? string.Empty,
                CreatedDate = blog.CreatedDate,
                LastUpdatedDate = blog.LastUpdatedDate,
            });

            Blogs = await PaginatedList<OutputModel>.CreateAsync(
                blogsQuery.AsNoTracking(),
                pageIndex ?? 1,
                pageSize: 10
            );
        }

        public class OutputModel
        {
            public int Id { get; set; }

            public int AuthorId { get; set; }

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
