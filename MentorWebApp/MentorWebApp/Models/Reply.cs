using System;
using System.ComponentModel.DataAnnotations.Schema;

// models a reply to a question
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

        //[ForeignKey("QuestionId")] the Id of the question the reply is for
        public string QuestionId { get; set; }
        public ContentAnalytic Analytic { get; set; }
    }
}