using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions.Attachment;
using work_platform_backend.Exceptions.Task;
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class AttachmentService
    {
<<<<<<< HEAD
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

=======
        private readonly IAttachmentRepository attachmentRepository ;      
        private IMapper mapper;
        private IRTaskRepository taskRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository, IMapper mapper, IRTaskRepository taskRepository)
        {
            this.attachmentRepository = attachmentRepository;
            this.mapper = mapper;
            this.taskRepository = taskRepository;
        }


        public async Task AddAttachment(AttachmentDto attachmentDto,string userId)
        {

            if ( attachmentDto!= null)
            {
                var newAttachment = mapper.Map<Attachment>(attachmentDto);
                newAttachment.CreatorId = userId;
                await attachmentRepository.SaveAttachment(newAttachment);
                await attachmentRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Error occured while saving, please try later");
            }
            
          
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }

        public async Task<Attachment> UpdateAttachment(int id, Attachment attachment)
        {
<<<<<<< HEAD
            Attachment UpdatedAttachmentt = await AttachmentRepository.UpdateAttachmentById(id, attachment);

            if (UpdatedAttachmentt != null)
            {
                await AttachmentRepository.SaveChanges();
=======
            Attachment UpdatedAttachmentt = await attachmentRepository.UpdateAttachmentById(id, attachment);

            if (UpdatedAttachmentt != null)
            {
                await attachmentRepository.SaveChanges();
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
                return UpdatedAttachmentt;
            }


            return null;

        }


        public async Task DeleteAttachment(int attachmentId)
        {
<<<<<<< HEAD
            var attachment = await AttachmentRepository.DeleteAttachmentById(attachmentId);
=======
            var attachment = await attachmentRepository.DeleteAttachmentById(attachmentId);
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
            if (attachment == null)
            {

                throw new NullReferenceException();

            }

<<<<<<< HEAD
            await AttachmentRepository.SaveChanges();
=======
            await attachmentRepository.SaveChanges();
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca


        }


<<<<<<< HEAD
        public async Task<IEnumerable<Attachment>> GetAttachmentsOfTask(int taskId)
        {
            return await AttachmentRepository.GetAttachmentByTask(taskId);
=======
        public async Task<IEnumerable<AttachmentDto>> GetAttachmentsOfTask(int taskId)
        {
            if(await taskRepository.GetTaskById(taskId) == null )
            {
                throw new TaskNotFoundException("task not exist");
            }

            IEnumerable<Attachment> attachments = await attachmentRepository.GetAttachmentByTask(taskId);

            return attachments.ToList().Select(a => mapper.Map<AttachmentDto>(a)).ToList();
        }

        public async Task getAttachmentById(int attachmentId)
        {
            if (await attachmentRepository.GetAttachmentById(attachmentId) == null)
            {
                throw new AttachmentNotFoundExcpetion("attachment not exist");
            }
>>>>>>> 1e220a26bb5cb28e0043bf6570f889c02ac1eeca
        }
    }
}
