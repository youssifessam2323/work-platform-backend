using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}