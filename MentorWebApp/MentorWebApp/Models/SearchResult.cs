using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MentorWebApp.Models
{
    public class SearchResult
    {
        public SearchResult(string id, string search, string type, string sort)
        {
            //using one that exists
            this.Id = id;
            this.Analytic = new SearchAnalytic(Id);
            this.searchVal = search;
            this.typeVal = type;
            this.sortVal = sort;
            this.ResourcesList = new List<Resource>();
            this.QuestionsList = new List<Question>();
        }

        public SearchResult(string search, string type, string sort)
        {
            //brand new one
            this.searchVal = search;
            this.typeVal = type;
            this.sortVal = sort;
            this.Analytic = new SearchAnalytic(this.Id);
            this.ResourcesList = new List<Resource>();
            this.QuestionsList = new List<Question>();
        }

        public SearchResult()
        {
            //one for EF to work
            this.Analytic = new SearchAnalytic(this.Id);

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

        
        public string sortVal { get; set; }

        public string typeVal { get; set; }

        public void CreateSearchLists()
        {
            ResultsList = new List<List<string>>();

            

            if (typeVal.Equals("both"))
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
            else if (typeVal.Equals("res"))
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
            UpdateAnalytic();
            Sort();
        }

        public void UpdateAnalytic()
        {
            
            this.Analytic.Count++;
            this.Analytic.NoOfResults = this.NoOfResults;
            if (this.Analytic.NoOfResults == 0)
            {
                this.Analytic.NoResultsCount++;
            }

        }


        public void Clicked(List<List<string>> list, int i)
        {
        }

        public void Sort()
        {
            if (sortVal.Equals("alpha"))
                SortAlpha(false);
            else if (sortVal.Equals("alphaRev"))
                SortAlpha(true);
            else if (sortVal.Equals("date"))
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