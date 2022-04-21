using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    public interface IRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAll();
        Task<T1> GetSpecified(T2 name);
        Task<T1> Insert(T1 entity);
        Task Delete(T2 name);
        Task Save();
    }
}
