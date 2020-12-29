using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using work_platform_backend.Authorization;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class AuthService
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        private readonly IVerificationTokenRepository verificationTokenRepository;

        public AuthService(IUserRepository userRepository , IMapper mapper , IConfiguration configuration , IEmailService emailService ,IVerificationTokenRepository verificationTokenRepository)
        {
            this.userRepository = userRepository ; 
            this.mapper = mapper;
            this.configuration = configuration ; 
            this.emailService = emailService ; 
            this.verificationTokenRepository = verificationTokenRepository;
        }
        public async Task SignUp(RegisterDto registerDto)
        {
            VerificationToken verificationToken = createVerificationToken(); 
            User user = createUser(registerDto , verificationToken);
            await SendConfirmationMail(registerDto.Email,verificationToken);
            await userRepository.AddNewUser(user);
            userRepository.SaveChanges();
        }

        private async Task SendConfirmationMail(string email ,VerificationToken verificationToken)
        {
            string confimationUrl = "http://localhost:5000/api/v1/auth/"+verificationToken.token; 
            Message message 
            = new Message(email, "Email Confirmation","Welcome to our Platform Please Click in the Link to Verify Your Email.", confimationUrl);
           
            await emailService.SendEmailAsync(message);
        }

        internal async Task VerifyEmail(string token)
        {
            User user = await userRepository.getUserByToken(token);
            user.isEnabled = true ;
            verificationTokenRepository.deleteVerificationToken(user.verificationToken);
            verificationTokenRepository.SaveChanges();   
        }

        private User createUser(RegisterDto registerDto , VerificationToken verificationToken)
        {
            User user = mapper.Map<User>(registerDto);
            user.isEnabled = false ;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            user.createdDate = DateTime.Now ;
            user.BirthDate = DateTime.Parse(registerDto.BirthDate);
            user.verificationToken = verificationToken;

            return user; 
        }

        public async Task<AuthenticationResponse> Signin(LoginDto loginDto)
        {
            User user = await AuthenticateUser(loginDto);

            if(user != null)
            {
                var token = GenerateJwtToken(user);
                return new AuthenticationResponse(token,user.Username,user.Email);
            }    

            return null;
        }


        private async Task<User> AuthenticateUser(LoginDto loginDto)
        {
            User user = await userRepository.GetUserByEmail(loginDto.Email);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password,user.Password);

            if(isValidPassword)
            {
                return user;  
            }

            return null ; 
        }

        
        private string  GenerateJwtToken(User user)
        {
            var securityKey =
             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])); 
            
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Username.ToString()),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim("role" , Policies.LEADER),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer : configuration["Jwt:Issuer"],
                audience : configuration["Jwt:Audience"],
                claims : claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token) ; 
        }


        private VerificationToken createVerificationToken()
        {
            VerificationToken verificationToken = new VerificationToken();
            verificationToken.token = Guid.NewGuid().ToString();
            return verificationToken;
        }
    }
}