﻿using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorWebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.ToListAsync());
        }

        // GET: Questions/Details/5
        // adds a reply to a question and to the database
        public async Task<Question> DetailsAddReply(string id, string reply, [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted")] Question question)
        {


            if (ModelState.IsValid)
            {

                Reply r = new Reply(id, reply, "bob");
                question.Replies.Add(r);
                _context.Update(question);
                await _context.SaveChangesAsync();

                return question;
            }
            return question;
        }

        //removes a reply from a question and from the database
        public async Task<Question> DetailsDeleteReply(string id, [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted")] Question question)
        {
           

            if (ModelState.IsValid)
            {
                var rep = from r in _context.Replies
                          select r;
                var trep = rep.SingleOrDefault(s => (s.Id.Equals(id)));
                _context.Replies.Remove(trep);
                await _context.SaveChangesAsync();

                return question;
            }
            return question;
        }



        public async Task<IActionResult> Details(string id, string reply, string delId)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();
            //question.CreateReply(reply);



            

            if (!string.IsNullOrEmpty(reply))
            {
                var temp = await DetailsAddReply(id, reply, question);
                question = temp;
            }

            if (!string.IsNullOrEmpty(delId))
            {
                var temp = await DetailsDeleteReply(delId, question);
                question = temp;
            } 

            var rep = from r in _context.Replies
                      select r;
            rep = rep.Where(s => s.QuestionId.Equals(id));
            var repList = await rep.ToListAsync();
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
            [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted,Title,Tags")] Question question)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(question);
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
            [Bind("Anonymous,MessageContent,Id,UctNumber,DatePosted")] Question question)
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
            foreach(var reply in tempRep)
            {
                _context.Replies.Remove(reply);
            }
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