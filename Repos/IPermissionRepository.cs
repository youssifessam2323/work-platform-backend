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
    }
}