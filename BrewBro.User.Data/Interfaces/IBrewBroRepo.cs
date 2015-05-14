using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Data.Interfaces
{
    public interface IBrewBroRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        T FindById(Guid Id);
    }
}
