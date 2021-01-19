using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using work_platform_backend.Dtos;
using work_platform_backend.Services;

using System;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [AllowAnonymous]
    [Route("/api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        public AuthController(AuthService authService)
        {
            this.authService = authService ; 
        }


        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            AuthenticationResponse authenticationResponse = await authService.SignUp(registerRequest);
            
            if(!authenticationResponse.IsSuccess)
            {
                return BadRequest(authenticationResponse);
            }

            return Ok(authenticationResponse);
        }



        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            if(ModelState.IsValid)
            {

            }
              var response = await authService.Signin(loginRequest);

              if(response.IsSuccess)
              {
                return Ok(response);
              }

              return Unauthorized(response);
        }



        //api/auth/confirmemail?userid&token
        [HttpGet]
        [Route("confirmemail")]
        public async Task<ActionResult> verifyEmail(string userId,string token)
        {
            var result = await authService.VerifyEmail(userId,token);

            if(result.IsSuccess)
            {
                return Ok("Email Verfifed") ; 
            }

            return BadRequest();
        }










    }
}