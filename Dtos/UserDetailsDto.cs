using System;
using System.Collections.Generic;

namespace work_platform_backend.Dtos
{
    public class UserDetailsDto
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
        public List<RoomDto> Rooms { get; set; }



        public override string ToString()
        {
            return "Name " + Name + " Email" + Email + " UserName " + UserName; 
        }
    }
}