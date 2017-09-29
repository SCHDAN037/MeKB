using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MentorWebApp.Models
{
    public class SearchResult
    {
        
        public List<List<string>> ResultsList { get; set; }
        
        public List<Resource> ResourcesList { get; set; }
        public List<Question> QuestionsList { get; set; }
        
        public string searchVal { get; set; }
        
        public string sortVal { get; set; }
        
        public string typeVal { get; set; }

        public SearchResult()
        {
            
        }

        public void CreateSearchLists(string type, string sort, string search)
        {
            ResultsList = new List<List<string>>();
            this.typeVal = type;
            this.sortVal = sort;
            this.searchVal = search;

            if (type.Equals("both"))
            {
                foreach (var item in ResourcesList)
                {
                    var temp = new List<string>
                    {
                        item.Title,
                        item.Link,
                        item.ResourceId,
                        item.DateAdded.ToLongDateString()
                    };
                    
                    ResultsList.Add(temp);
                }
                foreach (var item in QuestionsList)
                {
                    var temp = new List<string>
                    {
                        item.Title,
                        "/Questions/Details/" + item.Id,
                        item.Id,
                        item.DatePosted.ToLongDateString()
                        
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
                        item.ResourceId,
                        item.DateAdded.ToLongDateString()
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
                        item.Id,
                        item.DatePosted.ToLongDateString()
                    };
                    ResultsList.Add(temp);
                }
            }







            Sort(sort);
            




        }

        public void Sort(string sort)
        {
            if (sort.Equals("alpha"))
            {
                SortAlpha(false);
            }
            else if (sort.Equals("alphaRev"))
            {
                SortAlpha(true);
            }
            else if (sort.Equals("date"))
            {
                SortDate();
            }
            else
            {
                SortAlpha(false);
            }
        }

        private void SortAlpha(bool? rev)
        {
            var newResList = ResultsList;

            if (rev == null || rev == false)
                newResList.Sort((x, y) => string.Compare(x.FirstOrDefault(), y.FirstOrDefault()));
            else
                newResList.Sort((x, y) => string.Compare(y.FirstOrDefault(), x.FirstOrDefault()));
            ResultsList = newResList;
        }

        private void SortDate()
        {
        }
    }
}