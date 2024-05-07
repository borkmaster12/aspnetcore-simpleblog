using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Models;

namespace SimpleBlog.Pages.Blogs
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;

        public CreateModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public Blog? Blog { get; set; }

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

            Blog = new Blog
            {
                AuthorId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value),
                Title = Input.Title,
                Content = Input.Content,
                CreatedDate = DateTimeOffset.Now
            };

            await _blogRepository.AddAsync(Blog);

            return RedirectToPage("./Index");
        }

        public class InputModel
        {
            [Required]
            public string Title { get; set; } = default!;

            [Required]
            public string Content { get; set; } = default!;
        }
    }
}
