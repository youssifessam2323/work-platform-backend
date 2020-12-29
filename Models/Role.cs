using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }   
        public string Name {get;}
        
        public IList<UsersRoles> UserRoles { get; set; }
        
        
        
    }
}