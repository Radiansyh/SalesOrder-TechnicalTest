using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Web.ViewModels;
using SalesOrder.Domain.Entities.SalesOrder;
using SalesOrder.Infrastructure.Repository.Customer;
using SalesOrder.Infrastructure.Repository.SalesOrder;
using SalesOrder.Web.Models;
using SalesOrder.Web.ViewModels;

namespace SalesOrder.Web.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ISoOrderRepository _soOrderRepository;
        private readonly ISoItemRepository _soItemRepository;
        private readonly IComCustomerRepository _comCustomerRepository;

        public SalesOrderController(ISoOrderRepository soOrderRepository,ISoItemRepository soItemRepository, IComCustomerRepository comCustomerRepository)
        {
            _soOrderRepository = soOrderRepository;
            _soItemRepository = soItemRepository;
            _comCustomerRepository = comCustomerRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Order()
        {
            var orders = await _soOrderRepository.GetAllWithCustomerAsync();
            var model = new SoOrderModel
            {
                ListOrders = orders
            };
            return View("OrderList",model);
        }

        public IActionResult Item()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddEditOrder(long? id)
        {
            var customers = await _comCustomerRepository.GetAllAsync();
            var model = new OrderViewModel
            {
                Customers = customers.ToList(),
                IsEditMode = id.HasValue
            };

            if (id.HasValue)
            {
                model.Order = await _soOrderRepository.GetByOrderIdAsync(id.Value);
                model.Items = await _soItemRepository.GetItemsByOrderIdAsync(id.Value);
            }
            else
            {
                model.Order = new SoOrder
                {
                    OrderDate = DateTime.Now
                };
            }
            return View("AddEditOrder", model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(OrderViewModel model)
        {
            ModelState.Remove("Customers");
            ModelState.Remove("Order.Customer");

            var customer = await _comCustomerRepository.GetByIdAsync(model.Order.CustomerId);
            model.Order.Customer = customer;

            if (!ModelState.IsValid )
            {
                var customers = (await _comCustomerRepository.GetAllAsync()).ToList();
                model.Customers = customers;
                return View("AddEditOrder", model);
            }

            if (model.IsEditMode)
            {
                await _soOrderRepository.UpdateAsync(model.Order);
            }
            else
            {
                await _soOrderRepository.AddAsync(model.Order);
            }

            foreach (var item in model.Items)
            {
                item.OrderId = model.Order.OrderId;
                if (item.ItemId == 0)
                {
                    await _soItemRepository.AddAsync(item);
                }
                else
                {
                    await _soItemRepository.UpdateAsync(item);
                }
            }

            return RedirectToAction("Order");
        }

        public async Task<IActionResult> DeleteOrder(long id)
        {
            try
            {
                await _soOrderRepository.DeleteOrderWithItemsAsync(id);
                TempData["SuccessMessage"] = "Order deleted successfully";
                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Failed to delete order: {ex.Message}";
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchOrders(string keywords, DateTime? orderDate)
        {
            var orders = await _soOrderRepository.GetAllWithCustomerAsync();

            if (!string.IsNullOrEmpty(keywords))
            {
                orders = orders.Where(o =>
                    o.OrderNumber.Contains(keywords, StringComparison.OrdinalIgnoreCase) ||
                    o.Customer.CustomerName.Contains(keywords, StringComparison.OrdinalIgnoreCase));
            }

            if (orderDate.HasValue)
            {
                orders = orders.Where(o => o.OrderDate.Date == orderDate.Value.Date);
            }

            var model = new SoOrderModel
            {
                ListOrders = orders.ToList()
            };

            return PartialView("_OrderTablePartial", model);
        }

        public async Task<IActionResult> ExportToExcel(string keyword, DateTime? orderDate)
        {
            var orders = await _soOrderRepository.GetAllWithCustomerAsync();

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                orders = orders.Where(x =>
                    x.OrderNumber.ToLower().Contains(keyword) ||
                    (x.Customer != null && x.Customer.CustomerName.ToLower().Contains(keyword))
                ).ToList();
            }

            if (orderDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate.Date == orderDate.Value.Date).ToList();
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sales Orders");

                worksheet.Cell(1, 1).Value = "No";
                worksheet.Cell(1, 2).Value = "Sales Order";
                worksheet.Cell(1, 3).Value = "Order Date";
                worksheet.Cell(1, 4).Value = "Customer";

                int row = 2;
                int no = 1;
                foreach (var order in orders)
                {
                    worksheet.Cell(row, 1).Value = no++;
                    worksheet.Cell(row, 2).Value = order.OrderNumber;
                    worksheet.Cell(row, 3).Value = order.OrderDate.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 4).Value = order.Customer?.CustomerName;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "SalesOrders.xlsx");
                }
            }
        }

    }
}
