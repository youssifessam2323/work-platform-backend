using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class AttachmentRepo : IAttachmentRepository
    {

        private readonly ApplicationContext context;

        public AttachmentRepo(ApplicationContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Attachment>> GetAttachmentByTask(int taskId)
        {
            return (await context.Attachments.Where(A => A.TaskId == taskId).ToListAsync());
        }

        public async Task SaveAttachment(Attachment attachment)
        {
           await context.Attachments.AddAsync(attachment);
        }

        public async Task <Attachment>UpdateAttachmentById(int attachmentId, Attachment attachment)
        {
            var NewAttachment = await context.Attachments.FindAsync(attachmentId);
            if (NewAttachment != null)
            {
                NewAttachment.Name = attachment.Name;
                NewAttachment.Url = attachment.Url;
                NewAttachment.CreatedAt = attachment.CreatedAt;
                NewAttachment.TaskId = attachment.TaskId;
                return NewAttachment;
            }
            return null;
        }

        public async Task <Attachment>DeleteAttachmentById(int attachmentId)
        {
            Attachment attachment = await context.Attachments.FindAsync(attachmentId);
            if (attachment != null)
            {
                context.Attachments.Remove(attachment);
            }
            return attachment;
        }


        public async Task<List<Attachment>> DeleteAttachmentByTaskId(int taskId)
        {
            var attachments = await context.Attachments.Where(a => a.TaskId == taskId).ToListAsync();
            if (attachments != null)
            {
                foreach (Attachment attach in attachments)
                {
                    context.Attachments.Remove(attach);
                }
            }
            return attachments;
        }
        public async Task<bool> SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

        public async Task<Attachment> GetAttachmentById(int attachmentId)
        {
            return await context.Attachments.FindAsync(attachmentId); 
        }

      
    }
}
