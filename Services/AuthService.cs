using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using work_platform_backend.Authorization;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.Services
{
    public class AuthService
    {

        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private UserManager<User> userManager;

        public AuthService(IMapper mapper , IConfiguration configuration , IEmailService emailService, UserManager<User> userManager )
        {
            this.mapper = mapper;
            this.configuration = configuration ; 
            this.emailService = emailService ;
            this.userManager = userManager; 
        }
        public async Task<AuthenticationResponse> SignUp(RegisterRequest registerRequest)
        {
             if(registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return new AuthenticationResponse
                {
                    Message = "Confirm Password Does not match the Password",
                    IsSuccess  = false 
                };
            }

            var newUser = mapper.Map<User>(registerRequest);
            var isCreated = await userManager.CreateAsync(newUser,registerRequest.Password);


            if(isCreated.Succeeded)
            {
                var userId = newUser.Id;
                var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser); 
                var validTokenForWeb = createValidTokenForWeb(token);


                await SendConfirmationMail(userId,registerRequest.Email,validTokenForWeb);
                
                return new AuthenticationResponse
                {
                    Message = "User Added Successfully",
                    IsSuccess = true,
                    Errors = new List<string>()
                };

               
            }
            
            return new AuthenticationResponse
            {
                IsSuccess = false,
                Message = "User Didn't Created.",
                Errors = isCreated.Errors.Select(error => error.Description)
            };      
        }

        private string createValidTokenForWeb(string token)
        {
            var encodedToken = Encoding.UTF8.GetBytes(token);
            return WebEncoders.Base64UrlEncode(encodedToken);
        }


        private async Task SendConfirmationMail(string id ,string email,string token)
        {
            string confimationUrl = "http://localhost:5000/api/v1/auth/confirmemail?userid="+id+"&token="+token; 
            Message message 
            = new Message(email, "Email Confirmation","Welcome to our Platform Please Click in the Link to Verify Your Email.", confimationUrl);
           
           
                await emailService.SendEmailAsync(message);
        }

        internal async Task<AuthenticationResponse> VerifyEmail(string userId,string token)
        {
                User user = await userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    return new AuthenticationResponse
                    {
                        IsSuccess = false,
                    }; 
                }
               bool result = await ConfirmEmail(user,token);      

               if(result)
               {
                   return new AuthenticationResponse
                   {
                       IsSuccess = true
                   };
               }        

               return new AuthenticationResponse
               {
                   IsSuccess = false
               } ;          
        }

        private async Task<bool> ConfirmEmail(User user, string token)
        {
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken  = Encoding.UTF8.GetString(decodedToken);

            var result =  await userManager.ConfirmEmailAsync(user,normalToken);
            if(result.Succeeded)
            {
                return true ;
            }

            return false;
        }
        public async Task<AuthenticationResponse> Signin(LoginRequest loginRequest)
        {
            User user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
              
                return new AuthenticationResponse
                {
                    Message = "User is not Exist...",
                    IsSuccess = false
                } ;  
                

            }

            if (!user.EmailConfirmed)
            {
                return new AuthenticationResponse
                {
                    Message = "cred is right but email is not verified..",
                    IsSuccess = false
                };
            }


            var result =await userManager.CheckPasswordAsync(user,loginRequest.Password);

            if(!result)
            {
                return new AuthenticationResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };
            }
             
            
            var token = CreateJWTToken(user); 
            return new AuthenticationResponse
            {
                Message = token ,
                IsSuccess = true,
                Errors = new List<string>()
            };
        }

        private string CreateJWTToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            var token = new JwtSecurityToken(
                issuer:configuration["Jwt:Issuer"],
                audience:configuration["Jwt:Audience"],
                claims:claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
            );

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenAsString;
        }



       
    }
}