using MinimalAPIBasic.Models;

namespace MinimalAPIBasic.Data
{
    public static class CouponStore
    {
        public static List<Coupon> Coupons { get; set; } = new List<Coupon>() {
            new Coupon()
            {
                 Id = 1,
                 IsActive = true,
                 Name = "100FF"

            },
            new Coupon()
            {
                 Id= 2,
                 IsActive = false,
                 Name = "200FF"
            }

        };


    }
}
