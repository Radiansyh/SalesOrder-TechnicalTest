using Microsoft.AspNetCore.Mvc;

namespace Sales_Order.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
