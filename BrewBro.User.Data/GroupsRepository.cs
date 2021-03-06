﻿using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Entities;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Data
{
    //TODO Error logging
    public class GroupsRepository : IRepository<Group>
    {
        MongoClient _Repo = new MongoClient(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        IMongoDatabase _Database;
        IMongoCollection<Group> _Collection;

        public GroupsRepository()
        {
            _Database = _Repo.GetDatabase("BrewBro");
            _Collection = _Database.GetCollection<Group>("Group");
        }

        public IList<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Group> Query(Expression<Func<Group, bool>> filter)
        {
            return _Collection.Find(filter).ToListAsync().Result;
        }

        public void Add(Group item)
        {
            item.Id = Guid.NewGuid();
            Task.WaitAll(_Collection.InsertOneAsync(item));
        }

        public void Delete(Guid id)
        {
            Task.WaitAll(_Collection.DeleteOneAsync(u => u.Id == id));
        }

        public void Update(Group item)
        {
            Task.WaitAll(_Collection.ReplaceOneAsync(x => x.Id == item.Id, item));
        }

        public Group FindById(Guid Id)
        {
            return _Collection.Find(u => u.Id == Id).FirstAsync().Result;
        }
    }
}
