using System;
using System.ComponentModel.DataAnnotations;

namespace work_platform_backend.Dtos
{
    public class RegisterDto
    {
        [Required]
         public string Username { get; set; }
        
        [EmailAddress(ErrorMessage = "Email is not Valid...")]
        [Required]
         public string Email { get; set; }
        
        [Required]
         public  string FirstName { get; set; }
        
        [Required]
         public string LastName { get; set; }
         
         [MinLength(6)]
         [Required]
         public string Password { get; set; }
        
        [Required]
         public string Phone { get; set; }
         
         [Required]
         public string BirthDate { get; set; }

    }
}