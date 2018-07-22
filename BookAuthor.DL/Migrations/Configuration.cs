using System.Collections.Generic;

namespace BookAuthor.DL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            base.Seed(context);
            context.Books.Add(new Book()
            {
                
                Title = "Test",
                EditionDate = DateTime.Now,
                Authors = new List<Author>
                {
                    new Author() {  Name = "Garcia Marquez" },
                    new Author() {  Name = "Daniel Keyes" },
                    new Author() {  Name = "Tolstoi" },
                    new Author() {  Name = "Issac Asimov" },
                    new Author() {  Name = "Lord Byron" },
                    new Author() {  Name = "Edgar Allan Poe" },
                    new Author() {  Name = "Jean Paul Sartre" }
                }
            });
           

            context.SaveChanges();
        }
    }
}
