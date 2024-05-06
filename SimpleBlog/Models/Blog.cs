using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [ForeignKey(nameof(BlogUser.Id))]
        public int AuthorId { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;

        [Display(Name = "Created")]
        public DateTimeOffset CreatedDate { get; set; }

        [Display(Name = "Updated")]
        public DateTimeOffset? LastUpdatedDate { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public BlogUser? Author { get; set; }
    }
}
