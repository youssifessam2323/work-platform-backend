using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Services
{
    public class CommentService
    {
        public ApplicationContext context { get; set; }

        public CommentService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task AddComment(Comment comment)
        {
            await context.Comments.AddAsync(comment);
        }
        
        public async Task<List<Comment>> GetCommentsByTask(int taskId)
        {
            return  await context.Comments.Where(c => c.TaskId == taskId).ToListAsync();
        }

        public async Task<Comment> CreatNewComment(int taskId,Comment comment)
        {

             if(comment != null )
             {
                     await context.Comments.AddAsync(comment);
                     await context.SaveChangesAsync();
                    return comment;
             }
             return null ; 
        }
    }
}