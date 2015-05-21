using BrewBro.Users.Business;
using BrewBro.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrewBro.Controllers
{
    public class BrewController : ApiController
    {
        Brews _BusinessLayer = new Brews();

        [HttpPost]
        [Route("api/Group/{groupId}/Brew/{userId}")]
        public BrewHistory Post (Guid groupId, Guid userId)
        {
            return _BusinessLayer.InsertHistory(groupId, userId);
        }

        [Route("api/Group/{groupId}/Brew")]
        [HttpGet]
        public List<BrewHistory> Get(Guid groupId)
        {
            return _BusinessLayer.LoadHistoryByGroup(groupId);
        }
    }
}
