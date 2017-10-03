using System;

namespace MentorWebApp.Models
{
    public class ContentAnalytic : Analytic

    {
        //Count is number of times it was viewed somewhere
        //Helpful is the number of times a user rated it helpful
        //Unhelpful is the opposite
        //Clicks is the number of times that resource was clicked

        //public ContentAnalytic(string objectId)
        //{

        //    //this.ObjectId = objectId;
        //}

        public ContentAnalytic()
        {
            ContentId = "";
        }

        public ContentAnalytic(string contentId)
        {
            NewIdentity = Guid.NewGuid().ToString();
            ContentId = contentId;
            Clicks = 0;
            Helpful = 0;
            UnHelpful = 0;
        }
        //Content refers to Reply, Questions, Resource

        public int Helpful { get; set; }
        public int UnHelpful { get; set; }
        public int Clicks { get; set; }
        public string ContentId { get; set; }
    }
}