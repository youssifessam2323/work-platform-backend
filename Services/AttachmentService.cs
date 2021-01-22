using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class AttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepo;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepo = attachmentRepository;

        }


        public async Task<Attachment> AddAttachment(Attachment newAttachment)
        {
            if (newAttachment != null)
            {
                await _attachmentRepo.SaveAttachment(newAttachment);
                await _attachmentRepo.SaveChanges();
                return newAttachment;
            }
            return null;

        }

        public async Task<Attachment> UpdateAttachment(int id, Attachment attachment)
        {
            Attachment NewAttachmentt = await _attachmentRepo.UpdateAttachmentById(id, attachment);

            if (NewAttachmentt != null)
            {
                await _attachmentRepo.SaveChanges();
                return NewAttachmentt;
            }


            return null;

        }


        public async Task DeleteAttachment(int attachmentId)
        {
            var attachment = await _attachmentRepo.DeleteAttachmentById(attachmentId);
            if (attachment == null)
            {

                throw new NullReferenceException();

            }

            await _attachmentRepo.SaveChanges();


        }


        public async Task<IEnumerable<Attachment>> GetAttachmentsOfTask(int taskId)
        {
            var attachments = await _attachmentRepo.GetAttachmentByTask(taskId);

            if (attachments.Count().Equals(0))
            {
                return null;

            }

            return attachments;

        }
    }
}
