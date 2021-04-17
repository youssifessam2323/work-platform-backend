using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext context;

        public CommentRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task AddComment(Comment comment)
        {
            await context.Comments.AddAsync(comment);
        }

         
        public async Task<List<Comment>> DeleteCommentsByTask(int taskId)
        {
            var comment = await context.Comments.Where(c => c.TaskId == taskId).ToListAsync();
            if (comment != null)
            {
                foreach (Comment com in comment)
                {
                    context.Comments.Remove(com);
                }
            }
            return comment;
        }

        public async Task<Comment> DeleteCommentById(int commentId)
        {
            Comment comment = await context.Comments.Include(c => c.Replies)
                                    .Where(c=>c.Id == commentId)
                                    .FirstOrDefaultAsync();

            foreach (var replies in comment.Replies)
            {
                context.Comments.Remove(replies);
                
            }

            if (comment != null)
            {               
                context.Comments.Remove(comment);

            }
            return comment;
        }

        public async Task<List<Comment>> GetCommentsByTask(int taskId)
        {
            return  await context.Comments.Include(c => c.Replies).Where(c => c.TaskId == taskId).ToListAsync();
        }

        public async Task<bool> isTaskExist(int commentId)
        {
           var comment = await context.Comments.FindAsync(commentId);

           return comment != null ? true : false;
        }

        public async Task<bool>  SaveChanges()
        {
            return (await context.SaveChangesAsync() >= 0);
        }

      
    }
}