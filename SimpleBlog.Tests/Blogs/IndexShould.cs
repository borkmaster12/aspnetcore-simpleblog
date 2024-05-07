using Moq;
using SimpleBlog.Contracts.Persistence;
using SimpleBlog.Models;
using SimpleBlog.Pages.Blogs;
using SimpleBlog.Utilities;

namespace SimpleBlog.Tests.Blogs
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public async Task PopulateBlogsOnGet()
        {
            var blogs = new List<Blog>()
            {
                new Blog()
                {
                    Id = 1,
                    Title = "Test",
                    AuthorId = 1,
                    Content = "Test",
                    CreatedDate = DateTimeOffset.Now,
                    Author = new BlogUser() { UserName = "Test" }
                },
                new Blog()
                {
                    Id = 2,
                    Title = "Test",
                    AuthorId = 1,
                    Content = "Test",
                    CreatedDate = DateTimeOffset.Now,
                    Author = new BlogUser() { UserName = "Test" }
                }
            };

            var pageIndex = 1;
            var pageSize = 10;

            var pagedBlogs = () => new PaginatedList<Blog>(blogs, blogs.Count, pageIndex, pageSize);

            var mockBlogRepository = new Mock<IBlogRepository>();

            mockBlogRepository
                .Setup(r => r.GetPagedListAsync(pageIndex, pageSize))
                .ReturnsAsync(pagedBlogs());

            var indexModel = new IndexModel(mockBlogRepository.Object);

            await indexModel.OnGetAsync(pageIndex);

            Assert.That(indexModel.Blogs, Is.Not.Null.And.Count.EqualTo(2));
            mockBlogRepository.Verify(r => r.GetPagedListAsync(pageIndex, pageSize), Times.Once);
        }
    }
}
