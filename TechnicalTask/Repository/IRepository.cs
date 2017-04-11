using System.Collections.Generic;

namespace TechnicalTask.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList();
        T GetItem(int id);
        void Create(T item);
        void Update(T entry, T item);
        void Delete(T item);
    }
}
