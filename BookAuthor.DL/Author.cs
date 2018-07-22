using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookAuthor.DL
{
    public class Author
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}