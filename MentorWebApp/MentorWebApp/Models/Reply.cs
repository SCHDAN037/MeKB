using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorWebApp.Models
{
    public class Reply : Message
    {
        public Reply()
        {
            DatePosted = DateTime.Now;
        }

        public Reply(string qid, string message, string uctNumber)
        {
            QuestionId = qid;
            MessageContent = message;
            UctNumber = uctNumber;
            DatePosted = DateTime.Now;
        }

        //[ForeignKey("QuestionId")]
        public string QuestionId { get; set; }
    }
}