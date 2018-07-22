using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace BookAuthor.CORE
{
    /// <summary>
    /// Repositorio base para entity framework.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext">Ttipo T del contexto.</typeparam>
    public abstract class EntityRepository<T, TContext> : RepositoryBase<TContext>
        where T : class
        where TContext : class, IDbContext, IDisposable
    {
        /// <summary>
        ///  _dbset
        /// </summary>
        private readonly IDbSet<T> _dbset;
        private readonly ObjectResult<T> _objectResult;

        /// <summary>
        /// Inicializa una instancia de  <see cref="EntityRepository{T, TContext}"/> class.
        /// </summary>
        /// <param name="databaseFactory">database factory.</param>
        protected EntityRepository(IDataContextFactory<TContext> databaseFactory) :
            base(databaseFactory)
        {
            
            _dbset = DataContext.Set<T>();
            _objectResult = DataContext.SpObjectResult<T>();
        }

        /// <summary>
        /// Añade una entidad.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        /// <summary>
        ///Añade una coleccion de entidades.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Add(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _dbset.Add(entity);
            }
        }

        /// <summary>
        ///Actualiza la entidad.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Borra la entidad especifica.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        /// <summary>
        ///  Borra la entidad bajo criterio.
        /// </summary>
        /// <param name="where">The where.</param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbset.Where(@where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }
        /// <summary>
        /// Borra la entidad bajo id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(int id)
        {
        }

        /// <summary>
        ///  Borra la entidad bajo id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(string id)
        {
        }

        /// <summary>
        /// Obtiene la entidad(es) bajo criterio 
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>T.</returns>
        public virtual T Get(Expression<Func<T, bool>> @where)
        {
            return _dbset.Where(@where).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene la entidad por  id .
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// Obtiene la entidad por id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// Obtiene todas las entidades
        /// Bajo alerta!! de sobrecarga
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _dbset;
        }

        /// <summary>
        /// Obitne una coleccion de entidades bajo criterio .
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(@where);
        }

        /// <summary>
        /// Obtiene la entidad anidada a sus propiedades.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public virtual IQueryable<T> IncludeSubSets(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_dbset, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// obtiene dinamicamente las entidades.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <typeparam name="TDynamicEntity">The type of the t dynamic entity.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="maker">The maker.</param>
        /// <returns>List&lt;TDynamicEntity&gt;.</returns>
        public List<TDynamicEntity> GetDynamic<TTable, TDynamicEntity>(Expression<Func<TTable, object>> selector, Func<object, TDynamicEntity> maker) where TTable : class
        {
            return DataContext.Set<TTable>().Select(selector.Compile()).Select(maker).ToList();
        }

        /// <summary>
        ///  obtiene dinamicamente las entidades.
        /// </summary>
        /// <typeparam name="TTable">The type of the t table.</typeparam>
        /// <typeparam name="TDynamicEntity">The type of the t dynamic entity.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="maker">The maker.</param>
        /// <returns>List&lt;TDynamicEntity&gt;.</returns>
        public List<TDynamicEntity> GetDynamic<TTable, TDynamicEntity>(Func<TTable, object> selector, Func<object, TDynamicEntity> maker) where TTable : class
        {
            return DataContext.Set<TTable>().Select(selector).Select(maker).ToList();
        }
        /// <summary>
        /// Ejecuta Sp´s.
        /// Ejemplo
        /// IEnumerable<Products> products = 
        //       _unitOfWork.ProductRepository.ExecWithStoreProcedure(
        //       "spGetProducts @bigCategoryId",
        //       new SqlParameter("bigCategoryId", SqlDbType.BigInt) { Value = categoryId } 
        //);
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            
            return DataContext.Set<T>().SqlQuery(query, parameters);
        }
    }
}