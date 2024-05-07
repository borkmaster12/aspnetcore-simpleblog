using SimpleBlog.Models;
using SimpleBlog.Utilities;

namespace SimpleBlog.Contracts.Persistence
{
    public interface IBlogRepository : IAsyncRepository<Blog>
    {
        Task<PaginatedList<Blog>> GetPagedListAsync(int pageIndex, int pageSize);
    }
}
