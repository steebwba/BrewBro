using BrewBro.Users.Data;
using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BrewBro.Users.Business
{
    public class Groups
    {
        IRepository<Group> _Repo;
        Users _UserBAL;

        public Groups()
        {
            _Repo = new GroupsRepository();
            _UserBAL = new Users();
        }

        public Groups(IRepository<Group> repo, IRepository<User> userRepo)
        {
            _Repo = repo;
            _UserBAL = new Users(userRepo);
        }

        public void Save(Group group)
        {
            if (group.Id == default(Guid))
            {
                _Repo.Add(group);
            }
            else
            {
                _Repo.Update(group);
            }
        }

        public List<Group> Search(string searchText)
        {
            Expression<Func<Group, bool>> filter;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                filter = (u => !u.Deleted);
            }
            else
            {
                filter = (u => u.Name.ToLower().StartsWith(searchText.ToLower()) && !u.Deleted);
            }
            return _Repo.Query(filter).ToList();
        }

        public Group Load(Guid id)
        {
            Group retVal = _Repo.FindById(id);

            retVal.Users = _UserBAL.Load(retVal.Users.Select(u => u.Id));

            return retVal;
        }
    }
}
