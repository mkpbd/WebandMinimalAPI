namespace VillaAPI.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
