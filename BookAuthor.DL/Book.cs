using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAuthor.DL
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ? EditionDate { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}