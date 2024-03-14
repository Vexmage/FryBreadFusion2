using Microsoft.AspNetCore.Mvc;
using FrybreadFusion.Data;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FrybreadFusion.Controllers
{
    public class CommentsController : Controller
    {
        private readonly MyDatabase _context;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(MyDatabase context, ILogger<CommentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> FilteredComments(string userName, DateTime? datePosted)
        {
            try
            {
                var commentsQuery = _context.Comments.AsQueryable();

                if (!string.IsNullOrEmpty(userName))
                {
                    commentsQuery = commentsQuery.Where(c => c.UserName == userName);
                }

                if (datePosted.HasValue)
                {
                    commentsQuery = commentsQuery.Where(c => c.DatePosted.Date == datePosted.Value.Date);
                }

                var filteredComments = await commentsQuery.ToListAsync(); // Modified to use ToListAsync for async operation
                return View(filteredComments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while filtering comments.");
                return View("Error"); 
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Index()
        {
            var comments = await _context.Comments.ToListAsync();
            return View(comments);
        }


    }
}
