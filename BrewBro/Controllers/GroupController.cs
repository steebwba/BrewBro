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
        [Route("api/User/{userId}/Group/{searchText=}")]
        public List<Group> Get(string searchText)
        {
            return _BAL.Search(searchText);
        }

        [HttpGet]
        public Group Get(Guid id)
        {
            Group group = _BAL.Load(id);
            if(group == null)
            {
                //TODO log exception
                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Unable to find any results") };
                throw new HttpResponseException(response);
            }
            return group;
        }

        public Group Post(Group group)
        {
            _BAL.Save(group);

            //Return the saved group with populatedb Id. This will be a new Guid if a new group is created, or the existing group Id if editing
            return group;
        }

    }
}
