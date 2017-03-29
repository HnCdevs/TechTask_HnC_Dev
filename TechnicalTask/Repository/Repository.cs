using System.Collections.Generic;
using System.Linq;
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
            Context.SetAdded(item);
            Context.SaveChanges();
        }

        public virtual void Update(int id, T item)
        {
            var entry = GetItem(id);
            Context.SetValues(entry, item);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = GetItem(id);
            Context.SetDeleted(item);
            Context.SaveChanges();
        }

        public virtual bool IsValid(T item)
        {
            return true;
        }
    }
}
