using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Data;
using ZenithWebSite.Models;
using ZenithWebSite.Models.ActivityTypeViewModels;

namespace ZenithWebSite.Controllers
{
    [Authorize]
    public class ActivityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivityTypes
        public async Task<ActionResult> Index()
        {
            return View(await _context.ActivityTypes.ToListAsync());
        }

        // GET: ActivityTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ActivityType activityType = await _context.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }
            return View(activityType);
        }

        // GET: ActivityTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ActivityTypeId,Description")] ActivityTypeViewModel activityTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var newActivityType = new ActivityType(activityTypeViewModel.Description);
                _context.ActivityTypes.Add(newActivityType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(activityTypeViewModel);
        }

        // GET: ActivityTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ActivityType activityType = await _context.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }
            var viewModel = new ActivityTypeViewModel()
            {
                ActivityTypeId = activityType.ActivityTypeId,
                Description = activityType.Description
            };
            return View(viewModel);
        }

        // POST: ActivityTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("ActivityTypeId,Description,CreationDate")] ActivityTypeViewModel activityTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                ActivityType activityType = await _context.ActivityTypes.FindAsync(activityTypeViewModel.ActivityTypeId);
                activityType.SetDescription(activityTypeViewModel.Description);
                _context.Entry(activityType).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(activityTypeViewModel);
        }

        // GET: ActivityTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ActivityType activityType = await _context.ActivityTypes.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }
            return View(activityType);
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ActivityType activityType = await _context.ActivityTypes.FindAsync(id);
            _context.ActivityTypes.Remove(activityType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}