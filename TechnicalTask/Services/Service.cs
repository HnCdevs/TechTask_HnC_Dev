using System.Collections.Generic;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class Service<T> : IService<T> where T : class, IModel 
    {
        private readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<T> GetList()
        {
            return _repository.GetList();
        }

        public virtual T GetItem(int id)
        {
            return _repository.GetItem(id);
        }

        public virtual void Create(T item)
        {
            _repository.Create(item);
        }

        public virtual void Update(int id, T item)
        {
            var entry = GetItem(id);
            _repository.Update(entry, item);
        }

        public virtual void Delete(int id)
        {
            var item = GetItem(id);
            _repository.Delete(item);
        }

        public virtual bool IsValid(T item)
        {
            return true;
        }
    }
}
