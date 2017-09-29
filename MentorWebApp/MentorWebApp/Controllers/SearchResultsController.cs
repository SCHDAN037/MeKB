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
        

        public SearchResultsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<IActionResult> Index(string search, string sortSelect, string typeSelect, string browse)
        {
            var res = from r in _context.Resources
                select r;
            var ques = from q in _context.Questions
                select q;

            var tempRes = res;
            var tempQues = ques;

            

            if (!string.IsNullOrEmpty(search))
            {
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

                var resObject = new SearchResult();


                var wait = await tempRes.ToListAsync();
                resObject.ResourcesList = wait;
                var anotherWait = await tempQues.ToListAsync();
                resObject.QuestionsList = anotherWait;

                do
                {
                    if (!string.IsNullOrEmpty(typeSelect))
                    {
                        resObject.CreateSearchLists(typeSelect);
                        if (!string.IsNullOrEmpty(sortSelect))
                            switch (sortSelect)
                            {
                                case "alpha":
                                    resObject.SortAlpha(false);
                                    break;
                                case "alphaRev":
                                    resObject.SortAlpha(true);
                                    break;
                                // add case for date/time here
                            }
                        else
                            resObject.SortAlpha(false);
                    }
                    else
                    {
                        resObject.CreateSearchLists("both");
                        if (!string.IsNullOrEmpty(sortSelect))
                            switch (sortSelect)
                            {
                                case "alpha":
                                    resObject.SortAlpha(false);
                                    break;
                                case "alphaRev":
                                    resObject.SortAlpha(true);
                                    break;
                                // add case for date/time here
                            }
                        else
                            resObject.SortAlpha(false);
                    }
                } while (false);

                resObject.searchVal = search;
                resObject.sortVal = sortSelect;
                resObject.typeVal = typeSelect;

                return View(resObject);
            }
            var browseAll = new SearchResult();
            do
            {
                var browseRes = await res.ToListAsync();
                var browseQues = await ques.ToListAsync();

                browseAll.ResourcesList = browseRes;
                browseAll.QuestionsList = browseQues;
                browseAll.sortVal = "alpha";


                if (!string.IsNullOrEmpty(browse))
                {
                    switch (browse)
                    {
                        case "res":
                            browseAll.typeVal = "res";
                            browseAll.CreateSearchLists("res");
                            break;
                        case "ques":
                            browseAll.typeVal = "ques";
                            browseAll.CreateSearchLists("ques");
                            break;
                    }
                }
                else
                {
                    browse = "null";
                    browseAll.CreateSearchLists("both");
                }
            } while (false);

            return View(browseAll);
        }
    }
}