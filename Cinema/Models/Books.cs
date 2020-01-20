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
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookAddedTime { get; set; }

        public int PublishYear { get; set; }

        public byte[] CoverImage { get; set; }
    }
}
