using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MentorWebApp.Models
{
    public class SearchResult
    {
        public SearchResult()
        {
            Analytic = new SearchAnalytic(Id);
        }

        [NotMapped]
        public List<List<string>> ResultsList { get; set; }

        [NotMapped]
        public List<Resource> ResourcesList { get; set; }

        [NotMapped]
        public List<Question> QuestionsList { get; set; }

        [Key]
        public string Id { get; set; }

        public string searchVal { get; set; }

        public SearchAnalytic Analytic { get; set; }

        public int NoOfResults { get; set; }

        [NotMapped]
        public string sortVal { get; set; }

        [NotMapped]
        public string typeVal { get; set; }

        public void CreateSearchLists(string type, string sort, string search)
        {
            ResultsList = new List<List<string>>();

            typeVal = type;
            sortVal = sort;
            searchVal = search;

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


            UpdateAnalytic();

            Sort(sort);
        }

        public void UpdateAnalytic()
        {
            Analytic.NoOfResults = ResultsList.Count;

            if (Analytic.NoOfResults == 0) Analytic.NoResultsCount++;
            Analytic.Count++;

            if (QuestionsList.Count != 0)
            {
                foreach (var question in QuestionsList)
                    question.Analytic.Count++;
            }
            if (ResourcesList.Count != 0)
            {
                foreach (var resource in ResourcesList)
                    resource.Analytic.Count++;
            }
        }


        public void Clicked(string id)
        {
            var tempRes = ResourcesList.SingleOrDefault(s => s.ResourceId == id);
            var tempQues = QuestionsList.SingleOrDefault(s => s.Id == id);
            if (tempRes != null)
                tempRes.Analytic.Clicks++;
            else if (tempQues != null)
                tempQues.Analytic.Clicks++;
        }

        public void Sort(string sort)
        {
            if (sort.Equals("alpha"))
                SortAlpha(false);
            else if (sort.Equals("alphaRev"))
                SortAlpha(true);
            else if (sort.Equals("date"))
                SortDate();
            else
                SortAlpha(false);
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