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
        
        private readonly ILogger<BlogController> _logger;

        public BlogController(IRepository<BlogPost> repository, ILogger<BlogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = await _repository.GetAllAsync();
                return View(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in BlogController.Index");
                return RedirectToAction("Error", "Home"); // Redirect to a generic error page
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
    }
}
