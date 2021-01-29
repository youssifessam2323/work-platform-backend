using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetAttachmentByTask(int taskId);
        Task SaveAttachment(Attachment attachment);
        Task <Attachment>UpdateAttachmentById(int attachmentId,Attachment attachment);
        Task <Attachment>DeleteAttachmentById(int attachmentId);
        Task<bool> SaveChanges();


    }
}