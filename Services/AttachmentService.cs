using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using work_platform_backend.Dtos;
using work_platform_backend.Exceptions.Attachment;
using work_platform_backend.Exceptions.Task;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class AttachmentService
    {
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
            
          
        }

        public async Task<Attachment> UpdateAttachment(int id, Attachment attachment)
        {
            Attachment UpdatedAttachmentt = await attachmentRepository.UpdateAttachmentById(id, attachment);

            if (UpdatedAttachmentt != null)
            {
                await attachmentRepository.SaveChanges();
                return UpdatedAttachmentt;
            }


            return null;

        }


        public async Task<bool> DeleteAttachment(int attachmentId)
        {
            var attachment = await attachmentRepository.DeleteAttachmentById(attachmentId);
            if (attachment == null)
            {

                return false;

            }

           return await attachmentRepository.SaveChanges();


        }

        public async Task<bool> DeleteAttachmentByTask(int taskId)
        {
            var attachments = await attachmentRepository.GetAttachmentByTask(taskId);
            if (attachments.Count().Equals(0))
            {

                return false;

            }

            foreach(Attachment attachment in attachments)
            {

                await DeleteAttachment(attachment.Id);
            }

            return true;

        }


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
        }
    }
}
