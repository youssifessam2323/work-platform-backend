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
        private readonly IAttachmentRepository AttachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            AttachmentRepository = attachmentRepository;

        }


        public async Task<Attachment> AddAttachment(Attachment newAttachment)
        {
            if (newAttachment != null)
            {
                await AttachmentRepository.SaveAttachment(newAttachment);
                await AttachmentRepository.SaveChanges();
                return newAttachment;
            }
            return null;

        }

        public async Task<Attachment> UpdateAttachment(int id, Attachment attachment)
        {
            Attachment UpdatedAttachmentt = await AttachmentRepository.UpdateAttachmentById(id, attachment);

            if (UpdatedAttachmentt != null)
            {
                await AttachmentRepository.SaveChanges();
                return UpdatedAttachmentt;
            }


            return null;

        }


        public async Task DeleteAttachment(int attachmentId)
        {
            var attachment = await AttachmentRepository.DeleteAttachmentById(attachmentId);
            if (attachment == null)
            {

                throw new NullReferenceException();

            }

            await AttachmentRepository.SaveChanges();


        }


        public async Task<IEnumerable<Attachment>> GetAttachmentsOfTask(int taskId)
        {
            return await AttachmentRepository.GetAttachmentByTask(taskId);
        }
    }
}
