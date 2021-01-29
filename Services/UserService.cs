using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace work_platform_backend.Services
{
    public  class UserService
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public UserService(IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
        }


        public  string GetUserId()
        {
            var UserIdClaim = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (UserIdClaim !=null)
            {
                return UserIdClaim;
            }

            return null;
        }
    }
}
