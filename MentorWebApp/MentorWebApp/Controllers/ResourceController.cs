using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using MentorWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorWebApp.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public  ResourceController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public async Task<IActionResult> Index(string search)
        {
            var res = from r in _context.Resources
                select r;

            Debug.WriteLine("***********************************" + res.ToListAsync().Result.ToArray());

            if (!String.IsNullOrEmpty(search))
            {
                res = res.Where(s => s.Title.Contains(search));
                
            }
            else
            {
                return View(await _context.Resources.ToListAsync());
            }

            return View(await res.ToListAsync());

            //return View(await _context.Resources.ToListAsync());
        }

        
        // This is where we fetch the resource data
        // initialize the resource objects
        // update the database
        // send info to view page


    }
}