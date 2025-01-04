using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class HistorydeliveryEntity
    {
        public Guid? historydelivery_id { get; set; }
        public Guid? product_id { get; set; }
        public Guid? address_id { get; set; }
        public string? status { get; set; }
        public int? price { get; set; }
        public int? quantity { get; set; }
        public Guid? user_id { get; set; }
        public DateTime delivery_date { get; set; }
        public Guid? staff_id { get; set; }

    }
}
