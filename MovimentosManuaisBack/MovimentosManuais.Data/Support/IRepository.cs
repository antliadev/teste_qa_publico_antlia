using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MovimentosManuais.Data.Support
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Commit();
        void Delete(T entity);
        void Delete(List<T> entities);
        void DeleteCommit(Func<T, bool> predicate);
        void Delete(Func<T, bool> predicate);
        void Dispose();
        T Find(params object[] key);
        T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        T First(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params string[] paths);
        IEnumerable<T> GetBy(Func<T, bool> predicate, params string[] paths);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllOrderBy(Func<T, object> keySelector);
        IEnumerable<T> GetAllOrderByDescending(Func<T, object> keySelector);
        void Update(T entity);
        void Insert(T entity);
        void AddRange(List<T> entities);
        void BulkInsert(List<T> entities);
        IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] paths);
        IEnumerable<T> GetByNoTracking(Func<T, bool> predicate, params string[] paths);
        IEnumerable<T> GetAllNoTracking();
        IEnumerable<T> GetAllNoTracking(params string[] paths);
        IEnumerable<T> GetAllOrderByNoTracking(Func<T, object> keySelector);
        IEnumerable<T> GetAllOrderByDescendingNoTracking(Func<T, object> keySelector);
    }
}