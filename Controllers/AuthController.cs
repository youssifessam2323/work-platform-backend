using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using work_platform_backend.Repos;
using work_platform_backend.Dtos;
using work_platform_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Exceptions;
using System;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [AllowAnonymous]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly AuthService authService;
        public AuthController(IUserRepository userRepository, AuthService authService)
        {
            this.userRepository = userRepository;
            this.authService = authService ; 
        }


        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
            await authService.SignUp(registerDto);
            }catch(Exception ex)
            {
                return BadRequest(new {Error = ex.InnerException.ToString()});
            }
             return Ok("Registered Successfully");
        }



        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> Login([FromBody]LoginDto loginDto)
        {
              AuthenticationResponse authenticationResponse = await authService.Signin(loginDto);
                
                if(authenticationResponse == null )
                {
                    return Unauthorized("Bad Credentials");
                } 

                return Ok(authenticationResponse);
        }



        [HttpGet]
        [Route("{token}")]
        public async Task<ActionResult> verifyEmail(string token)
        {
            await authService.VerifyEmail(token);
            return Ok("Email Verfifed") ; 
        }










    }
}