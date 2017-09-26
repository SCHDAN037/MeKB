using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
                var temp = new List<string>();
                temp.Add(item.Title);
                temp.Add(item.Link);
                temp.Add(item.Id);
                //Add a type so we can see if RES or QUES
                ResultsList.Add(temp);
            }
            foreach (var item in QuestionsList)
            {
                var temp = new List<string>();
                temp.Add(item.Title);
                temp.Add("/Questions/Details/" + item.Id);
                temp.Add(item.Id);
                ResultsList.Add(temp);
            }


            //sort results alphabetically
        }

        public void sortAlpha(bool? rev)
        {

            List<List<string>> newResList = ResultsList;

            if (rev == null || rev == false)
            {
                //sort a to z
               

                newResList.Sort((x, y) => String.Compare(x.FirstOrDefault(), y.FirstOrDefault()));

                
            }
            else
            {
                //sort z to a

                newResList.Sort((x, y) => String.Compare(y.FirstOrDefault(), x.FirstOrDefault()));

            }
            ResultsList = newResList;
        }

        public void sortDate()
        {
            
        }



    }
}