using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(int id);
        IEnumerable<TEntity> GetAll();
        Task Remove(TEntity obj);
        Task Update(TEntity obj);
        void Dispose();
    }
}
