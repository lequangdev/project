namespace Common.Model
{
    public class OrdersEntity : BaseModel
    {
        public Guid? order_id { get; set; }
        public Guid? user_id { get; set; }
        public Guid? address_id { get; set; }
        public int? price { get; set; }
        public Guid? product_id { get; set; }
        public int? quantity { get; set; }
        public string? status { get; set; }
    }
}
