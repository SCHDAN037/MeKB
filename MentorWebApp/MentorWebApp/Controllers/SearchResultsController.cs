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

        public ActionResult RedirectToLink(string title, string link, string id)
        {

            try
            {
                var tempRes = _context.Resources.SingleOrDefault(s => s.ResourceId == id);
                tempRes.Analytic.Clicks++;
            }
            catch (Exception)
            {
                try
                {
                    var tempQues = _context.Questions.SingleOrDefault(s => s.Id == id);
                    tempQues.Analytic.Clicks++;
                }
                catch
                {
                    // not sure... this means there is no results??
                }
            }
            _context.SaveChanges();
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

            // Check if this search has been done before
            try
            {
                var before =
                    await _context.SearchResults.SingleAsync(id =>
                        (id.searchVal == search) && (id.typeVal == typeSelect));
                this.resObject = before;
            }
            catch (Exception)
            {
                resObject = new SearchResult();
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


                if (!string.IsNullOrEmpty(typeSelect))
                {
                    //if type is NOT null or empty
                    if (!string.IsNullOrEmpty(sortSelect))
                    {
                        //if sort is NOT null or empty
                        resObject.CreateSearchLists(typeSelect, sortSelect, search);
                    }
                    else
                    {
                        //if sort is empty
                        resObject.CreateSearchLists(typeSelect, "alpha", search);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sortSelect))
                    {
                        //if sort is NOT null or empty
                        resObject.CreateSearchLists("both", sortSelect, search);
                    }
                    else
                    {
                        //if sort is empty
                        resObject.CreateSearchLists("both", "alpha", search);
                    }
                    
                }
            }

            else
            {
                //If search is Empty or Null
                search = "";
                var wait = await tempRes.ToListAsync();
                resObject.ResourcesList = wait;
                var anotherWait = await tempQues.ToListAsync();
                resObject.QuestionsList = anotherWait;

                if (!string.IsNullOrEmpty(sortSelect))
                {
                    //If search is NOT empty
                    if (!string.IsNullOrEmpty(typeSelect))
                    {
                        //If type is NOT empty
                        resObject.CreateSearchLists(typeSelect, sortSelect, "");
                    }
                    else
                    {
                        //if type is empty
                        resObject.CreateSearchLists("both", sortSelect, "");
                    }
                }
                else
                {
                    //if sort is empty
                    if (!string.IsNullOrEmpty(typeSelect))
                    {
                        //If type is NOT empty
                        resObject.CreateSearchLists(typeSelect, "alpha", "");
                    }
                    else
                    {
                        //if type is empty
                        resObject.CreateSearchLists("both", "alpha", "");
                    }
                }
            }

            resObject.Analytic.Count++;
            resObject.Analytic.NoOfResults = resObject.NoOfResults;
            if (resObject.Analytic.NoOfResults == 0)
            {
                resObject.Analytic.NoResultsCount++;
            }
            
            if (_context.SearchResults.Find(resObject.Id) != null)
            {
                var updateRes = _context.SearchResults.Update(resObject);
            }
            else
            {
                var addRes = await _context.SearchResults.AddAsync(resObject);
            }

            _context.SaveChanges();
            return View(resObject);
        }
    }
}