using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MentorWebApp.Data;
using MentorWebApp.Models;

namespace MentorWebApp.Controllers
{
    public class SearchResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SearchResults
        public async Task<IActionResult> Index(string search)
        {
            var res = from r in _context.Resources
                select r;
            var ques = from q in _context.Questions
                select q;

            var tempRes = res;
            var tempQues = ques;

            //Debug.WriteLine("***********************************" + res.ToListAsync().Result.ToArray());

            if (!String.IsNullOrEmpty(search))
            {
                


                string[] words = search.Split(' ');
                int i = 0;
                string current = words[0];
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



                var final = tempRes.GroupJoin(tempQues, tempQues.Select(s => s.Title));
                    
                return View(final);


            }
            else
            {
                return View(await _context.Resources.ToListAsync());
            }

            //return View(await res.ToListAsync());
        }

        // GET: SearchResults/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        
    }
}
