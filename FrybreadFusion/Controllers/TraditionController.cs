using Microsoft.AspNetCore.Mvc;

namespace FrybreadFusion.Controllers
{
    public class TraditionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
