using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericService<Product> _prodectService;
        private readonly IGenericService<Customer> _customerService;
        public HomeController(ILogger<HomeController> logger,IGenericService<Product> prodectService, IGenericService<Customer> CustomerService)
        {
            _logger = logger;
            _prodectService = prodectService;
            _customerService = CustomerService;
        }

        public IActionResult Index()
        {
            ViewData["customerId"] = new SelectList(_customerService.GetAll(), "Id", "Name");
            ViewBag.Products = _prodectService.GetAll();
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
