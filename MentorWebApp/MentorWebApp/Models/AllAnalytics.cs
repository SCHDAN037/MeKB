using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MentorWebApp.Data;

/**
 * 
 * This is an object that is used to generate all the analytics for the site
 * it returns lists of data to the view
 * 
 * 
 */
namespace MentorWebApp.Models
{
    public class AllAnalytics
    {
        private readonly ApplicationDbContext _context;

        public AllAnalytics(ApplicationDbContext context)
        {
            _context = context;

            Top5Users = new List<ApplicationUser>();

            Top5ViewedSearches = new List<SearchResult>();
            Top5SuccessfulSearches = new List<SearchResult>();
            Worst5UnsuccessfulSearches = new List<SearchResult>();
            Top5NoResultsSearches = new List<SearchResult>();


            Top5Resources = new List<Resource>();
            Worst5Resources = new List<Resource>();

            Top5Questions = new List<Question>();
            Worst5Questions = new List<Question>();
            Top5RepliedQuestions = new List<Question>();
            LoginsPerDay = new List<int>
            {
                //starts on sunday
                0,
                0,
                0,
                0,
                0,
                0,
                0
            };
            NumberOfUnansweredQuestions = 0;
            NumberOfQuestions = 0;
            NumberOfResources = 0;
            NumberOfUsers = 0;
            NumberOfAdmins = 0;
            NumberOfMentors = 0;
            NumberOfMentees = 0;
        }

        //Number of Users
        [NotMapped]
        public int NumberOfUsers { get; set; }

        //Number of Admins
        [NotMapped]
        public int NumberOfAdmins { get; set; }

        //Number of Mentors
        [NotMapped]
        public int NumberOfMentors { get; set; }

        //Number of Mentees
        [NotMapped]
        public int NumberOfMentees { get; set; }

        //Number of Resources
        [NotMapped]
        public int NumberOfResources { get; set; }

        //Number of Questions
        [NotMapped]
        public int NumberOfQuestions { get; set; }

        //Number of Unanswered Questions
        [NotMapped]
        public int NumberOfUnansweredQuestions { get; set; }

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
        public List<SearchResult> Worst5UnsuccessfulSearches { get; set; }

        //By Descending Search Count where no results found
        [NotMapped]
        public List<SearchResult> Top5NoResultsSearches { get; set; }

        //By Descending Click Count
        [NotMapped]
        public List<Resource> Top5Resources { get; set; }

        //By Ascending Click Count
        [NotMapped]
        public List<Resource> Worst5Resources { get; set; }

        //By Descending Highest Helpful Count
        [NotMapped]
        public List<Question> Top5Questions { get; set; }

        //By Descending Highest Number of Replies
        [NotMapped]
        public List<Question> Top5RepliedQuestions { get; set; }

        //By Descending Highest UnHelpful Count
        [NotMapped]
        public List<Question> Worst5Questions { get; set; }

        //Sunday = 0, Monday = 1, etc...
        [NotMapped]
        public List<int> LoginsPerDay { get; set; }

        public void GenerateAllAnalytics()
        {
            // all the analytics at this time

            GenerateUserAnalytics();
            GenerateSearchAnalytics();
            GenerateContentAnalytics();
        }


        public void GenerateUserAnalytics()
        {
            var users = from u in _context.ApplicationUser select u;

            //Number of Users
            NumberOfUsers = users.Count();
            NumberOfMentees = users.Count(s => s.Permissions == "Mentee");
            NumberOfMentors = users.Count(s => s.Permissions == "Mentor");
            NumberOfAdmins = users.Count(s => s.Permissions == "Admin");

            var userAnalytics = from u in _context.UserAnalytics
                select u;

            //Top 5 users
            var orderByCount = userAnalytics.OrderByDescending(s => s.Count).ToList();
            for (var i = 0; i < orderByCount.Count && i < 5; i++)
            {
                var thisUser = _context.ApplicationUser.SingleOrDefault(s => s.Id == orderByCount[i].UserId);
                Top5Users.Add(thisUser);
            }

            //this weeks logins per day
            foreach (var userAnalytic in userAnalytics.ToList())
            {
                var thisUserWeek = userAnalytic.GetWeekCheck();
                for (var i = 0; i < 7; i++)
                    if (thisUserWeek[i]) LoginsPerDay[i]++;
            }
        }

