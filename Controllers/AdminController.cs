using Microsoft.AspNetCore.Mvc;

namespace KhoHang_XNK.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
