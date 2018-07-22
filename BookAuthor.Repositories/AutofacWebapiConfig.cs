using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BookAuthor.CORE;
using BookAuthor.DL;

namespace BookAuthor.Repositories
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register<IDataContextFactory<BookContext>>(x => new DefaultDataContextFactory<BookContext>()).InstancePerLifetimeScope();

            builder.Register<IAuthorRepository>(x => new AuthorRepository(x.Resolve<IDataContextFactory<BookContext>>())).SingleInstance();
            builder.Register<IBookRepository>(x => new BookRepository(x.Resolve<IDataContextFactory<BookContext>>())).SingleInstance();

            builder.Register<IUnitOfWork>(x => new UnitOfWork<BookContext>(x.Resolve<IDataContextFactory<BookContext>>())).SingleInstance();


            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}

