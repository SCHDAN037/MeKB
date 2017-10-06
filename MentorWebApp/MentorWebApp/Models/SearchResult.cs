using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

/**
 * 
 * this search object generates the search results based on the quiries inputted
 * 
 * also tracks the analytic object
 * 
 */

namespace MentorWebApp.Models
{
    public class SearchResult

    {
        [NotMapped] public bool init;

        //basic constructor
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

        //public string SearchAnalyticId { get; set; }
        public string searchVal { get; set; }

        [NotMapped]
        public string AnalyticNewIdentity { get; set; }

        public SearchAnalytic Analytic { get; set; }

        public int NoOfResults { get; set; }
        public string sortVal { get; set; }
        public string typeVal { get; set; }

        //Initialize mehod for creating a new result object after constructor
        public void Init(string search, string type, string sort, SearchAnalytic analytic)
        {
            searchVal = search;
            sortVal = sort;
            typeVal = type;
            Analytic = analytic;
            AnalyticNewIdentity = analytic.NewIdentity;
            ResourcesList = new List<Resource>();
            QuestionsList = new List<Question>();
            ResultsList = new List<List<string>>();
            init = true;
        }

        public void CreateSearchLists()
        {
            //Make sure this object has all the values it needs initialized
            if (!init) throw new Exception();

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

            NoOfResults = ResultsList.Count;

            //Now update the analytics
            UpdateAnalytic();
            Sort();
        }


        //Method for updating the analytic object
        public void UpdateAnalytic()
        {
            //The search has been made another time
            Analytic.Count++;
            //This is the number of results
            Analytic.NoOfResults = NoOfResults;
            //If there are no results then count that
            if (Analytic.NoOfResults == 0)
                Analytic.NoResultsCount++;
        }


        //sort method thats called, sorts based on sortVal
        public void Sort()
        {
            if (sortVal.Equals("alpha"))
                SortAlpha(false);
            else if (sortVal.Equals("alphaRev"))
                SortAlpha(true);
            else if (sortVal.Equals("new"))
                SortDate(true);
            else if (sortVal.Equals("old"))
                SortDate(false);
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

        private void SortDate(bool? newFirst)
        {
            var newResList = ResultsList;

            if (newFirst == null || newFirst == true)
                newResList.Sort((x, y) =>
                    DateTime.Compare(DateTime.Parse(x.LastOrDefault()), DateTime.Parse(y.LastOrDefault())));
            else
                newResList.Sort((x, y) =>
                    DateTime.Compare(DateTime.Parse(y.LastOrDefault()), DateTime.Parse(x.LastOrDefault())));
            ResultsList = newResList;
        }
    }
}