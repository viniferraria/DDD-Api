using System.Collections.Generic;
using System.IO;

namespace Infra.Data
{
    public class FileReader<T> where T : class
    {
        private string _path;

        public FileReader(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileLoadException();
            }
            this._path = path;
        }

        public string[] readFile()
        {
            return File.ReadAllLines(this._path);
        }
    }
}
