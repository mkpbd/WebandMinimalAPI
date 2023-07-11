namespace EmedicianApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderNo { get; set; }
        public string? OrderStatus { get; set; }
        public decimal OrderTotal { get; set; }


    }
}
