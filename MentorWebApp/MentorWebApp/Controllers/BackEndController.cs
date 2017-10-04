using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MentorWebApp.Data;
using MentorWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MentorWebApp.Controllers
{
    [Authorize(Policy = "MustBeAdmin")]
    public class BackEndController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public BackEndController(ApplicationDbContext context)
        {
            _context = context;

            _userManager = _context.GetService<UserManager<ApplicationUser>>();
            _roleManager = _context.GetService<RoleManager<IdentityRole>>();
        }

        // GET: BackEnd
        public ActionResult Index()
        {
            return View();
        }


        /// /// /// ///
        /// User Backend
        /// /// /// ///

        // GET: UserBackEnd
        public async Task<IActionResult> UserIndex()
        {
            return View(await _context.ApplicationUser.ToListAsync());
        }

        // GET: UserBackEnd/Details/5
        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
                return NotFound();

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
                return NotFound();

            return View(applicationUser);
        }

        // GET: UserBackEnd/Create
        public IActionResult UserCreate()
        {
            return View();
        }

        // POST: UserBackEnd/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate(
            [Bind(
                "UctNumber,Permissions,Enabled,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")]
            ApplicationUser applicationUser)
        {
            var oldRole = applicationUser.Permissions;
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserIndex));
            }

            var result = await applicationUser.ChangeRoleAsync(applicationUser.Permissions, oldRole,
                _context.GetService<UserManager<ApplicationUser>>());

            return View(applicationUser);
        }

        // GET: UserBackEnd/Edit/5
        public async Task<IActionResult> UserEdit(string id)
        {
            if (id == null)
                return NotFound();

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
                return NotFound();
            return View(applicationUser);
        }

        // POST: UserBackEnd/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(string id, string oldPermissions,
            [Bind(
                "ApplicationUserId,UctNumber,Permissions,Enabled,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")]
            ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
                return NotFound();


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                        return NotFound();
                }
                try
                {
                    var res = await applicationUser.ChangeRoleAsync(applicationUser.Permissions, oldPermissions,
                        _context.GetService<UserManager<ApplicationUser>>());
                    if (!res)
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }

                return RedirectToAction(nameof(UserIndex));
            }
            return View(applicationUser);
        }

        // GET: UserBackEnd/Delete/5
        public async Task<IActionResult> UserDelete(string id)
        {
            if (id == null)
                return NotFound();

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
                return NotFound();

            return View(applicationUser);
        }

        // POST: UserBackEnd/Delete/5
        [HttpPost]
        [ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDeleteConfirmed(string id)
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
                .SingleOrDefaultAsync(m => m.ResourceId == id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourcesCreate(
            [Bind("Id,DateAdded,Title,Tags,Type,UctNumber,Link")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                var analytic = new ContentAnalytic(resource.ResourceId);
                resource.Init(analytic);
                _context.Add(resource);
                _context.Add(analytic);
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

            var resource = await _context.Resources.SingleOrDefaultAsync(m => m.ResourceId == id);
            if (resource == null)
                return NotFound();
            return View(resource);
        }

        // POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourcesEdit(string id,
            [Bind("Id,DateAdded,Title,Tags,Type,UctNumber")] Resource resource)
        {
            if (id != resource.ResourceId)
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
                    if (!ResourceExists(resource.ResourceId))
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
                .SingleOrDefaultAsync(m => m.ResourceId == id);
            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost]
        [ActionName("ResourcesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResourcesDeleteConfirmed(string id)
        {
            var resource = await _context.Resources.SingleOrDefaultAsync(m => m.ResourceId == id);
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ResourcesIndex));
        }

        private bool ResourceExists(string id)
        {
            return _context.Resources.Any(e => e.ResourceId == id);
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
                return NotFound();

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();

            return View(question);
        }

        // GET: QuestionsBackEnd/Create
        public IActionResult QuestionsCreate()
        {
            return View();
        }

        // POST: QuestionsBackEnd/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionsCreate(
            [Bind("Anonymous,Title,Tags,MessageContent,Id,UctNumber,DatePosted")] Question question)
        {
            if (ModelState.IsValid)
            {
                var analytic = new ContentAnalytic(question.Id);
                question.Init(analytic);
                _context.Add(question);
                _context.Add(analytic);
                await _context.SaveChangesAsync();

                
                return RedirectToAction(nameof(QuestionsIndex));
            }
            return View(question);
        }

        // GET: QuestionsBackEnd/Edit/5
        public async Task<IActionResult> QuestionsEdit(string id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions.SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();
            return View(question);
        }

        // POST: QuestionsBackEnd/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionsEdit(string id,
            [Bind("Anonymous,Title,Tags,MessageContent,Id,UctNumber,DatePosted")] Question question)
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
                return RedirectToAction(nameof(QuestionsIndex));
            }
            return View(question);
        }

        // GET: QuestionsBackEnd/Delete/5
        public async Task<IActionResult> QuestionsDelete(string id)
        {
            if (id == null)
                return NotFound();

            var question = await _context.Questions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (question == null)
                return NotFound();

            return View(question);
        }

        // POST: QuestionsBackEnd/Delete/5
        [HttpPost]
        [ActionName("QuestionsDelete")]
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