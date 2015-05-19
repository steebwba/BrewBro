using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Business = BrewBro.Users.Business;
using BrewBro.Users.Entities;
using System.Web;
using System.Threading;

namespace BrewBro.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post(User user)
        {
            User authenticatedUser = new Business.Users().Authenticate(user.Email, user.Password);

            if (authenticatedUser != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { Id = authenticatedUser.Id, Token = Guid.NewGuid() });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Login");
            }
        }
    }
}
