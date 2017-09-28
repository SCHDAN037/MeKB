using System;

namespace MentorWebApp.Models
{
    public class Question : Message
    {
        public Question()
        {
            DatePosted = DateTime.Now;
        }

        public Question(string title, string message, string userid)
        {
            DatePosted = DateTime.Now;
            Title = title;
            MessageContent = message;
            UserId = userid;
        }

        public bool Anonymous { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
    }
}