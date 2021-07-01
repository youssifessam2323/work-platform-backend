using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByTask(int taskId);
        Task AddComment(Comment comment);

        Task<Comment> DeleteCommentById(int commentId);
        Task<List<Comment>> DeleteCommentsByTask(int taskId);


        Task<bool> SaveChanges();
        Task<bool> isTaskExist(int commentId);
    }
}