using BrewBro.Groups.Entities;
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
                    Id = 1,
                    Name = "Test Group 1"

                },
                new Group(){
                    Id = 1,
                    Name = "Test Group 2"

                }

            };
        }
    }
}
