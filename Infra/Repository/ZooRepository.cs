using Domain.Models;
using Infra.Data.Repository;
using Domain.Interfaces;
using Infra.Data;

namespace Infra.Repository
{
    public class ZooRepository : RepositoryBase<Zoo>, IZooRepository
    {

       public string readFile(string filename)
        {
            Db.fromFile(filename);
            return "File Read";
        }
    }
}
