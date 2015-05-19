using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Data
{
    public class BrewRepository : IRepository<BrewHistory>
    {
        MongoClient _Repo = new MongoClient(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        IMongoDatabase _Database;
        IMongoCollection<BrewHistory> _Collection;

        public BrewRepository()
        {
            _Database = _Repo.GetDatabase("BrewBro");
            _Collection = _Database.GetCollection<BrewHistory>("BrewHistory");
        }

        public IList<BrewHistory> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<BrewHistory> Query(System.Linq.Expressions.Expression<Func<BrewHistory, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(BrewHistory item)
        {
            item.Id = Guid.NewGuid();
            Task.WaitAll(_Collection.InsertOneAsync(item));
        }

        public void Delete(BrewHistory item)
        {
            throw new NotImplementedException();
        }

        public void Update(BrewHistory item)
        {
            throw new NotImplementedException();
        }

        public BrewHistory FindById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
