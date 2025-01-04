using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class StaffDTO
    {
        public Guid? staff_id { get; set; }
        public string? staff_dateOfBirth { get; set; }
        public string? staff_identification { get; set; }
        public string? staff_role { get; set; }
        public string? staff_imgUrl { get; set; }
        public Guid? user_id { get; set; }
        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? phone_number { get; set; }
        public string? account { get; set; }
        public string? password { get; set; }
        public string? avatar_url { get; set; }
        public string? user_role { get; set; }
        public Guid? address_id { get; set; }
        public string? address_fullName { get; set; }
        public string? address_phoneNumber { get; set; }
        public string? address_province { get; set; }
        public string? address_district { get; set; }
        public string? address_commune { get; set; }
        public string? address_village { get; set; }
        public string? address_apartment { get; set; }
        public int? address_default { get; set; }
    }
}
