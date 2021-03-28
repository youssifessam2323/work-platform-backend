using System;
using System.Collections.Generic;
using work_platform_backend.Models;

namespace work_platform_backend.Dtos.Response
{
    public class UserResponse
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string JobTitle { get; set; }
        
        
        
        
        
    }
}