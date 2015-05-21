using BrewBro.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IList<T> GetAll();

        IList<T> Query(Expression<Func<T, bool>> filter);
        void Add(T item);
        void Delete(Guid id);
        void Update(T item);
        T FindById(Guid Id);
    }
}
