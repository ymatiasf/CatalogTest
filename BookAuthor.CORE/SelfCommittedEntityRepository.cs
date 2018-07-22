using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookAuthor.CORE
{
    /// <summary>
    /// Class SelfCommittedEntityRepository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext">contexto.</typeparam>
    public class SelfCommittedEntityRepository<T, TContext> : EntityRepository<T, TContext>
        where T : class
        where TContext : class, IDbContext, IDisposable
    {
        /// <summary>
        /// The _dbset
        /// </summary>
        private readonly IDbSet<T> _dbset;

        /// <summary>
        /// Inicializa una nueva instancia de  <see cref="SelfCommittedEntityRepository{T, TContext}"/> class.
        /// </summary>
        /// <param name="databaseFactory">The database factory.</param>
        public SelfCommittedEntityRepository(IDataContextFactory<TContext> databaseFactory)
            : base(databaseFactory)
        {
            _dbset = DataContext.Set<T>();
        }

        /// <summary>
        /// añade una entidad especifica
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Add(T entity)
        {
            _dbset.Add(entity);
            SaveChanges();
        }

        /// <summary>
        /// añade entidades al contexto.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public override void Add(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _dbset.Add(entity);
            }
            SaveChanges();
        }

        /// <summary>
        ///Actualiza la entidad.
        /// </summary>
        /// <param name="entity">entidad.</param>
        public override void Update(T entity)
        {
            _dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        /// <summary>
        /// borra la entidad.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Delete(T entity)
        {
            _dbset.Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// borra la entidad bajo criterio .
        /// </summary>
        /// <param name="where">The where.</param>
        public override void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = Enumerable.AsEnumerable<T>(_dbset.Where(@where));
            foreach (T obj in objects)
                _dbset.Remove(obj);
            SaveChanges();
        }

        /// <summary>
        /// Guarda los cambios.
        /// </summary>
        private void SaveChanges()
        {
            DataContext.SaveChanges();
        }
    }
}