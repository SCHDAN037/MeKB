﻿using System;
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

        public ResourceController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string search)
        {
            var res = from r in _context.Resources
                select r;

            var tempRes = res;

            //Debug.WriteLine("***********************************" + res.ToListAsync().Result.ToArray());

            if (!String.IsNullOrEmpty(search))
            {
                
                string[] words = search.Split(' ');
                int i = 0;
                string current = words[0];
                tempRes = res.Where(s => s.Tags.Contains(current));
                i++;
                while (i <= words.Length - 1)
                {
                    current = words[i];
                    tempRes = tempRes.Intersect(res.Where(s => s.Tags.Contains(current)));
                    i++;
                }

                tempRes = tempRes.Union(res.Where(s => s.Title.Contains(search)));


                var final = await tempRes.ToListAsync();
                return View(final);
            }

            return View(await _context.Resources.ToListAsync());
        }
    }
}