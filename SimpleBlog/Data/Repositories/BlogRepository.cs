using Microsoft.EntityFrameworkCore;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Models;
using SimpleBlog.Utilities;

namespace SimpleBlog.Data.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(SimpleBlogContext dbContext)
            : base(dbContext) { }

        public async Task<PaginatedList<Blog>> GetPagedListAsync(int pageIndex, int pageSize)
        {
            return await PaginatedList<Blog>.CreateAsync(
                _context.Blogs.Include(b => b.Author).AsNoTracking(),
                pageIndex,
                pageSize
            );
        }
    }
}
