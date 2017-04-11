using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.Data;

namespace TechnicalTask.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TtContext _context;
        private DbSet<T> _entities;

        private DbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public Repository(TtContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> GetList()
        {
            return Entities;
        }

        public T GetItem(int id)
        {
            return Entities.Find(id);
        }

        public virtual void Create(T item)
        {
            //Entities.Add(item);
            _context.SetAdded(item);
            _context.SaveChanges();
        }

        public virtual void Update(T entry, T item)
        {
            //var entry = GetItem(id);
            _context.SetValues(entry, item);
            _context.SaveChanges();
        }

        public void Delete(T item)
        {
            //var item = GetItem(id);     
            //Entities.Remove(item);
            _context.SetDeleted(item);
            _context.SaveChanges();
        }
    }
}
