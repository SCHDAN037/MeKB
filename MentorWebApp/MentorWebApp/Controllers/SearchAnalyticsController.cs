using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MentorWebApp.Data;
using MentorWebApp.Models;

namespace MentorWebApp.Controllers
{
    public class SearchAnalyticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchAnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SearchAnalytics
        public async Task<IActionResult> Index()
        {
            return View(await _context.SearchAnalytics.ToListAsync());
        }

        // GET: SearchAnalytics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchAnalytic = await _context.SearchAnalytics
                .SingleOrDefaultAsync(m => m.Id == id);
            if (searchAnalytic == null)
            {
                return NotFound();
            }

            return View(searchAnalytic);
        }

        // GET: SearchAnalytics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SearchAnalytics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoOfResults,SucceedClicks,NoResultsCount,Id,Count,ObjectId")] SearchAnalytic searchAnalytic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(searchAnalytic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(searchAnalytic);
        }

        // GET: SearchAnalytics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchAnalytic = await _context.SearchAnalytics.SingleOrDefaultAsync(m => m.Id == id);
            if (searchAnalytic == null)
            {
                return NotFound();
            }
            return View(searchAnalytic);
        }

        // POST: SearchAnalytics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoOfResults,SucceedClicks,NoResultsCount,Id,Count,ObjectId")] SearchAnalytic searchAnalytic)
        {
            if (id != searchAnalytic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(searchAnalytic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SearchAnalyticExists(searchAnalytic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(searchAnalytic);
        }

        // GET: SearchAnalytics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var searchAnalytic = await _context.SearchAnalytics
                .SingleOrDefaultAsync(m => m.Id == id);
            if (searchAnalytic == null)
            {
                return NotFound();
            }

            return View(searchAnalytic);
        }

        // POST: SearchAnalytics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var searchAnalytic = await _context.SearchAnalytics.SingleOrDefaultAsync(m => m.Id == id);
            _context.SearchAnalytics.Remove(searchAnalytic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SearchAnalyticExists(string id)
        {
            return _context.SearchAnalytics.Any(e => e.Id == id);
        }
    }
}
