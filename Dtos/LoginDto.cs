using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;

namespace work_platform_backend.Dtos
{
    public class LoginDto
    {

       
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        
    
    }
}