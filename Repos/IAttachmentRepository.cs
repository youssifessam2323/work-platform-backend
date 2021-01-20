using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAttachmentByTask(string taskId);
        Task SaveAttachment(Attachment attachment);
        Task UpdateAttachmentById(int attachmentId,Attachment attachment);
        Task DeleteAttachmentById(int attachmentId);  
       
    }
}