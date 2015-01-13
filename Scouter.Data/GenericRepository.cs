using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Scouter.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DBSet { get; set; }
        protected DbContext Context { get; set; }

        public GenericRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentException("An instance of DBContext is required to use this repository.", "context");

            this.Context = context;
            this.DBSet = this.Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.DBSet;
        }

        public T GetById(int id)
        {
            return this.DBSet.Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry dbentry = this.Context.Entry(entity);
            if (dbentry.State != EntityState.Detached)
                dbentry.State = EntityState.Added;
            else
                this.DBSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbEntityEntry dbentry = this.Context.Entry(entity);
            if (dbentry.State == EntityState.Detached)
                this.DBSet.Attach(entity);

            dbentry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbentry = this.Context.Entry(entity);
            if (dbentry.State != EntityState.Deleted)
                dbentry.State = EntityState.Deleted;
            else
            {
                this.DBSet.Attach(entity);
                this.DBSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id);

            if (entity != null)
                this.Delete(entity);
        }

        public void Detach(T entity)
        {
            DbEntityEntry dbentry = this.Context.Entry(entity);
            dbentry.State = EntityState.Detached;
        }
    }
}
