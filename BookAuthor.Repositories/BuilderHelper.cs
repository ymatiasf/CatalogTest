using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BookAuthor.CORE;
using BookAuthor.DL;

namespace BookAuthor.Repositories
{
    public static class IoCContainerHelper
    {

        public static ContainerBuilder BuilderHelper(this ContainerBuilder builder)
        {
            builder.Register<IDataContextFactory<BookContext>>(x => new DefaultDataContextFactory<BookContext>()).InstancePerLifetimeScope();

            builder.Register<IAuthorRepository>(x => new AuthorRepository(x.Resolve<IDataContextFactory<BookContext>>())).SingleInstance();
            builder.Register<IBookRepository>(x => new BookRepository(x.Resolve<IDataContextFactory<BookContext>>())).SingleInstance();

            builder.Register<IUnitOfWork>(x => new UnitOfWork<BookContext>(x.Resolve<IDataContextFactory<BookContext>>())).SingleInstance();

            return builder;
        }
    }
}
