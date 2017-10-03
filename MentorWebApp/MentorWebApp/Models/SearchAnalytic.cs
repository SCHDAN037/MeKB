using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorWebApp.Models
{
    public class SearchAnalytic : Analytic
    {
        public int NoOfResults { get; set; }
        public int SucceedClicks { get; set; }
        public int NoResultsCount { get; set; }
        public string SearchResultId { get; set; }

        //Count is number of times it was searched
        //NoOfResults is the number of results returned
        //SuccedClicks is number of times a user clicked a result in that search
        //NoResultsCount is number of times that search returned no Results

        public SearchAnalytic()
        {
            //this.NewIdentity = Guid.NewGuid().ToString();
        }

        public SearchAnalytic(string id)
        {
            this.NewIdentity = Guid.NewGuid().ToString();
            this.SearchResultId = id;
            

        }
        
    }
}
