using System.Collections.Generic;
using System.Threading.Tasks;

namespace work_platform_backend.Repos
{
    // will implement this later
    public interface IRepository<T> where T :class
    {
           Task<T> GetById(int id);
           Task Save(T obj);
           Task UpdateById(int id , T obj);
           Task DeleteById(int id);
    }
}