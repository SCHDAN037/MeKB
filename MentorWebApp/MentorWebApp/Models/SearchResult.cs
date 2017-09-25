using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorWebApp.Models
{
    public class SearchResult
    {
        public List<List<string>> ResultsList { get; set; }
        public List<string> LinkResultsList { get; set; }
        public List<Resource> ResourcesList { get; set; }
        public List<Question> QuestionsList { get; set; }

        public void CreateSearchLists()

        {
            ResultsList = new List<List<string>>();
            foreach (var item in ResourcesList)
            {
                List<string> temp = new List<string>();
                temp.Add(item.Title);
                temp.Add(item.Link);
                temp.Add(item.Id);
                ResultsList.Add(temp);
            }
            foreach (var item in QuestionsList)
            {
                List<string> temp = new List<string>();
                temp.Add(item.Title);
                temp.Add("/Questions/Details/" + item.Id);
                temp.Add(item.Id);
                ResultsList.Add(temp);
            }

            
            //sort results alphabetically
            


        }
        
        
    }
}
