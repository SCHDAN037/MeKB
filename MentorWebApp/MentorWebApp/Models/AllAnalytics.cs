using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MentorWebApp.Models
{
    public class AllAnalytics
    {
        
        //By Descending Login Count
        [NotMapped]
        public List<ApplicationUser> Top5Users { get; set; }

        //By Descending Search Count
        [NotMapped]
        public List<SearchResult> Top5ViewedSearches { get; set; }

        //By Descending Succeed Count
        [NotMapped]
        public List<SearchResult> Top5SuccessfulSearches { get; set; }

        //By Ascending Succeed Count
        [NotMapped]
        public List<SearchResult> Worst5SuccessfulSearches { get; set; }

        //By Descending Click Count
        [NotMapped]
        public List<Resource> Top5Resources { get; set; }

        //By Ascending Click Count
        [NotMapped]
        public List<Resource> Worst5Resources { get; set; }

        //By Descending Highest Helpful Count
        [NotMapped]
        public List<Question> Top5Questions { get; set; }

        //By Descending Highest UnHelpful Count
        [NotMapped]
        public List<Question> Worst5Questions { get; set; }

        //Sunday = 0, Monday = 1, etc...
        [NotMapped]
        public List<int> LoginsPerDay { get; set; }

        private readonly ApplicationDbContext _context;

        public AllAnalytics(ApplicationDbContext context)
        {
            _context = context;
            
            Top5Users = new List<ApplicationUser>();

            Top5ViewedSearches = new List<SearchResult>();
            Top5SuccessfulSearches = new List<SearchResult>();
            Worst5SuccessfulSearches = new List<SearchResult>();

            Top5Resources = new List<Resource>();
            Worst5Resources = new List<Resource>();

            Top5Questions = new List<Question>();
            Worst5Questions = new List<Question>();

            LoginsPerDay = new List<int>
            {
                //starts on sunday
                0,0,0,0,0,0,0
            };
        }

        public void GenerateAllAnalytics()
        {
            // all the analytics at this time
            
            GenerateUserAnalytics();
            GenerateSearchAnalytics();
            GenerateContentAnalytics();
            //GenerateLoginAnalytics();
        }

        

        public void GenerateUserAnalytics()
        {
            var userAnalytics = from u in _context.UserAnalytics
                select u;

            //Top 5 users
            var orderByCount = userAnalytics.OrderByDescending(s => s.Count).ToList();
            for (int i = 0; (i < orderByCount.Count) && (i < 5); i++)
            {
                var thisUser = _context.ApplicationUser.SingleOrDefault(s => s.Id == orderByCount[i].UserId);
                Top5Users.Add(thisUser);
            }

            //this weeks logins per day
            foreach (var userAnalytic in userAnalytics.ToList())
            {
                var thisUserWeek = userAnalytic.GetWeekCheck();
                for (int i = 0; i < 7; i++)
                {
                    if (thisUserWeek[i]) LoginsPerDay[i]++;
                }
            }
            
        }

        public void GenerateSearchAnalytics()
        {
            var searchAnalytics = from s in _context.SearchAnalytics
                select s;

            //Top 5 searches by views
            var orderByCount = searchAnalytics.OrderByDescending(s => s.Count).ToList();

            for (int i = 0; (i < orderByCount.Count) && (i < 5); i++)
            {
                var thisSearch = _context.SearchResults.SingleOrDefault(s => s.Id == orderByCount[i].SearchResultId);
                Top5ViewedSearches.Add(thisSearch);
            }

            //Top 5 successful searches



        }

        public void GenerateContentAnalytics()
        {
            var contentAnalytics = from c in _context.ContentAnalytics
                select c;
        }
    }
}