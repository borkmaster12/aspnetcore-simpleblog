using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;

namespace SimpleBlog.Data
{
    public class SimpleBlogContext : IdentityDbContext<BlogUser, IdentityRole<int>, int>
    {
        public SimpleBlogContext(DbContextOptions<SimpleBlogContext> options)
            : base(options) { }

        public DbSet<SimpleBlog.Models.Blog> Blogs { get; set; } = default!;
    }
}
