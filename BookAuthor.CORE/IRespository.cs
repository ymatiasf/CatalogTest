using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BookAuthor.CORE
{
    /// <summary>
    /// Interface IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);
        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Add(IEnumerable<T> entities);
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(string id);
        /// <summary>
        /// Deletes the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        void Delete(Expression<Func<T, bool>> @where);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        T GetById(long id);
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        T GetById(string id);
        /// <summary>
        /// Gets the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>T.</returns>
        T Get(Expression<Func<T, bool>> @where);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Gets the many.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> @where);
        /// <summary>
        /// Includes the sub sets.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        IQueryable<T> IncludeSubSets(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Ejecuta Sp´s.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}
