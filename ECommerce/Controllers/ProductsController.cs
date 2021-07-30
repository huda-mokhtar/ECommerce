using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce;
using ECommerce.Models;
using ECommerce.Services;
using System.Globalization;

namespace ECommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IGenericService<Product> _productService;

        public ProductsController(IGenericService<Product> productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductsData()
        {
            return new JsonResult(_productService.GetAll());
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(_productService.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Quantity,Price,CostPrice,Discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.CostPrice > product.Price)
                {
                    ViewBag.Message = "You must set Price greater than Cost.";
                    return View(product);
                }
                else if (product.Discount.Contains("%"))
                {
                    var index = product.Discount.IndexOf("%");
                    var discount = product.Discount.Substring(0,index);
                    if (product.Price - ((decimal.Parse(discount) * product.Price) / 100) < product.CostPrice)
                    {
                        ViewBag.Message2 = "Price after discount  must be greater than or equal cost.";
                        return View(product);
                    }
                }
                else if (product.Price- decimal.Parse(product.Discount) < product.CostPrice)
                {
                    ViewBag.Message2 = "Price after discount  must be greater than or equal cost.";
                    return View(product);
                }
                
                    _productService.Add(product);
                    return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Quantity,Price,CostPrice,Discount")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _productService.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
