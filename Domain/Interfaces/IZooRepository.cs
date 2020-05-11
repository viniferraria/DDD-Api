using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IZooRepository : IRepositoryBase<Zoo>
    {
        new Task<int> Add(Zoo obj);

        string readFile(string filename);
    }
}