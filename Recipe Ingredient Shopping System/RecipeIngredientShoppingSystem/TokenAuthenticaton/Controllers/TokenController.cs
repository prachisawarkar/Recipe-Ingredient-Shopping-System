﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace TokenAuthenticaton.Controllers
{
    public class TokenController : ApiController
    {
        // This resource can accessible for everyone. No restrictions.
        [AllowAnonymous]
        [HttpGet]
        [Route("api/server/info")]
        public IHttpActionResult Get()
        {
            return Ok("Server time is: " + DateTime.Now.ToString());
        }

        // This resource can be accessible for only for admin and user.
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/user/normal")]
        public IHttpActionResult ResourceUser()
        {
            var identity = (ClaimsIdentity)User.Identity;

            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);

            return Ok("Hello " + identity.Name + "; Your role is : " + string.Join(",", roles.ToList()));
        }

        // This resource can be accessible only for admin.
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("api/user/admin")]
        public IHttpActionResult ResourceAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);

            return Ok("Hello " + identity.Name + "; Your role is : " + string.Join(",", roles.ToList()));
        }
    }
}
