using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MentorWebApp.Models
{
    public class SearchResult

    {
        //All variables being used in this object

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

        [NotMapped] private bool init = false;


        //public SearchResult(string id, string search, string type, string sort)
        //{
        //    //using one that exists
        //    this.Id = id;
        //    //this.Analytic = new SearchAnalytic(Id);
        //    this.searchVal = search;
        //    this.typeVal = type;
        //    this.sortVal = sort;
        //    this.ResourcesList = new List<Resource>();
        //    this.QuestionsList = new List<Question>();
        //}

        //public SearchResult(string search, string type, string sort)
        //{
        //    //brand new one
        //    this.searchVal = search;
        //    this.typeVal = type;
        //    this.sortVal = sort;
        //    //this.Analytic = new SearchAnalytic(this.Id);
        //    this.ResourcesList = new List<Resource>();
        //    this.QuestionsList = new List<Question>();
        //}

        //basic constructor

        public SearchResult()
        {
        }

        //Initialize mehod for creating a new result object after constructor
        public void Init(string search, string type, string sort)
        {
            this.searchVal = search;
            this.sortVal = sort;
            this.typeVal = type;
            this.Analytic = new SearchAnalytic
            {
                NoOfResults = 0,
                NoResultsCount = 0,
                SucceedClicks = 0,
                Count = 0
            };
            this.ResourcesList = new List<Resource>();
            this.QuestionsList = new List<Question>();
            this.ResultsList = new List<List<string>>();
            this.init = true;
        }

        public void CreateSearchLists()
        {
            //Make sure this object has all the values it needs initialized
            if(!init) throw new Exception();

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

            //Now update the analytics
            UpdateAnalytic();
            Sort();
        }


        //Method for updating the analytic object
        public void UpdateAnalytic()
        {
            //The search has been made another time
            this.Analytic.Count++;
            //This is the number of results
            this.Analytic.NoOfResults = this.NoOfResults;
            //If there are no results then count that
            if (this.Analytic.NoOfResults == 0)
            {
                this.Analytic.NoResultsCount++;
            }
        }

        

        //sort method thats called, sorts based on sortVal
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