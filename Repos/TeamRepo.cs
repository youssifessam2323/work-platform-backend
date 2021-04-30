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

        //get team without any joins (trying to improve the performance)
          public async Task<Team> GetTeamOnlyById(int teamId)
        {
           return( await context.Teams
                        .FirstOrDefaultAsync(T => T.Id == teamId));
        }

        public async Task<Team> GetTeamById(int teamId)
        {
           return( await context.Teams
                        .Include(t =>t.TeamMembers).ThenInclude(tm =>tm.User)
                        .Include(t => t.Tasks)
                        .Include(t => t.SubTeams).ThenInclude(t => t.SubTeams)
                        .Include(t => t.Room)
                        .Include(t => t.TeamChat)
                        .FirstOrDefaultAsync(T => T.Id == teamId));
        }

        public async Task<List<Team>> GetTeamByProject(int projectId)
        {
            List<TeamProject> teamProjects = await context.TeamProjects
                                                .Include(tp => tp.Team)
                                                .Where(tp => tp.ProjectId == projectId)
                                                .ToListAsync();


            List<Team> teams = new List<Team>();

            teamProjects.ForEach(tp => teams.Add(tp.Team));
            return teams;
        }

        public async Task<bool> RemoveTeamProjectbyTeam(int teamId)
        {
            var teamProjects = await context.TeamProjects
                                                    .Where(tp => tp.TeamId == teamId)
                                                    .ToListAsync();

            if (teamProjects.Count().Equals(0))
            {
                return false;
            }
            foreach(TeamProject tp in teamProjects) 
            {
                context.TeamProjects.Remove(tp);
            }
                          
            return true;
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
                NewTeam.LeaderId = team.LeaderId;
                NewTeam.RoomId = team.RoomId;
                return NewTeam;
            }
            return null;
        }


        public async Task<Team> DeleteTeamById(int teamId)
        {
            Team team = await context.Teams.Include(t => t.SubTeams)
                                    .Where(t => t.Id == teamId)
                                    .FirstOrDefaultAsync();


            foreach (var subteam in team.SubTeams)
            {
                await DeleteTeamById(subteam.Id);
            }

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
            return await context.Teams.Where(t => t.TeamCode == insertedTeamCode).SingleOrDefaultAsync();    
        }

        public async Task<List<Team>> GetTeamSubTeamsById(int teamId)
        {
            Team team = await context.Teams
                                .Include(t => t.SubTeams)
                                .Where(t => t.Id == teamId)
                                .FirstAsync();

            team.SubTeams.ForEach( t =>{
                if(t.SubTeams == null )
                {
                   t.SubTeams = GetTeamSubTeamsById(t.Id).Result;
                }

            });                             
             
             return team.SubTeams;
        }

        public async Task<List<User>> GetMembersOfTeam(int teamId)
        {
            var teamMembers =  await context.TeamsMembers.Include(tm =>tm.User).Where(tm => tm.TeamId == teamId).ToListAsync();
            List<User> users = new List<User>();
        
            teamMembers.ForEach(tm => users.Add(tm.User));

            return users;
        }


        public async Task<bool> isUserinThisTeamExist(int teamId, string userId)
        {
            var teamMember = context.TeamsMembers
                                        .Where(tm => tm.TeamId == teamId && tm.UserId == userId)
                                        .SingleOrDefaultAsync();

            return await teamMember != null ? true : false ;                                   


        }

        public async Task<bool> IsTeamExist(int teamId)
        {
            var team = await context.Teams.FindAsync(teamId);

            return team != null ? true : false;

        }

        public async Task<List<Team>> GetAllTeamsByCreatorAndRoom(string userId, int roomId)
        {
           return await context.Teams.Where(t => t.LeaderId == userId && t.RoomId == roomId).ToListAsync();
        }
    }
}
