using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Business = BrewBro.Users.Business;
using BrewBro.Users.Entities;

namespace BrewBro.Controllers
{
    public class UserController : ApiController
    {
        Business.Users _BAL = new Business.Users();

        /// <summary>
        /// Searches users with given parameters
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> Get(string searchText)
        {
            return _BAL.Search(searchText);
        }


        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(User user)
        {
            //hash the created password for security
            _BAL.Register(user);

            return Request.CreateResponse(HttpStatusCode.Created, "User Created");
        }

    }
}
