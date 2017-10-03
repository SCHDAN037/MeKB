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
        private SearchResult resObject;


        public SearchResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> RedirectToLink(string title, string link, string id)
        {
            try
            {
                var tempRes = await _context.Resources.SingleOrDefaultAsync(s => s.ResourceId.Equals(id));

                if (String.IsNullOrEmpty(tempRes.Analytic.NewIdentity))
                {
                    tempRes.Analytic.NewIdentity = Guid.NewGuid().ToString();
                    tempRes.Analytic.ObjectId = tempRes.ResourceId;
                    tempRes.Analytic.Clicks++;
                    _context.Update(tempRes.Analytic);
                    _context.Update(tempRes);
                }
                else
                {
                    tempRes.Analytic.Clicks++;
                    _context.Update(tempRes.Analytic);
                    _context.Update(tempRes);
                }
                
                
            }
            catch (Exception)
            {
                try
                {
                    var tempQ = await _context.Questions.SingleOrDefaultAsync(s => s.Id.Equals(id));
                    if (String.IsNullOrEmpty(tempQ.Analytic.NewIdentity))
                    {
                        tempQ.Analytic.NewIdentity = Guid.NewGuid().ToString();
                        tempQ.Analytic.ObjectId = tempQ.Id;
                        tempQ.Analytic.Clicks++;
                        _context.Update(tempQ.Analytic);
                        _context.Update(tempQ);
                    }
                    else
                    {
                        tempQ.Analytic.Clicks++;
                        _context.Update(tempQ.Analytic);
                        _context.Update(tempQ);
                    }

                }
                catch
                {
                    // not sure... this means there is no results??
                }
            }
            try
            {
                var save = _context.SaveChanges();
            }
            catch (Exception)
            {
                
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
            var res = from r in _context.Resources
                select r;
            var ques = from q in _context.Questions
                select q;

            var tempRes = res;
            var tempQues = ques;
            if (String.IsNullOrEmpty(typeSelect)) typeSelect = "both";
            if (String.IsNullOrEmpty(sortSelect)) sortSelect = "alpha";
            if (String.IsNullOrEmpty(search)) search = "";
            
            resObject = new SearchResult(search, typeSelect, sortSelect);
            
            try
            {
                //var sVal = this.resObject.searchVal;
                //var tVal = this.resObject.typeVal;
                
                // Check if this search has been done before
                var before =
                    await _context.SearchResults.SingleAsync(id =>
                        (id.searchVal.Equals(search)) && (id.typeVal.Equals(typeSelect)));

                // make it the current search object
                this.resObject = before;
            }
            catch (Exception)
            {
                // if not new search object
                resObject.Id = Guid.NewGuid().ToString();
                resObject.Analytic.ObjectId = resObject.Id;
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
                resObject.ResourcesList = wait;
                var anotherWait = await tempQues.ToListAsync();
                resObject.QuestionsList = anotherWait;
                
                
            }

            else
            {
                //If search is Empty or Null
                resObject.searchVal = search;
                var wait = await tempRes.ToListAsync();
                resObject.ResourcesList = wait;
                var anotherWait = await tempQues.ToListAsync();
                resObject.QuestionsList = anotherWait;
                
            }

            resObject.CreateSearchLists();

            if (_context.SearchResults.Find(resObject.Id) != null)
            {
                var updateResAnal = _context.SearchAnalytics.Update(resObject.Analytic);
                var updateRes = _context.SearchResults.Update(resObject);
                
            }
            else
            {
                //var updateResAnal = _context.SearchAnalytics.Update(resObject.Analytic);
                var addRes = _context.SearchResults.Add(resObject);

                
                try
                {
                    var save = _context.SaveChanges();
                }
                catch (Exception)
                {
                    //whyyyyyyyyyyyyyyyy
                    var fail = true;
                }

            }
            
            return View(resObject);
        }
    }
}