        public void GenerateSearchAnalytics()
        {
            var searchAnalytics = from s in _context.SearchAnalytics
                select s;

            //Top 5 searches by views
            //We do not include the blank searches. or duplica


            var orderByCount = searchAnalytics.OrderByDescending(s => s.Count).ToList();

            for (var i = 0; i < orderByCount.Count && i < 5; i++)
            {
                var thisSearch = _context.SearchResults.SingleOrDefault(s =>
                    s.Id == orderByCount[i].SearchResultId && s.searchVal != null && s.searchVal != "");
                if (thisSearch != null) Top5ViewedSearches.Add(thisSearch);
            }

            //Top 5 successful searches

            var orderBySuccess = searchAnalytics.OrderByDescending(s => s.SucceedClicks).ToList();

            for (var i = 0; i < orderBySuccess.Count && i < 5; i++)
            {
                var thisSearch = _context.SearchResults.SingleOrDefault(s =>
                    s.Id == orderBySuccess[i].SearchResultId && s.searchVal != null && s.searchVal != "");
                if (thisSearch != null) Top5SuccessfulSearches.Add(thisSearch);
            }

            //Worst 5 searches (Both unsuccessful and in order of views)

            var reverseOrderBySuccess = searchAnalytics.Where(s => s.SucceedClicks == 0);
            reverseOrderBySuccess = reverseOrderBySuccess.OrderBy(s => s.Count);
            var worstSearches = reverseOrderBySuccess.ToList();

            for (var i = 0; i < worstSearches.Count && i < 5; i++)
            {
                var thisSearch = _context.SearchResults.SingleOrDefault(s =>
                    s.Id == worstSearches[i].SearchResultId && s.searchVal != null && s.searchVal != "");
                if (thisSearch != null) Worst5UnsuccessfulSearches.Add(thisSearch);
            }

            //Top 5 searches with no results

            var orderByViewsNoSResults =
                searchAnalytics.Where(s => s.NoOfResults == 0).OrderByDescending(s => s.Count).ToList();

            for (var i = 0; i < orderByViewsNoSResults.Count && i < 5; i++)
            {
                var thisSearch = _context.SearchResults.SingleOrDefault(s =>
                    s.Id == orderByViewsNoSResults[i].SearchResultId && s.searchVal != null && s.searchVal != "");
                if (thisSearch != null) Top5NoResultsSearches.Add(thisSearch);
            }
        }

        public void GenerateContentAnalytics()
        {
            //var contentAnalytics = from c in _context.ContentAnalytics
            //    select c;
            var resources = from r in _context.Resources select r;
            var questions = from r in _context.Questions select r;

            NumberOfQuestions = questions.Count();
            NumberOfUnansweredQuestions = questions.Count(s => s.NoOfReplies == 0);
            NumberOfResources = resources.Count();

            var resourceAnalytics = from r in _context.Resources where r.Analytic != null select r.Analytic;
            var questionAnalytics = from q in _context.Questions where q.Analytic != null select q.Analytic;

            //Resources

            //Top 5 resources by click count

            var rOrderByClicks = resourceAnalytics.OrderByDescending(s => s.Clicks).ToList();

            for (var i = 0; i < rOrderByClicks.Count && i < 5; i++)
            {
                var thisResource = _context.Resources.SingleOrDefault(s => s.ResourceId == rOrderByClicks[i].ContentId);
                Top5Resources.Add(thisResource);
            }

            //Worst 5 resources by click count

            var rReverseOrderByClicks = resourceAnalytics.OrderBy(s => s.Clicks).ToList();

            for (var i = 0; i < rReverseOrderByClicks.Count && i < 5; i++)
            {
                var thisResource =
                    _context.Resources.SingleOrDefault(s => s.ResourceId == rReverseOrderByClicks[i].ContentId);
                Worst5Resources.Add(thisResource);
            }

            //Questions

            //Top 5 questions by helpful vote

            var qOrderByHelpful = questionAnalytics.OrderByDescending(s => s.Helpful).ToList();

            for (var i = 0; i < qOrderByHelpful.Count && i < 5; i++)
            {
                var thisQuestion = _context.Questions.SingleOrDefault(s => s.Id == qOrderByHelpful[i].ContentId);
                thisQuestion.RepList = _context.Replies.Where(s => s.QuestionId == thisQuestion.Id).ToList();
                Top5Questions.Add(thisQuestion);
            }

            //MAKE A TOP 5 LIST OF MOST REPLIED TO QUESTIONS

            var qOrderByNoOfReplies = _context.Questions.OrderByDescending(s => s.NoOfReplies).ToList();

            for (var i = 0; i < qOrderByNoOfReplies.Count && i < 5; i++)
                Top5RepliedQuestions.Add(qOrderByNoOfReplies[i]);

            //Worst 5 questions by vote

            var qOrderByUnHelpful = questionAnalytics.OrderBy(s => s.UnHelpful).ToList();

            for (var i = 0; i < qOrderByUnHelpful.Count && i < 5; i++)
            {
                var thisQuestion = _context.Questions.SingleOrDefault(s => s.Id == qOrderByUnHelpful[i].ContentId);
                thisQuestion.RepList = _context.Replies.Where(s => s.QuestionId == thisQuestion.Id).ToList();
                Worst5Questions.Add(thisQuestion);
            }
        }
    }
}