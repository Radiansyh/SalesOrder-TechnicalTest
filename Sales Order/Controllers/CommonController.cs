using Microsoft.AspNetCore.Mvc;

namespace Sales_Order.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
