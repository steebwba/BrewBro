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

using BrewBro.Users.Data.Interfaces;
using BrewBro.Users.Data;
using Business = BrewBro.Users.Business;
using BrewBro.Users.Entities;

namespace BrewBro.Controllers
{
    public class UserController : ApiController
    {
        IUsersRepository<User> _Repo = new UsersRepository();

        /// <summary>
        /// Searches users with given parameters
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> Get(string searchText)
        {
            return _Repo.Search(searchText);
        }


        /// <summary>
        /// Adds a new user
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public HttpResponseMessage Post(User user)
        {
            //hash the created password for security
            user.Password = Business.Users.CreateHash(user.Password);

            _Repo.Add(user);

            return Request.CreateResponse(HttpStatusCode.Created, "User Created");
        }

    }
}
