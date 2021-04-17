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
        private readonly TeamService teamService;
        private readonly ChatMessageService chatMessageService;

        public ChatHub(ApplicationContext context, IMapper mapper , UserService userService,TeamChatService teamChatService,TeamService teamService , ChatMessageService chatMessageService )
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
            this.teamChatService = teamChatService;
            this.teamService = teamService;
            this.chatMessageService = chatMessageService;



            }


      


        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task JoinTeam(string teamCode)     //Check every switching From team to team
        {
            try
            {
                string userId = userService.GetUserId();

                await userService.JoinTeam(teamCode,userId);   //Join this Team
                var teamJoin = await teamService.GetTeamByTeamCode(teamCode);  //Throw exception if Not Found TeamCode


                //User And Team Are Found 
                if (!string.IsNullOrEmpty(userId) && teamJoin!= null )
                {
                    
                   var JoinChatOfTeam = await teamChatService.GetTeamChatOfTeam(teamJoin.Id);

                    if (JoinChatOfTeam != null)
                    {
                       var user = await userService.getUserById(userId);
                        
                        await Clients.Group(JoinChatOfTeam.ChatName).SendAsync("ReceiveMessageOnJoin", $"User: {user.UserName} Join Group of {JoinChatOfTeam} "); //Not Show to New User That Join
                        await Groups.AddToGroupAsync(Context.ConnectionId, JoinChatOfTeam.ChatName);  //add to Group to tell Clients on Group new User Come
                    }         
                }

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the Team!" + ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task LeaveTeam(int teamId)
        {
            try
            {

                await userService.LeaveTeam(teamId, userService.GetUserId());

                var ChatThatJoined = await teamChatService.GetTeamChatOfTeam(teamId);
                if (ChatThatJoined != null)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, ChatThatJoined.ChatName);   //when change room or logout
                }

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to Leave the Team Unexpected Error !" + ex.Message);
            }
                    
          
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task SendToTeam(string message, int teamId)
        {
            try
            {

                var Chat = await teamChatService.GetTeamChatOfTeam(teamId);
                if (Chat != null)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, Chat.ChatName);

                    ChatMessage chatMessage = new ChatMessage()
                    {
                        Content = message
                    };
                   

                    var newMessage = await chatMessageService.CreateMessage(chatMessage, userService.GetUserId(), Chat.Id); //save message in database
                    if (newMessage != null)
                    {
                     
                        MessageViewModel messageViewModel = new MessageViewModel()
                        {
                            Content = newMessage.Content,
                            Timestamp = newMessage.Timestamp,
                            FromUser = newMessage.CreatorId,
                            ToChat = newMessage.ChatId

                        };
                        
                        await Clients.Group(Chat.ChatName).SendAsync("newMessage", messageViewModel);
                    }

                }

                
          
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync("onError", "Message not send! Message should be 1-500 characters.");  //on person who send message only
            }
        }




        public async Task DeleteTeam(int teamId)
        {
            try
            {
               await teamService.DeleteTeam(teamId);
              
            }
            catch (Exception)
            {
                await Clients.Caller.SendAsync("onError", "Can't delete this chat Team. Only owner can delete this Team.");
            }
        }


      
        public async Task< IEnumerable<MessageViewModel>> GetMessageHistory(int teamId)
        {
            //IEnumerable<ChatMessage> allMessagesOfChat = new List<ChatMessage>();

            var Chat = await teamChatService.GetTeamChatOfTeam(teamId);
            
            if (Chat != null)
            {

              var allMessagesOfChat = await chatMessageService.GetMessageHistorybyChat(Chat.Id);             
                return mapper.Map<IEnumerable<ChatMessage>, IEnumerable<MessageViewModel>>(allMessagesOfChat);

            }
           
            return (new List<MessageViewModel>());

        }

        //  public override Task OnConnectedAsync()  /*when enter application after signin or Register*/
        //  {
        //      try
        //      {
        //          //var user = userService.GetUserId();
        //          //var userViewModel = mapper.Map<User, UserViewModel>(user);
        //          //userViewModel.Device = GetDevice();
        //          //userViewModel.CurrentTeam = "";

        //          if (!_ConnectionUsers.Any(u => u.Username == IdentityName))
        //          {
        //          /*    _ConnectionUsers.Add(userViewModel); */  //add user to list show list of user when messages

        //          }

        //          /*Clients.Caller.SendAsync("getProfileInfo", user.FullName, user.Avatar);*/   // Show information of user
        //      }
        //      catch (Exception ex)
        //      {
        //          Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
        //      }
        //      return base.OnConnectedAsync();
        //  }

        //  public override Task OnDisconnectedAsync(Exception exception)
        //  {
        //      try
        //      {
        //          var user = _ConnectionUsers.Where(u => u.Username == IdentityName).First();
        //          _ConnectionUsers.Remove(user);

        //          // Tell other users to remove you from their list
        //          Clients.OthersInGroup(user.CurrentTeam).SendAsync("removeUser", user);

        //          // Remove mapping
        //          //_ConnectionsMap.Remove(user.Username);
        //      }
        //      catch (Exception ex)
        //      {
        //          Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
        //      }
        //return base.OnDisconnectedAsync(exception);

        //  }


        //public IEnumerable<UserViewModel> GetUsers(string teamName)  //after getting room show the current room of user in list of Users
        //{
        //    return _ConnectionUsers.Where(u => u.CurrentTeam == teamName).ToList();
        //}


        //private string IdentityName
        //{
        //    get { return Context.User.Identity.Name; }
        //}


        //private string GetDevice()
        //{
        //    var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
        //    if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
        //        return device;

        //    return "Web";
        //}
    }
}
