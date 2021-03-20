using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Dtos.Response;
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

        public async Task SaveNewTeamMember(User user, Team team)
        {
            TeamsMembers teamsMembers = new TeamsMembers();
            
            teamsMembers.User = user;
            teamsMembers.Team = team;

            Console.WriteLine("Team Members = " + teamsMembers);    
            await context.TeamsMembers.AddAsync(teamsMembers);
        }


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
    }
}