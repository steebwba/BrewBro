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
            if (group == null)
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

        /// <summary>
        /// Deletes the specified user that belongs to the group.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        [Route("api/Group/{groupId}/User/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid groupId, Guid id)
        {
            try
            {
                _BAL.RemoveUser(groupId, id);
            }
            catch(ArgumentException argEx)
            {
                //TODO log error 
                return new HttpResponseMessage(HttpStatusCode.Conflict) { Content = new StringContent(argEx.Message) };
            }
            catch (Exception)
            {
                //TODO log error 
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
           

            return new HttpResponseMessage(HttpStatusCode.NoContent) { Content = new StringContent("User Deleted") };
        }

        /// <summary>
        /// Deletes the specified group.
        /// </summary>
        /// <param name="id">The group identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            _BAL.RemoveUser(id);

            return new HttpResponseMessage(HttpStatusCode.NoContent) { Content = new StringContent("Group Deleted") };
        }

    }
}
