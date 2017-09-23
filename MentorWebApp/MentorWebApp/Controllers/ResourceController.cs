using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MentorWebApp.Models;

namespace MentorWebApp.Controllers
{
    public class ResourceController : Controller
    {
        public IActionResult Index()
        {
            

            return View();
        }


        // This is where we fetch the resource data
        // initialize the resource objects
        // update the database
        // send info to view page


    }
}