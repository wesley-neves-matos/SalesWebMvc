using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class Sellers : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
