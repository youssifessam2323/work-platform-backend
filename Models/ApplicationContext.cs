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
        public DbSet<DependOn> DependOns { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<TeamsMembers> TeamsMembers { get; set; }
        public DbSet<UserRoomPermission> UserRoomPermissions { get; set; }
        
        public DbSet<RoomSettings> RoomSettings { get; set; }
        
        
        
        
        
       
        
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              base.OnModelCreating(modelBuilder);

              modelBuilder.Entity<DependOn>()
              .HasOne( d => d.RTask)
              .WithMany(t => t.DependantOnMe)
              .HasForeignKey(d => d.RTaskId);


             modelBuilder.Entity<DependOn>()
              .HasOne( d => d.DependantTask)
              .WithMany(t => t.DependOnThem)
              .HasForeignKey(d => d.RTaskId);   


            modelBuilder.Entity<Project>()
            .HasMany(p => p.Managers)
            .WithOne(pm => pm.Project);


            modelBuilder.Entity<CheckPoint>()
            .HasOne(c => c.ParentRTask)
            .WithMany(t => t.ChildCheckPoints)
            .HasForeignKey(c => c.ParentRTaskId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<RTask>()
            .HasOne(t => t.ParentCheckPoint)
            .WithMany(c => c.SubTasks)
            .HasForeignKey(t => t.ParentCheckPointId)
            .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Attachment>()
            .HasOne(a => a.Task)
            .WithMany(t => t.Attachments)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<UserRoomPermission>()
            .HasKey(urp => new {urp.UserId,urp.RoomId} );


            modelBuilder.Entity<TeamsMembers>()
            .HasKey(tm => new {tm.UserId,tm.TeamId});

            modelBuilder.Entity<RoomSettings>()
            .HasKey(rs => new { rs.RoomId,rs.SettingId});


            modelBuilder.Entity<TeamsMembers>()
            .HasKey(tm => new { tm.UserId,tm.TeamId});


            modelBuilder.Entity<Team>()
            .HasOne(t => t.Creator)
            .WithMany(u => u.Leads)
            .HasForeignKey(t => t.CreatorId)
            .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}