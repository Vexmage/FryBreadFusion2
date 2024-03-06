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
                return View("Error"); // Or your error handling strategy
            }
        }

    }
}
