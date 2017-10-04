using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorWebApp.Controllers
{
    [Authorize("MustBeAdmin")]
    [Route("[controller]/[action]")]
    public class AllAnalyticsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public AllAnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {

            AllAnalytics analytics = new AllAnalytics(_context);
            analytics.GenerateAllAnalytics();
            
            return View(analytics);
        }
    }
}