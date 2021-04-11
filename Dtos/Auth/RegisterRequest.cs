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
         public  string FirstName { get; set; }
        
        [Required]
         public string LastName { get; set; }
         
         [MinLength(6)]
         [Required]
         public string Password { get; set; }

         [MinLength(6)]
         [Required]
         public string ConfirmPassword { get; set; }
        
        [Required]
         public string PhoneNumber { get; set; }
         
         [Required]
         public string BirthDate { get; set; }

<<<<<<< HEAD
=======
             public string GetName()
             {
                return $"{this.FirstName} {this.LastName}";
             }

>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
    }
}