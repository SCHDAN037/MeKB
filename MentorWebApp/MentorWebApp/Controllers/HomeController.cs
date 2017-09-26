using System.Diagnostics;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MentorWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var M = new MentorModel
            {
                Email = "s",
                UserName = "" +
                           "HERE IT IS U FUCKS" +
                           ""
            };

            Debug.WriteLine(M.UserName);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}