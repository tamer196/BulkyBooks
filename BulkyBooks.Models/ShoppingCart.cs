using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks.Models
{
    public class ShoppingCart
    {
        public Product product { get; set; }
        [Range(0, 100, ErrorMessage = "Please Enter a valid Value between 1 and 100")]
        public int Count { get; set; }
    }
}
