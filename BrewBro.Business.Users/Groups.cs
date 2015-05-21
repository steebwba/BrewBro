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

        /// <summary>
        /// Initializes a new instance of the <see cref="Groups"/> class.
        /// </summary>
        public Groups()
        {
            _Repo = new GroupsRepository();
            _UserBAL = new Users();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Groups"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        /// <param name="userBAL">The user bal.</param>
        public Groups(IRepository<Group> repo, Users userBAL)
        {
            _Repo = repo;
            _UserBAL = userBAL;
        }

        /// <summary>
        /// Saves the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
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

        /// <summary>
        /// Searches groups using the specified search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public List<Group> Search(string searchText)
        {
            Expression<Func<Group, bool>> filter = (u => u.Name.ToLower().StartsWith((searchText ?? string.Empty).ToLower()));

            return _Repo.Query(filter).ToList();
        }

        /// <summary>
        /// Loads the specified group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Group Load(Guid id)
        {
            Group retVal = _Repo.FindById(id);

            retVal.Users = _UserBAL.Load(retVal.Users.Select(u => u.Id));

            return retVal;
        }

        /// <summary>
        /// Finds the groups a user user belongs to.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public List<Group> FindByUser(Guid userId)
        {
            var groups = _Repo.Query(g => g.Users.Any(u => u.Id == userId));

            //Remove the users in the group, as we only want to display the basic data for a group when searching by user
            groups.AsParallel().ForAll(g => g.Users.Clear());

            return groups.ToList();
        }


        /// <summary>
        /// Removes the user from a specified group.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void RemoveUser(Guid groupId, Guid userId)
        {
            //TODO Logic around if user to remove is the owner
            Group group = _Repo.FindById(groupId);

            if (!group.Users.Any(u => u.Id == userId))
            {
                throw new ArgumentException(string.Format("User {0} does not belong to group {1}", userId, groupId));
            }

            group.Users.RemoveAll(u => u.Id == userId);

            //Removed the user, and now there aren't any users left. so remove the group
            if (group.Users.Count == 0)
            {
                _Repo.Delete(groupId);
            }
            else
            {
                //user is the owner, assign the owner role to another user
                if (group.Owner.Id == userId)
                {
                    group.Owner.Id = group.Users.First().Id;
                }

                _Repo.Update(group);
            }
            
        }

        /// <summary>
        /// Deletes the specified group
        /// </summary>
        /// <param name="id">The group identifier.</param>
        public void Delete(Guid id)
        {
            _Repo.Delete(id);
        }
    }
}
