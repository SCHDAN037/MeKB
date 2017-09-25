using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentorWebApp.Models
{
    public class SearchResult
    {
        public List<string> TitleResultsList { get; set; }
        public List<string> LinkResultsList { get; set; }
        public List<Resource> ResourcesList { get; set; }
        public List<Question> QuestionsList { get; set; }

        public void CreateSearchLists()

        {
            TitleResultsList = new List<string>();
            foreach (var item in ResourcesList)
            {
                TitleResultsList.Add(item.Title);
            }
            foreach (var item in QuestionsList)
            {
                TitleResultsList.Add(item.Title);
            }

            //sort results alphabetically
            TitleResultsList.Sort();

            LinkResultsList = new List<string>();
            foreach (var item in ResourcesList)
            {
                LinkResultsList.Add(item.Link);
            }
            foreach (var item in QuestionsList)
            {
                LinkResultsList.Add(item.Link);
            }

            //sort results alphabetically
            TitleResultsList.Sort();


        }
        
        
    }
}
