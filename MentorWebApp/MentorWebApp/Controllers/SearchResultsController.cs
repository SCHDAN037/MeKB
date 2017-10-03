using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

        public async Task<ActionResult> RedirectToLink(string title, string link, string id)
        {


            bool isResource = !link.Contains("Questions/Details");

            try
            {
                if (isResource)
                {
                    // We looking for a resource to update its analytic
                    var tempRes = await _context.Resources.SingleOrDefaultAsync(s => s.ResourceId.Equals(id));
                    tempRes.Analytic.Clicks++;
                    _context.Update(tempRes);
                }
                else
                {
                    var tempQues = await _context.Questions.SingleOrDefaultAsync(s => s.Id.Equals(id));
                    tempQues.Analytic.Clicks++;
                    _context.Update(tempQues);
                }
                            }
            catch (Exception)
            {
                // Means the object we looking for doesnt exist
            }
            
            if (link.Contains("http"))
            {
                return Redirect(link);
            }
            else
            {
                return Redirect("http://" + link);
            }
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
            bool newSearch = false;
            //Make sure our search parameters are not null
            if (String.IsNullOrEmpty(typeSelect)) typeSelect = "both";
            if (String.IsNullOrEmpty(sortSelect)) sortSelect = "alpha";
            if (String.IsNullOrEmpty(search)) search = "";
            
            //Initialize a new search result object
            _resObject = new SearchResult();

            try
            {
                // Check if this search has been done before
                var searchResult =
                    _context.SearchResults.Single(s => s.searchVal.Equals(search) && s.typeVal.Equals(typeSelect));
                // If it has then use that object instead
                _resObject = searchResult;
            }
            catch (Exception)
            {
                // If it hasnt been done before we initialize a New object
                _resObject.Init(search, typeSelect, sortSelect);
                newSearch = true;
            }
            
            
            if (!string.IsNullOrEmpty(search))
            {
                //If search has a query
                //then search the db
                var words = search.Split(' ');
                var i = 0;
                string current = words[0];
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
                var save = _context.SaveChanges();
            }
            else
            {
                //If not new then update its entry
                //var updateResAnal = _context.SearchAnalytics.Update(_resObject.Analytic);
                var updateRes = _context.SearchResults.Update(_resObject);
                
            }
            
            return View(_resObject);
        }
    }
}