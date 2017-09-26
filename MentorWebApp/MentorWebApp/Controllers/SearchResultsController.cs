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
        //public SearchResult _sr;

        public SearchResultsController(ApplicationDbContext context)
        {
            _context = context;
            //_sr = new SearchResult();
        }

        public async Task<IActionResult> Index(string search, string sortSelect)
        {
            var res = from r in _context.Resources
                select r;
            var ques = from q in _context.Questions
                select q;

            var tempRes = res;
            var tempQues = ques;

            //Debug.WriteLine("***********************************" + res.ToListAsync().Result.ToArray());

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
                    tempRes = tempRes.Intersect(res.Where(s => s.Tags.Contains(current)));
                    tempQues = tempQues.Intersect(ques.Where(s => s.Tags.Contains(current)));
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
                    resObject.CreateSearchLists();
                    if (!string.IsNullOrEmpty(sortSelect))
                    {
                        switch (sortSelect)
                        {
                            case "alpha":
                                resObject.sortAlpha(false);
                                break;
                            case "alphaRev":
                                resObject.sortAlpha(true);
                                break;
                                
                        }
                        
                    }
                    else
                    {
                        resObject.sortAlpha(false);
                    }
                    
                } while (false);

                return View(resObject);
            }


            return View(new SearchResult());
        }
    }
}