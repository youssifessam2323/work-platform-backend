using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class CommentService
    {

        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task AddComment(Comment comment)
        {
             await commentRepository.AddComment(comment);
             await commentRepository.SaveChanges();
        }
        
        public async Task<List<Comment>> GetCommentsByTask(int taskId)
        {
            return await commentRepository.GetCommentsByTask(taskId);
        }

        public async Task<Comment> AddNewCommentInTask(string userId, int taskId,Comment comment)
        {
            comment.TaskId = taskId;
            comment.ParentCommentId = null ; 
            comment.CreatorId = userId;
            comment.CreatedAt = DateTime.Now;
            await commentRepository.AddComment(comment);
            await commentRepository.SaveChanges();
            return comment;
        }

         public async Task<Comment> AddNewReplyToComment(int commentId,Comment comment)
        {
            comment.ParentCommentId= commentId;
            comment.TaskId = null ;
            await commentRepository.AddComment(comment);
            await commentRepository.SaveChanges();
            return comment;
        }
    }
}