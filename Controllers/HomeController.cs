using System.Diagnostics;
using Elfie.Serialization;
using Humanizer.Localisation;
using KhoHang_XNK.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;


namespace KhoHang_XNK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult RedirectBasedOnRole()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "TongQuan");
            }
            else
            {
                return RedirectToAction("IndexUser", "TongQuan");
            }
        }

        public IActionResult Index()
        {
            
            return View();
        }
      
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
     
    } 
}
