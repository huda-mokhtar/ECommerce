using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModel
{
    public class ProductItemsView
    {
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public int customerId { get; set; }

        public List<CountProductView> Products;
    }
}
