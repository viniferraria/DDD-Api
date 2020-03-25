using System.Collections.Generic;



namespace Domain.Interfaces
{
    public interface IFileRepository<T> where T : class
    {
        string [] getAll();
    }
}
