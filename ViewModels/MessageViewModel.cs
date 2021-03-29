using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace work_platform_backend.ViewModels
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
