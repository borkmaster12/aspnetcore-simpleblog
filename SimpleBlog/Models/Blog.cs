using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string Content { get; set; } = default!;

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastUpdatedDate { get; set; }
    }
}
