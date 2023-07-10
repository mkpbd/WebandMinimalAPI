using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage =" Name is required"), MinLength(3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = " Address  is required"), MinLength(10)]
        public string? Address { get; set; }
    }
}
