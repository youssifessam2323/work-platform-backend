using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Controllers
{
    [ApiController]
    public class SmarterHostTestController : ControllerBase
    {
       



        [HttpGet]
        [Authorize("Admin")]
        private string Hello()
        {
            return "HelloWorld";
        }
    }
}
