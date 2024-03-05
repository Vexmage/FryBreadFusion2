using FrybreadFusion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using System;
using FrybreadFusion.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FrybreadFusion.Data.Repositories;

namespace FrybreadFusion.Controllers
{
    public class BlogController : Controller
    {
        private readonly IRepository<BlogPost> _repository;
        private readonly MyDatabase _context;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IRepository<BlogPost> repository, MyDatabase context, ILogger<BlogController> logger) 
        {
            _repository = repository;
            _context = context; 
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = await _context.BlogPosts
                                          .Include(p => p.Comments)
                                              .ThenInclude(c => c.Replies)
                                          .ToListAsync();
                return View(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in BlogController.Index");
                return RedirectToAction("Error", "Home");
            }
        }


        public IActionResult Author()
        {

            return View();
        }

        public IActionResult Post()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(BlogPost newPost)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(newPost);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(newPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int blogPostId, string userName, string userComment)
        {
            var comment = new Comment
            {
                BlogPostId = blogPostId,
                UserName = userName,
                UserComment = userComment,
                DatePosted = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = blogPostId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReply(int commentId, string userName, string text)
        {
            var reply = new Reply
            {
                CommentId = commentId,
                UserName = userName,
                Text = text,
                DatePosted = DateTime.Now
            };
            
            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();
            
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null) return NotFound();
            return RedirectToAction(nameof(Details), new { id = comment.BlogPostId });
        }

        public async Task<IActionResult> Details(int id)
        {
            // This ensures you load the blog post along with its comments and replies.
            var blogPost = await _context.BlogPosts
                                         .Include(bp => bp.Comments)
                                             .ThenInclude(c => c.Replies)
                                         .FirstOrDefaultAsync(bp => bp.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }


    }
}
