using FrybreadFusion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FrybreadFusion.Data.Repositories
{
    public class BlogPostRepository : IRepository<BlogPost>
    {
        private readonly MyDatabase _context;

        public BlogPostRepository(MyDatabase context)
        {
            _context = context;
        }

        public async Task<BlogPost> GetByIdAsync(int id) =>
            await _context.BlogPosts.FindAsync(id);

        public async Task<IEnumerable<BlogPost>> GetAllAsync() =>
            await _context.BlogPosts.ToListAsync();

        public async Task<IEnumerable<BlogPost>> FindAsync(Expression<Func<BlogPost, bool>> predicate) =>
            await _context.BlogPosts.Where(predicate).ToListAsync();

        public async Task AddAsync(BlogPost entity)
        {
            await _context.BlogPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(BlogPost entity)
        {
            _context.BlogPosts.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(BlogPost entity)
        {
            _context.BlogPosts.Remove(entity);
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
