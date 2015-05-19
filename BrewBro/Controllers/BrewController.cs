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

        public BrewHistory Post (BrewHistory historyItem)
        {
            return _BusinessLayer.InsertHistory(historyItem);
        }
    }
}
