using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Utilities;

namespace SimpleBlog.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;

        public IndexModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public PaginatedList<OutputModel> Blogs { get; set; } = default!;

        public async Task OnGetAsync(int pageIndex = 1)
        {
            var pageSize = 10;
            var blogs = await _blogRepository.GetPagedListAsync(pageIndex, pageSize);
            var output = blogs.Select(blog => new OutputModel
            {
                Id = blog.Id,
                AuthorId = blog.AuthorId,
                Title = blog.Title,
                AuthorName = blog.Author!.UserName ?? string.Empty,
                CreatedDate = blog.CreatedDate,
                LastUpdatedDate = blog.LastUpdatedDate,
            });

            Blogs = new PaginatedList<OutputModel>(output, blogs.TotalPages, pageIndex, pageSize);
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
