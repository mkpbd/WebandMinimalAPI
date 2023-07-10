using VillaAPI.Models.Dto;

namespace VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> VillaList { get; set; } = new List<VillaDto>() {
                new VillaDto()
                {
                     Id = 1,
                     Name = "villa 1",
                     Address =" Gazipur",

                },
                    new VillaDto()
                {
                     Id = 2,
                     Name = "villa 2",
                     Address =" Ranpur",

                },
                        new VillaDto()
                {
                     Id = 3,
                     Name = "villa 3",
                     Address =" dhaka",

                },

            };

    }
}
