using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce;
using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using ECommerce.Models.ViewModel;
using ECommerce.Services;

namespace ECommerce.Controllers
{
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IGenericService<ProductItem> _productItemService;
        private readonly IGenericService<Bill> _billService;
        public BillsController(ApplicationDbContext context, IGenericService<ProductItem> productItemService, IGenericService<Bill> billService)
        {
            _productItemService = productItemService;
            _billService = billService;
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bills.Include(b => b.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        //Post:/CheackOut
        [HttpPost]
        public IActionResult CheackOut(ProductItemsView Check)
        {

            Bill bill = new Bill() { };
            bill.Date = Check.Date;
            bill.Discount = Check.Discount.ToString();
            bill.customerId = Check.customerId;
            _billService.Add(bill);
            List<ProductItem> productItem = new List<ProductItem>() { };
            foreach (var i in Check.Products)
            {
                foreach (var c in productItem)
                {
                    c.BillId = bill.Id;
                    c.ProductId = i.Product.Id;
                    c.Quantity = i.Count;
                }
            }

            foreach (var i in productItem)
            {
                _productItemService.Add(i);
            }
            return Ok(Check.Products);
        }
        

    }
}
