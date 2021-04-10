using System;

namespace work_platform_backend.Exceptions.Team
{
    public class TeamNotFoundException:Exception
    {
        public TeamNotFoundException(string message) :base(message){}
        
    }
}