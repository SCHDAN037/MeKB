using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorWebApp.Models
{
    public class Message
    {
        public string MessageContent { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
