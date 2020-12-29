using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace work_platform_backend.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{set;get;}
        [Required]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "The Email is not Valid")]
        [Required]
        public string Email { get; set; }

        [Required]
        public  string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime createdDate { get; set; }
        public bool isEnabled { get; set; }
        public  VerificationToken verificationToken { get; set; }
        
        public IList<UsersRoles> UsersRoles { get; set; }
        
        

        
        
        
        
        
        
    }
}