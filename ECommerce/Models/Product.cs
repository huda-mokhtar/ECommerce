using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Price { get; set; }
        [RegularExpression("^100$|^\\d{0,8}(\\.\\d{1,2})? *%?$", ErrorMessage = "Please enter Presentage or number.")]
        public string Discount { get; set; }
    }
}
