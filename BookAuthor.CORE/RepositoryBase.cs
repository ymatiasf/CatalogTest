using System;

namespace BookAuthor.CORE
{
    /// <summary>
    /// Class RepositoryBase.
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    public abstract class RepositoryBase<TContext> : IDisposable where TContext : class,IDisposable
    {
        /// <summary>
        /// The _data context
        /// </summary>
        private TContext _dataContext;
        /// <summary>
        /// Gets the data context factory.
        /// </summary>
        /// <value>The data context factory.</value>
        protected IDataContextFactory<TContext> DataContextFactory { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DataContextFactory.Dispose();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TContext}"/> class.
        /// </summary>
        /// <param name="dataContextFactory">The data context factory.</param>
        protected RepositoryBase(IDataContextFactory<TContext> dataContextFactory)
        {
            DataContextFactory = dataContextFactory;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        /// <value>The data context.</value>
        protected TContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DataContextFactory.GetContext()); }
        }
    }
}
