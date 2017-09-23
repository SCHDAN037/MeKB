using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MentorWebApp.Controllers
{
    public class SearchResultsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // recieve search from home search bar
        // create search query based on search
        // fetch resources from controller and models obv
        // search by tags
        // create search result list
        // send list to view


    }
}