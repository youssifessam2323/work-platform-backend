using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class TeamRepo : ITeamRepository
    {


        private readonly ApplicationContext context;

        public TeamRepo(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsByCreator(string userId)
        {
            var ListOfUserCreator = await context.Teams.Where(T => T.LeaderId == userId).ToListAsync();
            return ListOfUserCreator;
        }

  

        public async Task<IEnumerable<Team>> GetAllTeamsByMember(string userId)
        {

            var teamsOfMember = (from Team in context.Teams
                                 join Member in context.TeamsMembers
                                 on Team.Id equals Member.TeamId
                                 join U in context.Users
                                 on Member.UserId equals U.Id

                                 where Member.UserId == userId
                                 select Team
                      ).ToListAsync();
            return await teamsOfMember;

        }


        public async Task<IEnumerable<Team>> GetAllTeamsByRoom(int roomId)
        {
           return( await context.Teams.Where(T => T.RoomId == roomId).Include(T=>T.Tasks).ToListAsync());

   

        }

        public async Task<Team> GetTeamById(int teamId)
        {
           return( await context.Teams.FirstOrDefaultAsync(T => T.Id == teamId));
        }

        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task SaveTeam(Team team)
        {
            await context.Teams.AddAsync(team);
        }

        public async Task<Team> UpdateTeamById(int teamId,Team team)
        {
            var NewTeam =  await context.Teams.FindAsync(teamId);
            if( NewTeam!=null)
            {
                NewTeam.Name = team.Name;
                NewTeam.Description = team.Description;
                NewTeam.CreatedAt = team.CreatedAt;
                NewTeam.LeaderId = team.LeaderId;
                NewTeam.RoomId = team.RoomId;
                return NewTeam;
            }
            return null;
        }


        public async Task<Team> DeleteTeamById(int teamId)
        {
            Team team = await context.Teams.FindAsync(teamId);
            if (team != null)
            {
                context.Teams.Remove(team);
            }
            return team;
        }

        public async Task<Team> GetTeamByTeamCode(string teamCode)
        {
            Console.WriteLine("Inserted team code =  " + teamCode);
            Guid insertedTeamCode = new Guid(teamCode);
            return await context.Teams.Where(t => t.TeamCode == insertedTeamCode).FirstAsync();    
        }
    }
}
