using System;

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