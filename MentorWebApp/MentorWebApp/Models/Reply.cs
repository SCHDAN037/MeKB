using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorWebApp.Models
{
    public class Reply : Message
    {
        public Reply()
        {
            DatePosted = DateTime.Now;
            Analytic = new ContentAnalytic(this.Id);
        }

        public Reply(string qid, string message, string uctNumber)
        {
            QuestionId = qid;
            MessageContent = message;
            UctNumber = uctNumber;
            DatePosted = DateTime.Now;
            Analytic = new ContentAnalytic(this.Id);
        }

        //[ForeignKey("QuestionId")]
        public string QuestionId { get; set; }
        public ContentAnalytic Analytic { get; set; }
    }
}