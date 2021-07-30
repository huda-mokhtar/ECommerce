using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    public class ProductItemsController : Controller
    {
        private readonly IGenericService<ProductItem> _productItemService;
        private readonly IGenericService<Bill> _billService;

        public ProductItemsController(IGenericService<ProductItem> prodectService, IGenericService<Bill> billService)
        {
            _productItemService = prodectService;
            _billService = billService;
        }
       
    }
}
