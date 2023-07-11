using System.ComponentModel.DataAnnotations;

namespace EmedicianApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        public decimal? Amount { get; set; }
        public string? Type { get; set;}
        public int Status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
    }
}
