using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;

namespace work_platform_backend.Repos
{
    public class PermissionRepo : IPermissionRepository
    {
        private readonly ApplicationContext _context;
        public PermissionRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            return (await _context.Permissions.ToListAsync());
        }
  

        public async Task<Permission> GetPermissionById(int permissionId)
        {
            return (await _context.Permissions.FirstOrDefaultAsync(P => P.Id == permissionId));
        }

        public async Task<Permission> GetPermissionByName(string permissionName)
        {
            return (await _context.Permissions.FirstOrDefaultAsync(P => P.Name == permissionName));
        }

      

        public async Task SavePermissions(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);

        }

        public async Task<Permission> UpdatePermissionById(int permissionId,Permission permission)
        {
            var NewPermission = await _context.Permissions.FindAsync(permissionId);
            if(NewPermission!=null)
            {
                NewPermission.Name = permission.Name;
                return NewPermission;
            }
            return null;
        }



        public async Task<Permission> DeletePermissionById(int permissionId)
        {
            Permission permission = await _context.Permissions.FindAsync(permissionId);
            if (permission != null)
            {
               _context.Permissions.Remove(permission);
            }
            return permission;
        }


        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
