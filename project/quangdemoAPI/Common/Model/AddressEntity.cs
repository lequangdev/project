namespace Common.Model
{
    public class AddressEntity
    {
        public Guid? address_id { get; set; }
        public string? address_fullName { get; set; }
        public string? address_phoneNumber { get; set; }
        public string? address_province { get; set; }
        public string? address_district { get; set; }
        public string? address_commune { get; set; }
        public string? address_village { get; set; }
        public string? address_apartment { get; set; }
        public Guid? user_id { get; set; }
        public int? address_default { get; set; }
    }
}
