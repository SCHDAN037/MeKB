using System;

namespace MentorWebApp.Models
{
    public class Reply : Message
    {
        public Reply()
        {
            DatePosted = DateTime.Now;
        }

        public Reply(string qid, string message, string userid)
        {
            QuestionId = qid;
            MessageContent = message;
            UserId = userid;
            DatePosted = DateTime.Now;
        }

        public string QuestionId { get; set; }
    }
}