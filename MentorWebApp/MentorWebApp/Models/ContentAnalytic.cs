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

        //Count is number of times it was viewed somewhere
        //Helpful is the number of times a user rated it helpful
        //Unhelpful is the opposite
        //Clicks is the number of times that resource was clicked

        public ContentAnalytic(string objectId)
        {
            //this.NewIdentity = Guid.NewGuid().ToString();
            
            this.ObjectId = objectId;
        }
    }
}
