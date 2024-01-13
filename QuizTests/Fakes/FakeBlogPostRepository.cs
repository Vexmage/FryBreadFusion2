using FrybreadFusion.Data.Repositories;
using FrybreadFusion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QuizTests.Fakes
{
    public class FakeBlogPostRepository : IRepository<BlogPost>
    {
        private readonly List<BlogPost> _blogPosts = new List<BlogPost>();

        public Task AddAsync(BlogPost entity)
        {
            // Simulate the behavior of a database by generating an ID
            entity.Id = _blogPosts.Any() ? _blogPosts.Max(p => p.Id) + 1 : 1;
            _blogPosts.Add(entity);
            return Task.CompletedTask;
        }

        public void Delete(BlogPost entity)
        {
            _blogPosts.Remove(entity);
        }

        public Task<IEnumerable<BlogPost>> FindAsync(Expression<Func<BlogPost, bool>> predicate)
        {
            var results = _blogPosts.AsQueryable().Where(predicate).ToList();
            return Task.FromResult(results.AsEnumerable());
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return Task.FromResult(_blogPosts.AsEnumerable());
        }

        public Task<BlogPost> GetByIdAsync(int id)
        {
            var blogPost = _blogPosts.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(blogPost);
        }

        public void Update(BlogPost entity)
        {
            var index = _blogPosts.FindIndex(p => p.Id == entity.Id);
            if (index != -1)
                _blogPosts[index] = entity;
        }

        public Task SaveChangesAsync()
        {

            return Task.CompletedTask;
        }
    }
}
