using System.Collections.Generic;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public interface IPermissionRepository
    {
         Task<IEnumerable<Permission>> GetAllPermissions();
        Task<Permission> GetPermissionById(int permissionId);
        Task<Permission> GetPermissionByName(string permissionName);
        Task SavePermissions(Permission permission);
        Task<Permission> UpdatePermissionById(int permissionId,Permission permission);
        Task<Permission> DeletePermissionById(int permissionId);
        Task<bool> SaveChanges();



    }
}