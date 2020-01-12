using Repositories.Entities;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IBaseService<T>
    {
        T Get(long id);
        IEnumerable<T> Get(IEnumerable<long> ids);
        void Update(T item);
        T Insert(T item);
        void Delete(long id);
        IEnumerable<T> GetAll();
    }
}
