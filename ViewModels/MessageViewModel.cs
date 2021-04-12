using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace work_platform_backend.ViewModels
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string FromUser { get; set; }
        public int ToChat { get; set; }
    }
}
