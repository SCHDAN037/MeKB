using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/**
 * 
 * this is for fetching the all anaytics and rendering them
 * 
 * 
 */
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
            var analytics = new AllAnalytics(_context);
            analytics.GenerateAllAnalytics();

            return View(analytics);
        }
    }
}