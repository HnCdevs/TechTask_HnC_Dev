using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList();
        T GetItem(int id);
        void Create(T item);
        void Update(int id, T item);
        void Delete(int id);
    }
}
