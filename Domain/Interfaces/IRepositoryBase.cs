using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Remove(TEntity obj);
        void Update(TEntity obj);
        void Dispose();
    }
}
