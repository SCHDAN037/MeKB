using System;
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
            
            this.Id = Guid.NewGuid().ToString();
            this.Analytic = new SearchAnalytic(Id);
            this.ResourcesList = new List<Resource>();
            this.QuestionsList = new List<Question>();
        }

        [NotMapped]
        public List<List<string>> ResultsList { get; set; }

        [NotMapped]
        public List<Resource> ResourcesList { get; set; }

        [NotMapped]
        public List<Question> QuestionsList { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string searchVal { get; set; }

        public SearchAnalytic Analytic { get; set; }

        public int NoOfResults { get; set; }

        [NotMapped]
        public string sortVal { get; set; }
        
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

            this.NoOfResults = ResultsList.Count;
            //UpdateAnalytic();

            Sort(sort);
        }

       public void UpdateAnalytic()
        {
            //Analytic.NoOfResults = ResultsList.Count;
            //Analytic.NoOfResults
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


        public void Clicked(List<List<string>> list, int i)
        {
            
            
            //Update db now
            
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