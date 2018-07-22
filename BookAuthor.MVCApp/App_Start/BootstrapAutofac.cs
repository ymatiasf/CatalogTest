using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BookAuthor.Repositories;

namespace BookAuthor.MVCApp.App_Start
{
    public class BootstrapAutofac
    {
        public IContainer myContainer { get; private set; }

        public void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;



            OnConfigure(builder);

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();


            if (this.myContainer == null)
            {
                this.myContainer = builder.Build();

            }
            else
            {
                builder.Update(this.myContainer);
            }
            config.DependencyResolver = new AutofacWebApiDependencyResolver(myContainer);
            //This tells the MVC application to use myContainer as its dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(this.myContainer));
        }


        protected virtual void OnConfigure(ContainerBuilder builder)
        {
            //This is where you register all dependencies

            //The line below tells autofac, when a controller is initialized, pass into its constructor, the implementations of the required interfaces
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(typeof(BookRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
            //The line below tells autofac, everytime an implementation IDAL is needed, pass in an instance of the class DAL
            //builder.RegisterType<DAL>().As<IDAL>().InstancePerLifetimeScope();         
            builder.BuilderHelper();
        }
    }
}