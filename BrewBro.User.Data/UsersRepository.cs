using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Data
{
    public class UsersRepository : IRepository<User>
    {
        MongoClient _Repo = new MongoClient(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        IMongoDatabase _Database;
        IMongoCollection<User> _Collection;

        public UsersRepository()
        {
            _Database = _Repo.GetDatabase("BrewBro");
            _Collection = _Database.GetCollection<User>("User");
        }

        public IList<User> GetAll()
        {
            return _Collection.Find(x => x.Id != Guid.Empty).ToListAsync().Result;
        }

        public void Add(User item)
        {
            item.Id = Guid.NewGuid();
            _Collection.InsertOneAsync(item).RunSynchronously();
        }

        public void Delete(User item)
        {
            _Collection.DeleteOneAsync(u => u.Id == item.Id).RunSynchronously();
        }

        public void Update(User item)
        {
            _Collection.ReplaceOneAsync(x => x.Id == item.Id, item).RunSynchronously();
        }

        public User FindById(Guid Id)
        {
            return _Collection.Find(u => u.Id == Id).FirstAsync().Result;
        }

        public IList<User> Query(Expression<Func<User, bool>> filter)
        {
            return _Collection.Find(filter).ToListAsync().Result;
        }
    }
}
