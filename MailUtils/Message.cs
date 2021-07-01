using MimeKit;

namespace work_platform_backend.Models
{
    public class Message
    {
        public MailboxAddress To{set;get;}
        public string Subject { get; set; }
        
        public string Content { get; set; }
        public string ConfirmationLink{set;get;}
        
        
        
        public Message(string to , string subject , string content , string confirmationLink )
        {
            To = MailboxAddress.Parse(to) ;
            Subject = subject ; 
            Content = content; 
            ConfirmationLink = confirmationLink;
        }
    }
}