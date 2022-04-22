using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab3_asp.NET.API.Repositories
{
    //Generic interface to implement the repository pattern
    public interface IRepository<T1, T2> where T1 : class
    {
        //async generic method to get all from context
        Task<IEnumerable<T1>> GetAll();

        //async generic method to get generic class entity by id from context
        Task<T1> GetById(T2 id);

        //async generic method to insert new record to the database
        Task<T1> Insert(T1 entity);

        //async generic method to delete record from the database
        Task Delete(T2 id);

        //async generic method to save changes to the context
        Task Save();
    }
}
