using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MentorWebApp.Models
{
    public class SearchResult
    {
        public List<List<string>> ResultsList { get; set; }
        public List<string> LinkResultsList { get; set; }
        public List<Resource> ResourcesList { get; set; }
        public List<Question> QuestionsList { get; set; }
        public string searchVal { get; set; }
        public string sortVal { get; set; }
        public string typeVal { get; set; }

        public void CreateSearchLists(string type)

        {
            
            ResultsList = new List<List<string>>();
            if (type.Equals("both"))
            {
                foreach (var item in ResourcesList)
                {
                    var temp = new List<string>
                    {
                        item.Title,
                        item.Link,
                        item.Id
                    };
                    //Add a type so we can see if RES or QUES
                    ResultsList.Add(temp);
                }
                foreach (var item in QuestionsList)
                {
                    var temp = new List<string>
                    {
                        item.Title,
                        "/Questions/Details/" + item.Id,
                        item.Id
                    };
                    ResultsList.Add(temp);
                }
            }
            else if (type.Equals("res"))
            {
                foreach (var item in ResourcesList)
                {
                    var temp = new List<string>
                    {
                        item.Title,
                        item.Link,
                        item.Id
                    };
                    //Add a type so we can see if RES or QUES
                    ResultsList.Add(temp);
                }
            }
            else
            {
                foreach (var item in QuestionsList)
                {
                    var temp = new List<string>
                    {
                        item.Title,
                        "/Questions/Details/" + item.Id,
                        item.Id
                    };
                    ResultsList.Add(temp);
                }
            }


            //sort results alphabetically
        }

        public void SortAlpha(bool? rev)
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

        public void SortDate()
        {
            
        }



    }
}