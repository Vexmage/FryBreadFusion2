using Xunit;
using FrybreadFusion.Controllers;
using FrybreadFusion.Models;
using QuizTests.Fakes;
using System.Threading.Tasks;

namespace QuizTests
{
    public class BlogControllerTests
    {
        [Fact]
        public async Task Post_AddsNewBlogPost()
        {
            // I will need extensive comments to understand later what I'm doing. 
            // Arrange: Set up a fake repository, controller, and a new blog post.
            var fakeRepository = new FakeBlogPostRepository();
            //var controller = new BlogController(fakeRepository);
            var newPost = new BlogPost
            {
                Title = "Test Title",
                Text = "Test Text",
                Name = "Test Author",
                DatePosted = DateTime.UtcNow
            };

            // Act: Call the Post method on the controller.
            //var result = await controller.Post(newPost);

            // Assert: Verify that the blog post was added to the repository.
            var addedPost = await fakeRepository.GetByIdAsync(newPost.Id);
            Assert.NotNull(addedPost);
            Assert.Equal(newPost.Title, addedPost.Title);
        }

        [Fact]
        public async Task CalculatePostsByAuthor_ReturnsCorrectCount()
        {
            // Arrange: Create a fake repository and add two posts, one by the specified author.
            var fakeRepository = new FakeBlogPostRepository();
            var authorName = "Author";
            var post1 = new BlogPost { Id = 1, Title = "First Post", Name = authorName };
            var post2 = new BlogPost { Id = 2, Title = "Second Post", Name = "Other Author" };
            await fakeRepository.AddAsync(post1);
            await fakeRepository.AddAsync(post2);

            // Act: Retrieve posts by the specified author.
            var result = await fakeRepository.FindAsync(p => p.Name == authorName);

            // Assert: Verify that only one post by the specified author is returned.
            Assert.Single(result); // Only one post by "Author"
        }
    }
}
