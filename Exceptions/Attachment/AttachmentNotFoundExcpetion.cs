using System;

namespace work_platform_backend.Exceptions.Attachment
{
    public class AttachmentNotFoundExcpetion : Exception
    {
        public AttachmentNotFoundExcpetion(string message) :base(message){}
    }
}