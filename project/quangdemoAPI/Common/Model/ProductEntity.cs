namespace Common.Model
{
    public class ProductEntity 
    {
        public Guid? product_id { get; set; }
        public string? product_name { get; set; }
        public int? product_price { get; set; }
        public int? product_quantity { get; set; }
        public string? img_name { get; set; }
        public int? product_status { get; set; }
    }
}
