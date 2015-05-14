using BrewBro.Users.Entities;
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
        [HttpGet]
        public IEnumerable<Group> LoadAllGroups()
        {
            return new List<Group>()
            {
                new Group(){
                    Id = Guid.NewGuid(),
                    Name = "Test Group 1"

                },
                new Group(){
                    Id = Guid.NewGuid(),
                    Name = "Test Group 2"

                }

            };
        }

        public void Post(JObject group)
        {
        }
      
    }
}
