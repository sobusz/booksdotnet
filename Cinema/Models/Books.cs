using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Author { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookAddedTime { get; set; }

        [Required]
        public int PublishYear { get; set; }

        public byte[] CoverImage { get; set; }
    }

}
