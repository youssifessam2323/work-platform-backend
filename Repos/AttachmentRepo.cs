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

        private readonly ApplicationContext _context;

        public AttachmentRepo(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Attachment>> GetAttachmentByTask(int taskId)
        {
            return (await _context.Attachments.Where(A => A.TaskId == taskId).ToListAsync());
        }

        public async Task SaveAttachment(Attachment attachment)
        {
           await _context.Attachments.AddAsync(attachment);
        }

        public async Task <Attachment>UpdateAttachmentById(int attachmentId, Attachment attachment)
        {
            var NewAttachment = await _context.Attachments.FindAsync(attachmentId);
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
            Attachment attachment = await _context.Attachments.FindAsync(attachmentId);
            if (attachment != null)
            {
                _context.Attachments.Remove(attachment);
            }
            return attachment;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
