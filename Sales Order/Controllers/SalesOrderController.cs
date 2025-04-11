using Microsoft.AspNetCore.Mvc;

namespace Sales_Order.Controllers
{
    public class SalesOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Item()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }
    }
}
