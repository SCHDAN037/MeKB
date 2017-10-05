﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorWebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        // adds a reply to a question and to the database
        public async Task<Question> DetailsAddReply(string id, string reply, string thisUserId, string thisUserUctNumber,
            [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted")] Question question)
        {
            if (ModelState.IsValid)
            {
                var r = new Reply(id, reply, thisUserUctNumber, thisUserId);
                var analytic = new ContentAnalytic(r.Id);
                r.Init(analytic);
                question.RepList.Add(r);
                question.NoOfReplies++;
                _context.Update(question);
                //_context.Update(r.Analytic);
                
                    await _context.SaveChangesAsync();
                
                
                return question;
            }
            return question;
        }

        //removes a reply from a question and from the database
        public async Task<Question> DetailsDeleteReply(string id,
            [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted,ApplicationId")] Question question)
        {
            if (ModelState.IsValid)
            {
                var rep = from r in _context.Replies
                    select r;
                var trep = rep.SingleOrDefault(s => s.Id.Equals(id));
                question.NoOfReplies--;

                _context.Replies.Remove(trep);
                _context.Update(question);

                await _context.SaveChangesAsync();

                return question;
            }
            return question;
        }

        public async Task<Question> DetailsVote(string id, int helpful,
            [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted")] Question question)
        {
            if (ModelState.IsValid)
            {
                var rep = from r in _context.Replies
                    select r;
                var trep = rep.SingleOrDefault(s => s.Id.Equals(id));
                var analytic = _context.ContentAnalytics.SingleOrDefault(s => s.ContentId == trep.Id);
                if (helpful == 1)
                    analytic.Helpful++;
                else if (helpful == -1)
                    analytic.UnHelpful++;

                _context.Update(analytic);
                await _context.SaveChangesAsync();
                return question;
            }
            return question;
        }

        public async Task<IActionResult> Details(string id, string reply, string delId, string voteId, int helpful)
        {
            if (id == null)
                return NotFound();

            //WE NEED THE LOGGED ON USER ID
            var loggedInUser = (await _userManager.GetUserAsync(HttpContext.User));
            var thisUserId = "";
            var thisUserUctNumber = "";
            bool loggedIn = false;
            if (loggedInUser != null)
            {
                thisUserId = loggedInUser.Id;
                thisUserUctNumber = loggedInUser.UctNumber;
                loggedIn = true;
            }

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();


            if (loggedIn)
            {
                if (!string.IsNullOrEmpty(reply))
                {
                    var temp = await DetailsAddReply(id, reply, thisUserId, thisUserUctNumber, question);
                    question = temp;
                }

                else if (!string.IsNullOrEmpty(delId))
                {
                    var temp = await DetailsDeleteReply(delId, question);
                    question = temp;
                }

                else if (helpful == 1 || helpful == -1)
                {
                    var temp = await DetailsVote(voteId, helpful, question);
                    question = temp;
                }
            }

            else
            {
                //Update this questions analytic ONLY IF THEY VISIT THE PAGE WITHOUT ADDING/DELETING A REPLY
                var analytic = await _context.ContentAnalytics.SingleOrDefaultAsync(m => m.ContentId == question.Id);
                analytic.Clicks++;
                _context.Update(analytic);
                _context.SaveChanges();
            }

            var rep = from r in _context.Replies
                select r;
            rep = rep.Where(s => s.QuestionId.Equals(id));
            var repList = await rep.ToListAsync();
            foreach (var replyEach in repList)
            {
                var analytic = _context.ContentAnalytics.SingleOrDefault(s => s.ContentId == replyEach.Id);
                replyEach.Analytic = analytic;
            }

            var sortedList = repList.OrderBy(x => x.DatePosted).ToList();
            question.RepList = sortedList;


            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Anonymous,MessageContent,Id,DatePosted,Title,Tags")] Question question)
        {
            var loggedInUser = (await _userManager.GetUserAsync(HttpContext.User));
            var thisUserId = "";
            var thisUserUctNumber = "";
            bool loggedIn = false;
            if (loggedInUser != null)
            {
                thisUserId = loggedInUser.Id;
                thisUserUctNumber = loggedInUser.UctNumber;
                loggedIn = true;
            }

            if (ModelState.IsValid && loggedIn)
            {
                var analytic = new ContentAnalytic(question.Id);
                question.Init(analytic);
                question.ApplicationUserId = thisUserId;
                question.UctNumber = thisUserUctNumber;
                _context.Add(question);
                _context.Add(analytic);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();
            return View(question);
        }

        // POST: Questions/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            [Bind("Anonymous,Title,MessageContent,Id,UctNumber,DatePosted")] Question question)
        {
            if (id != question.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rep = from r in _context.Replies
                select r;
            rep = rep.Where(s => s.QuestionId.Equals(id));
            var tempRep = await rep.ToListAsync();
            //var noOfReplies = tempRep.Count;
            foreach (var reply in tempRep)

                _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(string id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}