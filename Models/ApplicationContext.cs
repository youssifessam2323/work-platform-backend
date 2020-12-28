using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace work_platform_backend.Models
{
    public class ApplicationContext : DbContext
    {


       
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
        }

      
            public virtual DbSet<VerificationToken> tokens { get; set; }
            public virtual DbSet<User> User { get; set; }
            public virtual DbSet<Role> Role { get; set; }
            public virtual DbSet<UsersRoles> UsersRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);

            modelBuilder.Entity<UsersRoles>()
                            .HasKey(sc => new {sc.UserId , sc.RoleId});    
            
        }
    }
}