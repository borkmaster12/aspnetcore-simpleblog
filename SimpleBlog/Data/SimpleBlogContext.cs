using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Data
{
    public class SimpleBlogContext : DbContext
    {
        public SimpleBlogContext(DbContextOptions<SimpleBlogContext> options)
            : base(options) { }

        public DbSet<SimpleBlog.Models.Blog> Blogs { get; set; } = default!;
    }
}
