using System;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorWebApp.Controllers
{
    public class SearchResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private SearchResult _resObject;


        public SearchResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> RedirectToLink(string title, string link, string id, string searchId)
        {
            var isResource = !link.Contains("Questions/Details");


            if (isResource)
            {
                // We looking for a resource to update its analytic
                var tempRes = await _context.Resources.SingleOrDefaultAsync(s => s.ResourceId.Equals(id));
                var tempAnalytic =
                    await _context.ContentAnalytics.SingleOrDefaultAsync(
                        s => s.ContentId.Equals(tempRes.ResourceId));
                tempAnalytic.Clicks++;
                _context.Update(tempRes);
                _context.Update(tempAnalytic);
                if (!link.Contains("http")) link = "http://" + link;
            }
            else
            {
                //update question analytics
                var tempQues = await _context.Questions.SingleOrDefaultAsync(s => s.Id.Equals(id));
                var tempAnalytic =
                    await _context.ContentAnalytics.SingleOrDefaultAsync(
                        s => s.ContentId.Equals(tempQues.Id));
                tempAnalytic.Clicks++;

                _context.Update(tempQues);
                _context.Update(tempAnalytic);

                return RedirectToAction("Details", "Questions", tempQues);
            }

            //update the succeed count for this search
            var searchResult = await _context.SearchResults.SingleOrDefaultAsync(s => s.Id == searchId);
            var searchAnalytic =
                await _context.SearchAnalytics.SingleOrDefaultAsync(s => s.SearchResultId == searchResult.Id);
            searchAnalytic.SucceedClicks++;
            _context.Update(searchResult);
            _context.Update(searchAnalytic);

            _context.SaveChanges();

            return Redirect(link);
        }

        public async Task<IActionResult> Index(string search, string sortSelect, string typeSelect)
        {
            //Gather data from database to search
            var res = from r in _context.Resources
                select r;
            var ques = from q in _context.Questions
                select q;

            var tempRes = res;
            var tempQues = ques;
            var newSearch = false;

            //Make sure our search parameters are not null
            if (string.IsNullOrEmpty(typeSelect)) typeSelect = "res";
            if (string.IsNullOrEmpty(sortSelect)) sortSelect = "alpha";
            if (string.IsNullOrEmpty(search)) search = "";
            search = search.ToLower();

            //Initialize a new search result object
            try
            {
                // Check if this search has been done before
                var searchResult =
                    _context.SearchResults.Single(s => s.searchVal.Equals(search) && s.typeVal.Equals(typeSelect) && s.sortVal.Equals(sortSelect));
                // If it has then use that object instead
                var searchAnalytic = _context.SearchAnalytics.Single(s => s.SearchResultId.Equals(searchResult.Id));
                _resObject = searchResult;
                _resObject.Init(search, typeSelect, sortSelect, searchAnalytic);
            }
            catch (Exception)
            {
                // If it hasnt been done before we initialize a New object
                _resObject = new SearchResult();
                var newAnalytic = new SearchAnalytic(_resObject.Id);
                _resObject.Init(search, typeSelect, sortSelect, newAnalytic);
                newSearch = true;
            }


            if (!string.IsNullOrEmpty(search))
            {
                //If search has a query
                //then search the db
                var words = search.Split(' ');
                var i = 0;
                var current = words[0];
                tempRes = res.Where(s => s.Tags.Contains(current));
                tempQues = ques.Where(s => s.Tags.Contains(current));
                i++;
                while (i <= words.Length - 1)
                {
                    current = words[i];
                    tempRes = tempRes.Union(res.Where(s => s.Tags.Contains(current)));
                    tempQues = tempQues.Union(ques.Where(s => s.Tags.Contains(current)));
                    i++;
                }

                tempRes = tempRes.Union(res.Where(s => s.Title.Contains(search)));
                tempQues = tempQues.Union(ques.Where(s => s.Title.Contains(search)));

                //Adding all results to the res object

                var wait = await tempRes.ToListAsync();
                _resObject.ResourcesList = wait;
                var anotherWait = await tempQues.ToListAsync();
                _resObject.QuestionsList = anotherWait;
            }

            else
            {
                //If search is Empty or Null
                _resObject.searchVal = search;
                var wait = await tempRes.ToListAsync();
                _resObject.ResourcesList = wait;
                var anotherWait = await tempQues.ToListAsync();
                _resObject.QuestionsList = anotherWait;
            }

            _resObject.CreateSearchLists();

            if (newSearch)
            {
                // if its new search then add the record to the db
                var addRes = _context.SearchResults.Add(_resObject);
                var addAnalytic = _context.SearchAnalytics.Add(_resObject.Analytic);
                var save = _context.SaveChanges();
            }
            else
            {
                //If not new then update its entry
                //var updateResAnal = _context.SearchAnalytics.Update(_resObject.Analytic);
                var updateRes = _context.SearchResults.Update(_resObject);
                var updateAnalytic = _context.SearchAnalytics.Update(_resObject.Analytic);
            }
            _context.SaveChanges();
            return View(_resObject);
        }
    }
}