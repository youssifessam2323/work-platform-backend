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

<<<<<<< HEAD
        public async Task SaveNewTeamMember(User user, Team team)
        {
            TeamsMembers teamsMembers = new TeamsMembers();
            
            teamsMembers.User = user;
            teamsMembers.Team = team;
=======
        public async Task SaveNewTeamMember(string userId, int teamId)
        {
            TeamsMembers teamsMembers = new TeamsMembers();
            
            teamsMembers.UserId = userId;
            teamsMembers.TeamId = teamId;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca

            Console.WriteLine("Team Members = " + teamsMembers);    
            await context.TeamsMembers.AddAsync(teamsMembers);
        }

<<<<<<< HEAD
        public void  DeleteTeamMember(User user, Team team)
        {
            TeamsMembers teamsMembers = new TeamsMembers();

            teamsMembers.User = user;
            teamsMembers.Team = team;

            Console.WriteLine("Team Members = " + teamsMembers);
            context.TeamsMembers.Remove(teamsMembers);
           
        }

=======
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<Team>> getUserTeams(string userId)
        {
            var teamMembers =  await context.TeamsMembers
                    .Where(tm => tm.UserId == userId)
                    .Include(tm => tm.Team)
                    .ToListAsync();
            
            teamMembers.ForEach(tm => Console.WriteLine("Team Members ====> "+tm));
            return teamMembers.Select( tm => tm.Team).ToList();
        }

        public async Task<User> GetUserByUsername(string username)
        {
<<<<<<< HEAD
            return await context.Users.Where(u => u.UserName == username).FirstAsync();
=======
            return await context.Users.Where(u => u.UserName == username).SingleOrDefaultAsync();
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
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

<<<<<<< HEAD
      
=======
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
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
    }
}