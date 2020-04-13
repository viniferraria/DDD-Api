using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Repository;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ZooRepository : RepositoryBase<Zoo>, IZooRepository
    {

        public async Task<int> Add(Zoo obj)
        {
            await base.Add(obj);
            return obj.Id;
            
        }

        public string readFile(string filename)
        {
            Db.fromFile(filename);
            return "Procedure executado";
        }
    }
}
