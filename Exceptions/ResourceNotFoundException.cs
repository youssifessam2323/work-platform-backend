using System;

namespace work_platform_backend.Exceptions
{
    public class ResourceNotFoundException : Exception
    {

        public ResourceNotFoundException()
        :base(string.Format("the resource is not exist"))
        {

        }
    }
}