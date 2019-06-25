using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public interface IRepository<T> where T:class
    {
        Task<T> Get(int Id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T NewTest);
        T Update(T UpdatedTest);
        Task<T> Delete(int Id);
        Task<T> DeleteConfirmed(int id);
        //bool CooperTestExists(int id);
    }
}
