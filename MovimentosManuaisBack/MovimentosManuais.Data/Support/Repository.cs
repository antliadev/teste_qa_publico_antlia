using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MovimentosManuais.Data.Support
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly DataContext Context;

        public Repository(DataContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public void DeleteCommit(Func<T, bool> predicate)
        {
            Context.Set<T>()
                   .Where(predicate).ToList()
                   .ForEach(del => Context.Entry(del).State = EntityState.Deleted);

            Context.SaveChanges();
        }

        public void Delete(Func<T, bool> predicate)
        {
            Context.Set<T>()
                   .Where(predicate).ToList()
                   .ForEach(del => Context.Entry(del).State = EntityState.Deleted);
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public T Find(params object[] key)
        {
            return Context.Set<T>().Find(key);
        }

        public T Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            IQueryable<T> result;

            if (string.IsNullOrEmpty(paths.First().Name))
            {
                result = Context.Set<T>().Where(predicate);
            }
            else
            {
                result = Context.Set<T>().Include(paths.First()).Where(predicate);

                foreach (var path in paths.Skip(1))
                    result = result.Include(path).Where(predicate);
            }

            return result.FirstOrDefault();
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetNoTracking(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().AsNoTracking().Where(predicate);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            if (paths.First() == null)
            {
                var result = Context.Set<T>().Where(predicate);

                return result.ToList();
            }
            else
            {
                var result = Context.Set<T>().Include(paths.First()).Where(predicate);

                foreach (var path in paths.Skip(1))
                    result = result.Include(path).Where(predicate);

                return result.ToList();
            }
        }

        public IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            if (paths.First() == null)
            {
                var result = Context.Set<T>().AsNoTracking().Where(predicate);

                return result.ToList();
            }
            else
            {
                var result = Context.Set<T>().AsNoTracking().Include(paths.First()).Where(predicate);

                foreach (var path in paths.Skip(1))
                    result = result.Include(path).Where(predicate);

                return result.ToList();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params string[] paths)
        {
            if (string.IsNullOrEmpty(paths.First()))
            {
                var result = Context.Set<T>().Where(predicate);

                return result.ToList();
            }
            else
            {
                var result = Context.Set<T>().Include(paths.First()).Where(predicate);

                foreach (var path in paths.Skip(1))
                    result = result.Include(path).Where(predicate);

                return result.ToList();
            }
        }

        public IEnumerable<T> GetNoTracking(Expression<Func<T, bool>> predicate, params string[] paths)
        {
            if (string.IsNullOrEmpty(paths.First()))
            {
                var result = Context.Set<T>().AsNoTracking().Where(predicate);

                return result.ToList();
            }
            else
            {
                var result = Context.Set<T>().AsNoTracking().Include(paths.First()).Where(predicate);

                foreach (var path in paths.Skip(1))
                    result = result.Include(path).Where(predicate);

                return result.ToList();
            }
        }

        public IEnumerable<T> GetBy(Func<T, bool> predicate, params string[] paths)
        {
            if (string.IsNullOrEmpty(paths.First()))
            {
                var result = Context.Set<T>().Where(predicate);

                return result.ToList();
            }
            else
            {
                var result = Context.Set<T>().Include(paths.First());

                foreach (var path in paths.Skip(1))
                    result = result.Include(path);

                return result.Where(predicate).ToList();
            }
        }

        public IEnumerable<T> GetByNoTracking(Func<T, bool> predicate, params string[] paths)
        {
            if (string.IsNullOrEmpty(paths.First()))
            {
                var result = Context.Set<T>().AsNoTracking().Where(predicate);

                return result.ToList();
            }
            else
            {
                var result = Context.Set<T>().AsNoTracking().Include(paths.First());

                foreach (var path in paths.Skip(1))
                    result = result.Include(path);

                return result.Where(predicate).ToList();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAllNoTracking()
        {
            return Context.Set<T>().AsNoTracking().ToList();
        }

        public IEnumerable<T> GetAll(params string[] paths)
        {
            if (string.IsNullOrEmpty(paths.First()))
            {
                return Context.Set<T>().ToList();
            }
            else
            {
                var result = Context.Set<T>().Include(paths.First());

                foreach (var path in paths.Skip(1))
                    result = result.Include(path);

                return result.ToList();
            }
        }

        public IEnumerable<T> GetAllNoTracking(params string[] paths)
        {
            if (string.IsNullOrEmpty(paths.First()))
            {
                return Context.Set<T>().AsNoTracking().ToList();
            }
            else
            {
                var result = Context.Set<T>().AsNoTracking().Include(paths.First());

                foreach (var path in paths.Skip(1))
                    result = result.Include(path);

                return result.ToList();
            }
        }

        public IEnumerable<T> GetAllOrderBy(Func<T, object> keySelector)
        {
            return Context.Set<T>().OrderBy(keySelector).ToList();
        }

        public IEnumerable<T> GetAllOrderByNoTracking(Func<T, object> keySelector)
        {
            return Context.Set<T>().AsNoTracking().OrderBy(keySelector).ToList();
        }

        public IEnumerable<T> GetAllOrderByDescending(Func<T, object> keySelector)
        {
            return Context.Set<T>().OrderByDescending(keySelector).ToList();
        }

        public IEnumerable<T> GetAllOrderByDescendingNoTracking(Func<T, object> keySelector)
        {
            return Context.Set<T>().AsNoTracking().OrderByDescending(keySelector).ToList();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
        }

        public void AddRange(List<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public void BulkInsert(List<T> entities)
        {
            Context.Set<T>().BulkInsert(entities);
        }

        public void ExecuteSqlCommand(string strCommand, object[] parameters)
        {
            Context.Database.ExecuteSqlCommand(strCommand, parameters);
        }
    }
}