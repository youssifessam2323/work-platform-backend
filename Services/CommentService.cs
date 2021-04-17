using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Dtos;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class CommentService
    {

        private readonly ICommentRepository commentRepository;
        private readonly IRTaskRepository taskRepository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IRTaskRepository taskRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.taskRepository = taskRepository;
            this.mapper = mapper;
        }

        public async Task AddComment(Comment comment)
        {
             await commentRepository.AddComment(comment);
             await commentRepository.SaveChanges();
        }
        
        public async Task<List<CommentDto>> GetCommentsByTask(int taskId)
        {
            var isTaskExist = await taskRepository.isTaskExist(taskId);
             var comments = await commentRepository.GetCommentsByTask(taskId);
            
            return comments.Select(c=> mapper.Map<CommentDto>(c)).ToList();
        }

        public async Task AddNewCommentInTask(string userId, int taskId,CommentDto commentDto)
        {
            if(! await taskRepository.isTaskExist(taskId))
            {
                throw new Exception("task not exist");
            }
            var comment = mapper.Map<Comment>(commentDto);

            comment.TaskId = taskId;
            comment.CreatorId = userId;
            comment.CreatedAt = DateTime.Now;
            comment.ParentCommentId = null;
            await commentRepository.AddComment(comment);
            await commentRepository.SaveChanges();
        }

         public async Task<Comment> AddNewReplyToComment(int commentId,Comment comment)
        {

            if(! await commentRepository.isTaskExist(commentId))
            {
                throw new Exception("comment not exist");
            }

            comment.ParentCommentId= commentId;
            comment.TaskId = null ;
            await commentRepository.AddComment(comment);
            await commentRepository.SaveChanges();
            return comment;
        }



        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await commentRepository.DeleteCommentById(commentId);
            if (comment == null)
            {

                return false;

            }
            
          
            

           return await commentRepository.SaveChanges();


        }

        public async Task<bool> DeleteCommentByTask(int taskId)
        {
            var comments = await commentRepository.GetCommentsByTask(taskId);
            if (comments.Count().Equals(0))
            {

                return false;

            }

           foreach(Comment c in comments)
            {
                await DeleteComment(c.Id);
            }

            return true;


        }

    }
}