using Domain.Interfaces;
using Infra.Data;

namespace Infra.Repository
{
    public class FileRepository<T> : IFileRepository<T> where T : class
    {
        private FileReader<T> _reader;

        public FileRepository(FileReader<T> obj)
        {
            this._reader = obj;
        }

        public string[] getAll()
        {
            return this._reader.readFile();
        }

    }
}
