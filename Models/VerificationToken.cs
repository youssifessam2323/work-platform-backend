using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace work_platform_backend.Models
{
    public class VerificationToken
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }
        public string token { get; set; }
        public int  UserId { get; set; }

        [ForeignKey("UserId")]
        public  User User  { get; set; }
        
    }
}