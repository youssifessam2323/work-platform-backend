using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace work_platform_backend.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {


        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }


        public DbSet<Team> Teams { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<CheckPoint> CheckPoints { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<TeamsMembers> TeamsMembers { get; set; }
        
        public DbSet<RoomSettings> RoomSettings { get; set; }
        public DbSet<TeamProject> TeamProjects { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<TeamChat> TeamChats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatMessageType> ChatMessageTypes { get; set; }















        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Creator)
                .WithMany(u => u.Rooms)
                .OnDelete(DeleteBehavior.SetNull);



             modelBuilder.Entity<Comment>()
                    .HasOne(c => c.Task)
                    .WithMany(t => t.Comments)
                    .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Creator)
                .WithMany(u => u.OwnedProjects)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.SetNull);


               


            modelBuilder.Entity<RTask>()
                .HasOne(t => t.ParentCheckPoint)
                .WithMany(c => c.SubTasks)
                .HasForeignKey(t => t.ParentCheckPointId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RTask>()
                .HasOne(t => t.Creator)
                .WithMany(u => u.OwnedTasks)
                .HasForeignKey(u => u.CreatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RTask>()
                .Property(t => t.CreatorId)
                .IsRequired(false);

            modelBuilder.Entity<RTask>()
                .HasOne(t => t.Team)
                .WithMany(t => t.Tasks)
                .OnDelete(DeleteBehavior.SetNull);    

            modelBuilder.Entity<RTask>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .OnDelete(DeleteBehavior.Restrict);    

            // modelBuilder.Entity<RTask>()
                // .HasOne(t => t.DependantTask)
                // .WithOne()
                // .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<CheckPoint>()
            .HasOne(c => c.ParentRTask)
            .WithMany(t => t.ChildCheckPoints)
            .HasForeignKey(c => c.ParentRTaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);


          

            modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Task)
            .WithMany(t => t.Attachments)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);







            modelBuilder.Entity<TeamsMembers>()
            .HasKey(tm => new {tm.UserId,tm.TeamId});


            modelBuilder.Entity<TeamsMembers>()
                .HasOne(tm => tm.User)
                .WithMany(u => u.TeamMembers)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<TeamsMembers>()
                .HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .OnDelete(DeleteBehavior.Cascade);






            modelBuilder.Entity<RoomSettings>()
            .HasKey(rs => new { rs.RoomId,rs.SettingId});

            modelBuilder.Entity<RoomSettings>()
                .HasOne(rs => rs.Room)
                .WithMany(r => r.RoomSettings)
                .OnDelete(DeleteBehavior.Cascade);










            modelBuilder.Entity<Team>()
            .HasOne(t => t.Leader)
            .WithMany(u => u.Leads)
            .HasForeignKey(t => t.LeaderId)
            .OnDelete(DeleteBehavior.SetNull);
            
            
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Room)
                .WithMany(r => r.Teams)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            






            modelBuilder.Entity<ProjectManager>()
                .HasKey(pm => new {pm.UserId,pm.RoomId});

            modelBuilder.Entity<ProjectManager>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ManagedProjects)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectManager>()
                .HasOne(pm => pm.Room)
                .WithMany(r => r.ProjectManagers)
                .OnDelete(DeleteBehavior.Cascade);
    








            modelBuilder.Entity<TeamProject>()
                .HasKey(pm => new {pm.TeamId,pm.ProjectId});


            modelBuilder.Entity<TeamProject>()
                .HasOne(tp => tp.Team)
                .WithMany(t => t.TeamProjects)
                .OnDelete(DeleteBehavior.NoAction);


                modelBuilder.Entity<Room>()
                    .HasIndex(r => r.Name)
                    .IsUnique();

                    
                modelBuilder.Entity<Room>()
                    .HasOne(r => r.Creator)
                    .WithMany(u => u.Rooms)
                    .HasForeignKey(p => p.CreatorId)
                    .OnDelete(DeleteBehavior.SetNull);








                modelBuilder.Entity<UserTask>()
                    .HasKey(ut => new {ut.UserId,ut.TaskId});


                modelBuilder.Entity<UserTask>()
                    .HasOne(ut => ut.User)
                    .WithMany(u => u.UserTasks)
                    .OnDelete(DeleteBehavior.Cascade);


                modelBuilder.Entity<UserTask>()
                    .HasOne(ut => ut.Task)
                    .WithMany(t => t.UserTasks)
                    .OnDelete(DeleteBehavior.Cascade);

               








                modelBuilder.Entity<Comment>()
                    .HasOne(c => c.Creator)
                    .WithMany(u => u.Comments)
                    .OnDelete(DeleteBehavior.Cascade);    

                
                modelBuilder.Entity<Comment>()
                    .HasOne<Comment>()
                    .WithMany(c => c.Replies)
                    .HasForeignKey("ParentCommentId")
                    .OnDelete(DeleteBehavior.NoAction);    
                

               
                


                modelBuilder.Entity<Session>()
                    .HasOne(s => s.Task)
                    .WithMany(t => t.Sessions)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                modelBuilder.Entity<Session>()
                    .HasOne(s => s.User)
                    .WithMany(u => u.Sessions)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);



                    //Reply feature Configuration


                modelBuilder.Entity<TeamChat>()
                    .HasIndex(t => t.TeamId)
                    .IsUnique(true);

                modelBuilder.Entity<TeamChat>()
                    .HasOne(tc => tc.Team)
                    .WithOne(t => t.TeamChat)
                    .OnDelete(DeleteBehavior.SetNull);


                modelBuilder.Entity<ChatMessage>()
                        .HasIndex(tc => tc.ChatId)
                        .IsUnique(true);

                modelBuilder.Entity<ChatMessage>()
                      .HasOne(cm => cm.Chat)
                       .WithMany(tc => tc.ChatMessages)
                       .HasForeignKey(m => m.ChatId)
                       .OnDelete(DeleteBehavior.Cascade);


                modelBuilder.Entity<ChatMessage>()
                    .HasOne(mt => mt.MessageType)
                     .WithMany()
                     .HasForeignKey(cm => cm.MessageTypeId)
                     .OnDelete(DeleteBehavior.SetNull);


                modelBuilder.Entity<ChatMessage>()
                    .HasOne( u => u.Creator)
                    .WithMany( u => u.Messages)
                    .HasForeignKey("CreatorId")
                    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }
    }
}