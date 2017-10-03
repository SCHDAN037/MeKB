using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorWebApp.Models
{
    public class ContentAnalytic : Analytic

    {
        //Content refers to Reply, Questions, Resource

        public int Helpful { get; set; }
        public int UnHelpful { get; set; }
        public int Clicks { get; set; }
        public string ContentId { get; set; }

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
            this.ContentId = "";
        }

        public ContentAnalytic(string contentId)
        {
            this.NewIdentity = Guid.NewGuid().ToString();
            this.ContentId = contentId;
            this.Clicks = 0;
            this.Helpful = 0;
            this.UnHelpful = 0;
        }


    }
}
