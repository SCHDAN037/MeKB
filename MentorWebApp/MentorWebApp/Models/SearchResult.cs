using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


// models the result of a search made on the site
namespace MentorWebApp.Models
{
    public class SearchResult
    {
        [NotMapped]
        public List<List<string>> ResultsList { get; set; }
        [NotMapped]
        public List<string> LinkResultsList { get; set; }
        [NotMapped]
        public List<Resource> ResourcesList { get; set; }
        public List<Question> QuestionsList { get; set; }
        [NotMapped]
        public string searchVal { get; set; }
        [NotMapped]
        public string sortVal { get; set; }
        [NotMapped]
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
                        item.ResourceId
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
                        item.ResourceId
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
            var newResList = ResultsList;

            if (rev == null || rev == false)
                newResList.Sort((x, y) => string.Compare(x.FirstOrDefault(), y.FirstOrDefault()));
            else
                newResList.Sort((x, y) => string.Compare(y.FirstOrDefault(), x.FirstOrDefault()));
            ResultsList = newResList;
        }

        public void SortDate()
        {
        }
    }
}