using BrewBro.Users.Business;
using BrewBro.Users.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrewBro.Controllers
{
    public class GroupController : ApiController
    {
        Groups _BAL = new Groups();

        [HttpGet]
        public List<Group> Get(string searchText)
        {
            return _BAL.Search(searchText);
        }

        [HttpGet]
        public Group Get(Guid id)
        {
            return _BAL.Load(id);
        }

        public Group Post(Group group)
        {
            _BAL.Save(group);

            //Return the saved group with populatedb Id. This will be a new Guid if a new group is created, or the existing group Id if editing
            return group;
        }

    }
}
