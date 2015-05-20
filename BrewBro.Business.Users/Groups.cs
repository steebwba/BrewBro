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

        public Groups(IRepository<Group> repo, Users userBAL)
        {
            _Repo = repo;
            _UserBAL = userBAL;
        }

        public void Save(Group group)
        {
            //TODO Find users added to group and send email (Possibly change to not add to group until an invite is accepted)
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
            Expression<Func<Group, bool>> filter = (u => u.Name.ToLower().StartsWith((searchText ?? string.Empty).ToLower()));

            return _Repo.Query(filter).ToList();
        }

        /// <summary>
        /// Finds the groups a user user belongs to.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<Group> FindByUser(Guid userId)
        {
            var groups = _Repo.Query(g => g.Users.Any(u => u.Id == userId));

            //Remove the users in the group, as we onyl want to display the basic data for a group when searching by user
            groups.AsParallel().ForAll(g => g.Users.Clear());

            return groups.ToList();
        }

        public Group Load(Guid id)
        {
            Group retVal = _Repo.FindById(id);

            retVal.Users = _UserBAL.Load(retVal.Users.Select(u => u.Id));

            return retVal;
        }
    }
}
