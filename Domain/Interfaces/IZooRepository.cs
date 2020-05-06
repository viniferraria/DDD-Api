using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IZooRepository : IRepositoryBase<Zoo>
    {
        Task<int> Add(Zoo obj);

        string readFile(string filename);
    }
}