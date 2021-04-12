using System;

namespace work_platform_backend.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public string ImageUrl { get; set; }
        
        public string JobTitle { get; set; }
        

        public DateTime CreatedAt { get; set; }
        
        public bool IsEnabled { get; set; }

        public  string PhoneNumber { get; set; }
        
        public bool EmailConfirmed { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}