using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class HistoryDeliveryDTO
    {
        public Guid? historydelivery_id { get; set; }
        public Guid? user_id { get; set; }
        public Guid? address_id { get; set; }
        public int? price { get; set; }
        public Guid? product_id { get; set; }
        public int? quantity { get; set; }
        public string? product_name { get; set; }
        public string? img_name { get; set; }
        public string? status { get; set; }
        public string? address_fullName { get; set; }
        public string? address_phoneNumber { get; set; }
        public string? address_province { get; set; }
        public string? address_district { get; set; }
        public string? address_commune { get; set; }
        public string? address_village { get; set; }
        public string? address_apartment { get; set; }
        public DateTime delivery_date { get; set; }
    }
}
