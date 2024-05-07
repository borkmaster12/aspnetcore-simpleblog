using SimpleBlog.Models;

namespace SimpleBlog.Contracts.Persistence
{
    public interface IBlogRepository : IAsyncRepository<Blog>
    { }
}
