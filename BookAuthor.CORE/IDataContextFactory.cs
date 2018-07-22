using System;

namespace BookAuthor.CORE
{
    /// <summary>
    /// Interface IDataContextFactory
    /// </summary>
    /// <typeparam name="TContext">The type of the t context.</typeparam>
    public interface IDataContextFactory<out TContext> : IDisposable where TContext : IDisposable
    {
        new void Dispose();
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns>TContext.</returns>
        TContext GetContext();
    }
}
