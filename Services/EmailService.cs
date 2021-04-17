using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using work_platform_backend.Models;

namespace work_platform_backend.Services
{
    public class EmailService : IEmailService
    {
        public readonly EmailConfiguration _emailConfig ;
        
        
        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig ;
        }



         public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }


        private MimeMessage CreateEmailMessage(Message message)
        {   
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {Text = string.Format("<html style='background-color='#00022e'><body style='background-color='#00022e''><h1>Welcome To WorkPlatform</h1> <p>{0}</p> <a href={1}>Click Here</a></body><html>",message.Content,message.ConfirmationLink)};

               return emailMessage;  
 
        }


        private async Task SendAsync(MimeMessage mimeMessage)
            {
                using(var client = new SmtpClient())
                {
                    try
                    {
                       await client.ConnectAsync(_emailConfig.SmtpServer,_emailConfig.Port,true);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        await client.AuthenticateAsync(_emailConfig.Username,_emailConfig.Password);

                       await client.SendAsync(mimeMessage);
                    }
                    catch
                    {
                        throw;
                    }    
                    finally
                    {
                       await client.DisconnectAsync(true);
                        client.Dispose();
                    }
                }
            }

    }
}