using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Models;

namespace SimpleBlog.Data.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(SimpleBlogContext dbContext)
            : base(dbContext) { }
    }
}
