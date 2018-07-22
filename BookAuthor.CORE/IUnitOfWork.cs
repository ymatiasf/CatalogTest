namespace BookAuthor.CORE
{
    /// <summary>
    /// Interface IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        void Dispose();
    }
}
