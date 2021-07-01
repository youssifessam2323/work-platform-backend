using System;
using System.ComponentModel.DataAnnotations;

namespace work_platform_backend.Dtos
{
    public class RegisterRequest
    {
        [Required]
         public string Username { get; set; }
        
        [Required]
         public string Email { get; set; }
        
        [Required]
         public  string Name { get; set; }
        
         
         [MinLength(6)]
         [Required]
         public string Password { get; set; }

         [MinLength(6)]
         [Required]
         public string ConfirmPassword { get; set; }
        
        [Required]
         public string PhoneNumber { get; set; }

         [Required]
         public string JobTitle { get; set; }
         
         [Required]
         public string BirthDate { get; set; }

    }
}