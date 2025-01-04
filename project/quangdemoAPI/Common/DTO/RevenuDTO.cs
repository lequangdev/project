﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class RevenuDTO
    {
        public DateTime delivery_date { get; set; }
        public string? product_name { get; set; }
        public int? quantity { get; set; }
        public string? fullname { get; set; }
        public int? price { get; set; }
        public string? status { get; set; }
    }
}
