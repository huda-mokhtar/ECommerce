using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Bill
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [RegularExpression("^100$|^\\d{0,8}(\\.\\d{1,2})? *%?$", ErrorMessage = "Please enter Presentage or number.")]
        public string Discount { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<ProductItem> ProductItems { get; set; }
    }
}
