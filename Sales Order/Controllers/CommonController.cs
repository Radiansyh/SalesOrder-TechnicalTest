using Microsoft.AspNetCore.Mvc;
using SalesOrder.Web.Models;
using SalesOrder.Infrastructure.Repository.Customer;

namespace SalesOrder.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly IComCustomerRepository _comCustomerRepository;

        public CommonController(IComCustomerRepository comCustomerRepository)
        {
            _comCustomerRepository = comCustomerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var cust = await _comCustomerRepository.GetAllAsync();
            var model = new ComCustomerModel
            {
                ListCustomers = cust
            };

            return View(model);
        }
    }
}
