using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class CartDTO
    {
        public Guid? cart_id { get; set; }
        public Guid? product_id { get; set; }
        public string? product_name { get; set; }
        public int? product_price { get; set; }
        public int? quantity { get; set; }
        public string? img_name { get; set; }
        public Guid? user_id { get; set; }
        public int? product_quantity { get; set; }
    }
}
