using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using work_platform_backend.Models;
using work_platform_backend.Repos;

namespace work_platform_backend.Services
{
    public class PermissionService
    {
        private readonly IPermissionRepository _PermissionRepo;


        public PermissionService(IPermissionRepository permissionRepository)
        {
            _PermissionRepo = permissionRepository;

        }


        public async Task<Permission> AddPermission(Permission newPermission)
        {
            if (newPermission != null)
            {
                await _PermissionRepo.SavePermissions(newPermission);
                await _PermissionRepo.SaveChanges();
                return newPermission;
            }
            return null;

        }

        public async Task<Permission> UpdatePermission(int permissionId, Permission permission)
        {
            Permission NewPermission = await _PermissionRepo.UpdatePermissionById(permissionId, permission);

            if (NewPermission != null)
            {
                await _PermissionRepo.SaveChanges();
                return NewPermission;
            }

            return null;

        }


        public async Task DeletePermission(int permissionId)
        {
            var Team = await _PermissionRepo.DeletePermissionById(permissionId);
            if (Team == null)
            {

                throw new NullReferenceException();

            }

            await _PermissionRepo.SaveChanges();

        }


        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            var Permissions = await _PermissionRepo.GetAllPermissions();

            if (Permissions.Count().Equals(0))
            {
                return null;

            }

            return Permissions;

        }

        public async Task<Permission> GetPermissionById(int permissionId)
        {
            var Permission = await _PermissionRepo.GetPermissionById(permissionId);

            if (Permission!=null)
            {
                return Permission;

            }
            return null;         

        }


        public async Task<Permission> GetPermissionByName(string permissionName)
        {
            var Permission = await _PermissionRepo.GetPermissionByName(permissionName);

            if (Permission != null)
            {
                return Permission;

            }
            return null;

        }
    }
}
