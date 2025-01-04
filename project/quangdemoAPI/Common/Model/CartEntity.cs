
namespace Common.Model
{
    public class CartEntity 
    {
        public Guid cart_id { get; set; }
        public Guid product_id { get; set; }
        public Guid user_id { get; set; }
        public int quantity { get; set; } = 1;
    }
}
