using Domain.Models;


namespace Domain.Interfaces
{
    public interface IZooRepository : IRepositoryBase<Zoo>
    {
        string readFile(string filename);
    }
}
