using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.ViewModel
{
    public class CheckOutView
    {
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public int CustomerId { get; set; }

        public List<CountProductView> Products { get; set; }
    }
}
