using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using work_platform_backend.Validations;

namespace work_platform_backend.Dtos
{
    public class LoginDto
    {

        [EmailAttributeUniqueValidation] // not tested yet
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        
    
    }
}