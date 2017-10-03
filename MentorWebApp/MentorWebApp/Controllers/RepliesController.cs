using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MentorWebApp.Controllers
{
    public class RepliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Replies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Replies.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var reply = await _context.Replies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reply == null)
                return NotFound();

            return View(reply);
        }

        // GET: Replies/Create
        public IActionResult Create(string qid)
        {
            var r = new Reply();
            r.QuestionId = qid;
            return View(r);
        }

        // POST: Replies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,MessageContent,Id,UserId,DatePosted")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reply);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var reply = await _context.Replies.SingleOrDefaultAsync(m => m.Id == id);
            if (reply == null)
                return NotFound();
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            [Bind("QuestionId,MessageContent,Id,UserId,DatePosted")] Reply reply)
        {
            if (id != reply.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reply);
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var reply = await _context.Replies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (reply == null)
                return NotFound();

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var reply = await _context.Replies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Questions");
        }

        private bool ReplyExists(string id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }
    }
}