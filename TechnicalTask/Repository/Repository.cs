using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.Data;

namespace TechnicalTask.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TtContext Context;

        public Repository(TtContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetList()
        {
            var list = Context.Set<T>().ToList();
            return list;
        }

        public T GetItem(int id)
        {
            var item = Context.Find<T>(id);
            return item;
        }

        public virtual void Create(T item)
        {
            Context.SetModify(item);
            Context.SaveChanges();
        }

        public virtual void Update(int id, T item)
        {
            var entry = GetItem(id);
            Context.Entry(entry).CurrentValues.SetValues(item);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = GetItem(id);
            Context.Entry(item).State = EntityState.Deleted;
            Context.SaveChanges();
        }

    }
}
