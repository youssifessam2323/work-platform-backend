using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using work_platform_backend.Dtos;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly ApplicationContext dbContext;

        public UserRepositoryImpl(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AddNewUser(User user)
        {
            var insertedUser = await dbContext.User.AddAsync(user);
            dbContext.SaveChanges();
            return insertedUser.Entity;
        }

        

        public IEnumerable<User> GetAllUsers()
        {
            return  dbContext.User.ToList();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await dbContext.User.SingleOrDefaultAsync(user => email == user.Email);
        }

        public async Task<User> getUserByToken(string token)
        {
            VerificationToken verificationToken = await dbContext.tokens.SingleOrDefaultAsync(verificationToken => verificationToken.token == token);
            
            return await dbContext.User.Where(user =>  user.verificationToken == verificationToken)
                                    .FirstOrDefaultAsync<User>();
        }

        public ICollection<Role> getUserRoleByUsername(string username)
        {
            var UserRoles = (from u in dbContext.User
                        join ur in dbContext.UsersRoles on u.Id equals ur.UserId
                        join r in dbContext.Role on ur.RoleId equals r.Id
                        where u.Username == username
                        select r
                        ).ToList();
                        
                        
                         
            return UserRoles ; 
        }

        public bool SaveChanges()
        {
            return (dbContext.SaveChanges() >= 0 );
        }
    }
}