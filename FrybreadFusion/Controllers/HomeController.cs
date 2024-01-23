using FrybreadFusion.Data.Repositories;
using FrybreadFusion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrybreadFusion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Navigated to Home Index");
            return View("~/Views/Home/Index.cshtml");
        }


        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult References()
        {
            return View();
        }

        public IActionResult Blog()
        {
            // Let's redirect to BlogController's Index action.
            _logger.LogInformation("Redirecting to Blog");
            return RedirectToAction("Index", "Blog");
        }

        public IActionResult Author()
        {
            return View();
        }

        public IActionResult Tradition()
        { 
          
            return View();
        }
        public IActionResult Quiz()
        {

            return View("Quiz/Index");
        }
        public IActionResult Privacy()
        {

            return View();
        }
    }
}