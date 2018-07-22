using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Data.IsolationLevel;


namespace BookAuthor.CORE
{
    public class UnitOfWork<TContext> : UnitOfWorkBase<TContext> where TContext : class ,IDbContext, IDisposable, new()
    {

        public UnitOfWork(IDataContextFactory<TContext> databaseFactory)
            : base(databaseFactory)
        {

        }

        public override void Commit()
        {
            if (DataContext != null)
            {
                //using (var scope = new TransactionScope(TransactionScopeOption.Required,
                //new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                //{
                bool saveFailed;
                var count = 0;
                do
                {
                    saveFailed = false;

                    try
                    {
                        
                            // DataContext.Entry(this).State = EntityState.Modified;
                            DataContext.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        // Update the values of the entity that failed to save from the store 
                        ex.Entries.Single().Reload();
                        count++;
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }

                        }
                        saveFailed = true;
                        count++;
                    }
                   
                    if (saveFailed && count == 512)
                    {
                        break;
                    }
                } while (saveFailed && count <= 512);

                //    scope.Complete();
                //}

            }
        }

        //public override async void Commit()
        //{
        //    await CommiDatat();
        //}

        //private async Task CommiDatat()
        //{
        //    await Task.Run(() =>
        //    {
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required,
        //                               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
        //        {
        //            DataContext.SaveChanges();
        //            scope.Complete();
        //        }
        //    });
        //}

    }
}
