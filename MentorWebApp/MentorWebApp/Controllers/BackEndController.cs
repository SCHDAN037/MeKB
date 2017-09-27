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
    public class BackEndController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BackEndController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BackEnd
        public ActionResult Index()
        {
            return View();
        }

        
        /// User backend
        
            
        // GET: UserBackEnd
        public async Task<IActionResult> UserIndex()
        {
            return View(await _context.ApplicationUser.ToListAsync());
        }

        // GET: UserBackEnd/Details/5
        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: UserBackEnd/Create
        public IActionResult UserCreate()
        {
            return View();
        }

        // POST: UserBackEnd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate([Bind("UctiId,Role,Enabled,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserIndex));
            }
            return View(applicationUser);
        }

        // GET: UserBackEnd/Edit/5
        public async Task<IActionResult> UserEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: UserBackEnd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(string id, [Bind("UctiId,Role,Enabled,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserIndex));
            }
            return View(applicationUser);
        }

        // GET: UserBackEnd/Delete/5
        public async Task<IActionResult> UserDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: UserBackEnd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserIndex));
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }



        /////////////
        /// Resources
        /// 

        

        


        // GET: Resources
        public async Task<IActionResult> ResourcesIndex()
        {
            return View(await _context.Resources.ToListAsync());
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> ResourcesDetails(string id)
        {
            if (id == null)
                return NotFound();

            var resource = await _context.Resources
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // GET: Resources/Create
        public IActionResult ResourcesCreate()
        {
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourcesCreate([Bind("Id,DateAdded,Title,Tags,Type,UserId,Link")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ResourcesIndex));
            }
            return View(resource);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> ResourcesEdit(string id)
        {
            if (id == null)
                return NotFound();

            var resource = await _context.Resources.SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
                return NotFound();
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourcesEdit(string id,
            [Bind("Id,DateAdded,Title,Tags,Type,UserId")] Resource resource)
        {
            if (id != resource.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(ResourcesIndex));
            }
            return View(resource);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> ResourcesDelete(string id)
        {
            if (id == null)
                return NotFound();

            var resource = await _context.Resources
                .SingleOrDefaultAsync(m => m.Id == id);
            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourcesDeleteConfirmed(string id)
        {
            var resource = await _context.Resources.SingleOrDefaultAsync(m => m.Id == id);
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ResourcesIndex));
        }

        private bool ResourceExists(string id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }



        ////////////
        /// Questions

        // GET: QuestionsBackEnd
        public async Task<IActionResult> QuestionsIndex()
        {
            return View(await _context.Questions.ToListAsync());
        }

        // GET: QuestionsBackEnd/Details/5
        public async Task<IActionResult> QuestionsDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: QuestionsBackEnd/Create
        public IActionResult QuestionsCreate()
        {
            return View();
        }

        // POST: QuestionsBackEnd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionsCreate([Bind("Anonymous,Title,Tags,MessageContent,Id,UserId,DatePosted")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuestionsIndex));
            }
            return View(question);
        }

        // GET: QuestionsBackEnd/Edit/5
        public async Task<IActionResult> QuestionsEdit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: QuestionsBackEnd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionsEdit(string id, [Bind("Anonymous,Title,Tags,MessageContent,Id,UserId,DatePosted")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

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
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(QuestionsIndex));
            }
            return View(question);
        }

        // GET: QuestionsBackEnd/Delete/5
        public async Task<IActionResult> QuestionsDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: QuestionsBackEnd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionsDeleteConfirmed(string id)
        {
            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuestionsIndex));
        }

        private bool QuestionExists(string id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }



    }
}
