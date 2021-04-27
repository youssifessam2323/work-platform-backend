using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext context ;
        private readonly IMapper mapper ;

        public UserRepository(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<User> GetUserById(string id)
        {
            return await context.Users.FindAsync(id); 
        }

        public async Task SaveNewTeamMember(string userId, int teamId)
        {
            TeamsMembers teamsMembers = new TeamsMembers();
            
            teamsMembers.UserId = userId;
            teamsMembers.TeamId = teamId;

            Console.WriteLine("Team Members = " + teamsMembers);    
            await context.TeamsMembers.AddAsync(teamsMembers);
        }

        public async Task DeleteTeamMember(string userId, int teamId)
        {
            var teamMembers = await context.TeamsMembers
             .Where(tm => tm.UserId == userId && tm.TeamId == teamId )
             .SingleOrDefaultAsync();

            Console.WriteLine("Team Members = " + teamMembers);
            context.TeamsMembers.Remove(teamMembers);
           
        }


        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

     
        public async Task<User> GetUserByUsername(string username)
        {
            return await context.Users.Where(u => u.UserName == username).SingleOrDefaultAsync();
        }

        public async Task<User> UpdateUser(string userId, User newUser)
        {
            User user = await context.Users.FindAsync(userId);
            user = UpdateUser(newUser, user);
            context.Users.Update(user);
            await SaveChanges();
            return user;
        }

        private static User UpdateUser(User newUser, User user)
        {
            user.BirthDate = newUser.BirthDate;
            user.Name = newUser.Name;
            user.UserName = newUser.UserName;
            user.Email = newUser.Email;
            user.PhoneNumber = newUser.PhoneNumber;
            user.JobTitle = newUser.JobTitle;
            user.ImageUrl = newUser.ImageUrl;

            return user;
        }

        public async Task<bool> IsUserExistByUsername(string username)
        {
            var user = await context.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();
            return user != null ? true : false;
        }

        public async Task<bool> IsUserExistById(string userId)
        {
            var user = await context.Users.FindAsync(userId);
            return user != null ? true : false;
        }

        public async Task<bool> IsUserExistByEmail(string email)
        {
            var user = await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user != null ? true : false;
        }

        public async Task<List<Team>> getUserTeams(string userId)
        {
            var teamMembers = await context.TeamsMembers
                                                .Include(tm => tm.Team)
                                                .Where(tm => tm.UserId == userId)
                                                .ToListAsync();
                                                            
            teamMembers.ForEach(tm => Console.WriteLine("Team Members ====> "+tm));
            return teamMembers.Select( tm => tm.Team).ToList();
        }

        }
}
