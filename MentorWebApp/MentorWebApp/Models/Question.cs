using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// A question posted by a user on the site, extends message
namespace MentorWebApp.Models
{
    public class Question : Message
    {
        // constructor with no arguments
        public Question()
        {
            DatePosted = DateTime.Now;
            Replies = new List<Reply>();
            Id = Guid.NewGuid().ToString();
        }

        // constructor that takes 3 parameters, title and question body and student number
        public Question(string title, string message, string uctNumber)
        {
            this.Id = Guid.NewGuid().ToString();
            DatePosted = DateTime.Now;
            Title = title;
            MessageContent = message;
            UctNumber = uctNumber;
            Id = Guid.NewGuid().ToString();
        }


        // list of replies to the question
        [NotMapped]
        public List<Reply> RepList { get; set; }

        public List<Reply> Replies { get; set; }

        // represents whether a question is posted anonymously
        public bool Anonymous { get; set; }

        //[Required] the Title of the question
        public string Title { get; set; }

        public string Tags { get; set; }
        public ContentAnalytic Analytic { get; set; }

        public void Init(ContentAnalytic analytic)
        {
            Analytic = analytic;
        }
    }
}