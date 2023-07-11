namespace EmedicianApi.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }

        public List<User>? Users { get; set; }
        public User? SingleUser { get; set; }
        public List<Medicine>? Medicines { get; set; }
        public Medicine? SingleMedicine { get; set; }

        public List<Cart> Carts { get; set; }
        public Cart? SingleCart { get; set; }
        public List<Order> Orders { get; set; }
        public Order? SingleOrder { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderItem? SingleOrderItem { get; set; }
    }
}
