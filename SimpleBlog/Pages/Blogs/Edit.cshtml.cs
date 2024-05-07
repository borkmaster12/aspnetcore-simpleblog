using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Extensions;

namespace SimpleBlog.Pages.Blogs
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;

        public EditModel(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
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

            var blog = await _blogRepository.GetByIdAsync(id.Value);
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

            var blog = await _blogRepository.GetByIdAsync(id.Value);

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

            try
            {
                await _blogRepository.UpdateAsync(blog);
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
            return _blogRepository.GetByIdAsync(id) is not null;
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
