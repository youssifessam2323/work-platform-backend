using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class AuthenticationResponse
    {
       
        public  string Message { get; set; }
        public  string Token { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        
        
        
    }
}