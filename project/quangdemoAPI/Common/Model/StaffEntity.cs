using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class StaffEntity
    {
        public Guid? staff_id { get; set; }
        public Guid? user_id { get; set; }
        public Guid? address_id { get; set; }
        public string? staff_dateOfBirth { get; set; }
        public string? staff_identification { get; set; }
        public string? staff_role { get; set; }
        public string? staff_imgUrl { get; set; }
    }
}
