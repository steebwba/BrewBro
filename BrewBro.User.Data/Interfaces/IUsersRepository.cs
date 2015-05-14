using BrewBro.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Data.Interfaces
{
    public interface IUsersRepository<T> : IBrewBroRepo<T> where T : User
    {
        List<User> Search(string searchText);

        User GetByEmail(string email);
    }
}
