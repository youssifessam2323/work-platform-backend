using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;

namespace work_platform_backend.Dtos
{
    public class LoginRequest
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
      





    }
}