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
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<Customer> _customerService;

        public BillsController( IGenericService<ProductItem> productItemService, IGenericService<Bill> billService, IGenericService<Product> productService, IGenericService<Customer> customerService)
        {
            _productItemService = productItemService;
            _billService = billService;
            _productService = productService;
            _customerService = customerService;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            return View(_billService.GetAll());
        }

        //Post:/CheackOut
        [HttpPost]
        public IActionResult CheackOut(CheckOutView Check)
        {
            Bill bill = new Bill() { };
            bill.Date = Check.Date;
            bill.Discount = Check.Discount.ToString();
            bill.customerId = Check.CustomerId;
            _billService.Add(bill);
            foreach (var i in Check.Products)
            {
                ProductItem productItem = new ProductItem() { };
                productItem.BillId = bill.Id;
                productItem.ProductId = i.Product.Id;
                productItem.Quantity = i.Count;
                _productItemService.Add(productItem);

                Product oldproduct=_productService.GetById(i.Product.Id);
                oldproduct.Quantity -= i.Count;
                _productService.Update(oldproduct);
            }
            return Ok(Check.Products);
        }
        
        public async Task<IActionResult> AddInVoice()
        {
            ViewData["customerId"] = new SelectList(_customerService.GetAll(), "Id", "Name");
            ViewData["productId"] = new SelectList(_productService.GetAll(), "Id", "Name");
            return View();
        }
        [HttpPost]
         public async Task<IActionResult> SaveInvoice(Bill bill)
         {
            _billService.Add(bill);

            //foreach(var i in bill.ProductItems)
            //{
            //     i.BillId = bill.Id;
            //    _productItemService.Add(i);
            //}
            return Ok();
         }
        public async Task<IActionResult> Details(int id)
        {
            var bill = _billService.GetById(id);
            if (bill == null)
            {
                return NotFound();
            }
            decimal total=0;
            foreach (var i in bill.ProductItems)
            {
                foreach (var item in _productService.GetAll())
                {
                    if (item.Id == i.ProductId)
                    {
                        total += (item.Price - (decimal.Parse(item.Discount) + decimal.Parse(i.Discount)))*i.Quantity;
                    }
                }
            }
            total = total - decimal.Parse(bill.Discount);
            ViewData["Total"] = total.ToString();
            ViewData["products"] = _productService.GetAll();
            return View(bill);
        }

    }
}
