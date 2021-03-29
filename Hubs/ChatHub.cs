using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Services;
using work_platform_backend.ViewModels;

namespace work_platform_backend.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public readonly static List<UserViewModel> _ConnectionUsers = new List<UserViewModel>();
        public readonly static List<TeamChatViewModel> _TeamChats = new List<TeamChatViewModel>();

        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        private readonly UserService userService;
        private readonly TeamChatService teamChatService;

        public ChatHub(ApplicationContext context, IMapper mapper , UserService userService,TeamChatService teamChatService )
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
            this.teamChatService = teamChatService;
        }


        public async Task CreateTeamChat(string teamChatName)
        {
            try
            {

                // Accept: Letters, numbers and one space between words.
                Match match = Regex.Match(teamChatName, @"^\w+( \w+)*$");
                if (!match.Success)
                {
                    await Clients.Caller.SendAsync("onError", "Invalid Team name!\nTeam name must contain only letters and numbers.");
                }
                else if (teamChatName.Length < 5 || teamChatName.Length > 100)
                {
                    await Clients.Caller.SendAsync("onError", "Team name must be between 5-100 characters!");
                }
                else if (context.Teams.Any(r => r.Name == teamChatName))
                {
                    await Clients.Caller.SendAsync("onError", "Another Team with this name exists");
                }
                else
                {
                    // Create and save chat team in database
                    //var user = context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                    //var team = new TeamChat()
                    //{
                    //    Name = teamName,
                    //    //Admin = user
                    //};
                    //context.Teams.Add(team);
                    //context.SaveChanges();

                    //if (team != null)
                    //{
                        // Update Team list
                        //var teamChatViewModel = _mapper.Map<TeamChat, TeamChatViewModel>(team);
                        //_Teams.Add(teamChatViewModel);
                        //await Clients.All.SendAsync("addChatTeam", teamChatViewModel);  //Change soon
                    //}
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "Couldn't create chat team: " + ex.Message);
            }
        }



        public async Task Join(string teamName)     //Check every switching From team to team
        {
            try
            {
                var user = _ConnectionUsers.Where(u => u.Username == IdentityName).FirstOrDefault();   //Connected on this Team
                if (user != null && user.CurrentTeam != teamName)
                {
                    //// Remove user from others list as if it is not on this room (Deactive)
                    if (!string.IsNullOrEmpty(user.CurrentTeam))
                        await Clients.OthersInGroup(user.CurrentTeam).SendAsync("removeUser", user);  //remove UserName

                    ////// Join to new chat room (active)
                    await Leave(user.CurrentTeam);  //to leave GroupChat from current room old(Remove Connection From Group)
                    await Groups.AddToGroupAsync(Context.ConnectionId, teamName);  //show in this Room that turn to it add His Name To This Group 


                    user.CurrentTeam = teamName;

                    // Tell others to update their list of users
                    await Clients.Groups(teamName).SendAsync("addUser", user);  //username added on this room
                }


            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public async Task Leave(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);   //when change room or logout
        }



        public async Task SendToTeam(string teamName, string message)
        {
            try
            {
                var user = context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                var team = context.Teams.Where(r => r.Name == teamName).FirstOrDefault();

                if (!string.IsNullOrEmpty(message.Trim()))      //remove white spaces character from message 
                {
                    // Create and save message in database
                    //var msg = new Message()
                    //{
                    //    Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty),   //to remove html tags
                    //    FromUser = user,
                    //    ToTeam = team,
                    //    Timestamp = DateTime.Now
                    //};
                    //_context.Messages.Add(msg);
                    context.SaveChanges();

                    // Broadcast the message
                    //var messageViewModel = _mapper.Map<Message, MessageViewModel>(msg);      //chat.js recieve this message to send to it
                    //await Clients.Group(teamName).SendAsync("newMessage", messageViewModel);   //used to send message on group of Room and users listen
                }
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync("onError", "Message not send! Message should be 1-500 characters.");  //on person who send message only
            }
        }

     
       

        public async Task DeleteTeam(string teamName)
        {
            try
            {
                // Delete from database
                //var team = _context.Teams.Include(r => r.Admin)
                //    .Where(r => r.Name == teamName && r.Admin.UserName == IdentityName).FirstOrDefault();
                //_context.Teams.Remove(team);
                context.SaveChanges();

                // Delete from list
                var teamViewModel = _TeamChats.First(r => r.Name == teamName);
                _TeamChats.Remove(teamViewModel);

                // Move users back to Lobby
                await Clients.Group(teamName).SendAsync("onRoomDeleted", string.Format("Team {0} has been deleted.\nYou are now moved to the Lobby!", teamName));

                // Tell all users to update their Team list
                await Clients.All.SendAsync("removeChatTeam", teamViewModel);
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync("onError", "Can't delete this chat Team. Only owner can delete this room.");
            }
        }


        public override Task OnConnectedAsync()  /*when enter application after signin or Register*/
        {
            try
            {
                var user = context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                //var userViewModel = _mapper.Map<ApplicationUser, UserViewModel>(user);
                //userViewModel.Device = GetDevice();
                //userViewModel.CurrentTeam = "";

                if (!_ConnectionUsers.Any(u => u.Username == IdentityName))
                {
                /*    _ConnectionUsers.Add(userViewModel); */  //add user to list show list of user when messages

                }

                /*Clients.Caller.SendAsync("getProfileInfo", user.FullName, user.Avatar);*/   // Show information of user
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = _ConnectionUsers.Where(u => u.Username == IdentityName).First();
                _ConnectionUsers.Remove(user);

                // Tell other users to remove you from their list
                Clients.OthersInGroup(user.CurrentTeam).SendAsync("removeUser", user);

                // Remove mapping
                //_ConnectionsMap.Remove(user.Username);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }


        public IEnumerable<UserViewModel> GetUsers(string teamName)  //after getting room show the current room of user in list of Users
        {
            return _ConnectionUsers.Where(u => u.CurrentTeam == teamName).ToList();
        }

    
        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }


        private string GetDevice()
        {
            var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
            if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
                return device;

            return "Web";
        }
        }
}
