﻿using System.ComponentModel.DataAnnotations;

namespace EmedicianApi.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal UnitPrice { get; set; }  
        public decimal Discount { get; set;}
        public int Quntity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
