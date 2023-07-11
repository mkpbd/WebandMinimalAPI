namespace EmedicianApi.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string?  Manufacture { get; set; }
        public decimal UintPrice { get; set; }
        public decimal Discount { get;set; }
        public int Quntity { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int Status { get; set; }
    }
}
