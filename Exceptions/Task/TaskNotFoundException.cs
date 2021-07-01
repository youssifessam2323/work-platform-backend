using System;

namespace work_platform_backend.Exceptions.Task
{
    public class TaskNotFoundException :Exception
    {
        public TaskNotFoundException(string message) :base(message){}
    }
}