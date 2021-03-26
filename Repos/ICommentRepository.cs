using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByTask(int taskId);
        Task AddComment(Comment comment);

        Task SaveChanges();

    }
}