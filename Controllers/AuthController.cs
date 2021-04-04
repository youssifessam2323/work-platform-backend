using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using work_platform_backend.Dtos;
using work_platform_backend.Services;

using System;
using System.Linq;
using work_platform_backend.validation;
using work_platform_backend.Dtos.Auth;

namespace work_platform_backend.Controllers
{

    [ApiController]
    [AllowAnonymous]
    [Route("/api/v1/auth")]
    [ValidateModel]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly UserService userService;

        public AuthController(AuthService authService, UserService userService = null)
        {
            this.authService = authService;
            this.userService = userService;
        }


        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if(!ModelState.IsValid)
            {   
                var errors = (from item in ModelState.Values
                                    from error in item.Errors
                                    select error.ErrorMessage).ToList();


                var response = new AuthenticationResponse
                {
                    Message = "One or more validation errors occurred.",
                    Errors = errors
                };
            }
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
                Console.WriteLine("Iaam Hitted Broh...");
            if(result.IsSuccess)
            {
                return Ok("Email Verfifed") ; 
            }

            return BadRequest();
        }

         //api/auth/confirmemail?userid&token
        [HttpGet]
        [Route("password/change")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> ChangePassword([FromBody] PasswordChangeRequest passwordRequest)
        {
            
          
            return Ok(await authService
                    .ChangePassword
                    (
                        userService.GetUserId()
                        ,passwordRequest.CurrentPassword
                        ,passwordRequest.NewPassword
                    ));
        }
    









    }
}