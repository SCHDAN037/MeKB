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
        public List<Resource> ResourcesList { get; set; }
        public List<Question> QuestionsList { get; set; }

        public List<string> CreateSearchList()

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

            return TitleResultsList;
        }
        
        
    }
}
