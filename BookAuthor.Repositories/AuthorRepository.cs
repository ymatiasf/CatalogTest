using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAuthor.CORE;
using BookAuthor.DL;

namespace BookAuthor.Repositories
{
    public class AuthorRepository : EntityRepository<Author, BookContext>, IAuthorRepository
    {
        public AuthorRepository(IDataContextFactory<BookContext> databaseFactory) : base(databaseFactory)
        {
        }
    }
}
