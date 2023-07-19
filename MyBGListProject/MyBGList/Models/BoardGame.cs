﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBGList.Models
{
    [Table("BoardGames")]
    public class BoardGame
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public int? Year { get; set; }

        [Required]
        public int MinPlayers { get; set; }
        [Required]
        public int MaxPlayers { get; set; }
        [Required]
        public int PlayTime { get; set; }
        [Required]
        public int MinAge { get; set; }
        [Required]
        public int UsersRated { get; set; }
        [Required]
        [Precision(4, 2)]
        public decimal RatingAverage { get; set; }
        [Required]
        public int BGGRank { get; set; }
        [Required]
        [Precision(4, 2)]
        public decimal ComplexityAverage { get; set; }

        [Required]
        public int OwnedUsers { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }

        // composition or relationShip
        public ICollection<BoardGamesDomains>? BoardGames_Domains { get; set; }
        public ICollection<BoardGamesMechanics>? BoardGames_Mechanics { get; set; }
    }
}
