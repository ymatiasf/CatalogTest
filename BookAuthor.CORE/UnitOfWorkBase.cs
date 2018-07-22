using System;

namespace BookAuthor.CORE
{
    /// <summary>
    /// Class UnitOfWorkBase.
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    public abstract class UnitOfWorkBase<TContext> : IUnitOfWork, IDisposable where TContext : class ,IDisposable
    {

        /// <summary>
        /// The _data context factory
        /// </summary>
        private readonly IDataContextFactory<TContext> _dataContextFactory;
        /// <summary>
        /// The _data context
        /// </summary>
        private TContext _dataContext;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _dataContextFactory.Dispose();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkBase{TContext}"/> class.
        /// </summary>
        /// <param name="dataContextFactory">The data context factory.</param>
        protected UnitOfWorkBase(IDataContextFactory<TContext> dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        /// <summary>
        /// Gets the data context.
        /// </summary>
        /// <value>The data context.</value>
        protected TContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _dataContextFactory.GetContext()); }
        }

        #region IUnitOfWork Members

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public abstract void Commit();

        #endregion
    }
}
