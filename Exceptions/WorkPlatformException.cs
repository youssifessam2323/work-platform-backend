using System;

namespace work_platform_backend.Exceptions
{
    public class WorkPlatformException : Exception
    {

        public WorkPlatformException(int StatusCode , string Message) : base(Message)
        {
            this.StatusCode = StatusCode;
        }
        public int StatusCode{set;get;} 
        
        
    }
}