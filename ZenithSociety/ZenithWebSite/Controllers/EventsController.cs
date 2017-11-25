using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Data;
using ZenithWebSite.Models;
using ZenithWebSite.Models.EventViewModels;

namespace ZenithWebSite.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<ActionResult> Index()
        {
            var events = _context.Events.Include(_ => _.ActivityType);
            return View(await events.OrderBy(_ => _.DateFrom).ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Event @event = await GetEventById(id.Value);
            
            if (@event == null)
            {
                return NotFound();
            }
            return View(MapEventToViewModel(@event));
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.ActivityTypes = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Description");
            var viewModel = new EventViewModel();
            return View(viewModel);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("DateFrom,DateTo,ActivityTypeId,IsActive")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                var @event = new Event(eventViewModel.ActivityTypeId, eventViewModel.DateFrom.Value, eventViewModel.DateTo.Value, eventViewModel.IsActive, User.Identity.Name);
                _context.Events.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityTypes = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Description", eventViewModel.ActivityTypeId);
            return View(eventViewModel);
        }

        // GET: Events/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event @event = await GetEventById(id.Value);
            if (@event == null)
            {
                return NotFound();
            }
            ViewBag.ActivityTypes = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Description", @event.ActivityTypeId);
            
            return View(MapEventToViewModel(@event));
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("EventId,DateFrom,DateTo,ActivityTypeId,IsActive")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                var @event = await GetEventById(eventViewModel.EventId);
                @event.SetDates(eventViewModel.DateFrom.Value, eventViewModel.DateTo.Value);
                @event.SetActivityTypeId(eventViewModel.ActivityTypeId);
                @event.SetActionByUsername(User.Identity.Name);
                if (eventViewModel.IsActive)
                {
                    @event.Activate();
                }
                else
                {
                    @event.Desactivate();
                }
                _context.Entry(@event).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypes = new SelectList(_context.ActivityTypes, "ActivityTypeId", "Description", eventViewModel.ActivityTypeId);
            return View(eventViewModel);
        }

        // GET: Events/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event @event = await GetEventById(id.Value);
            if (@event == null)
            {
                return NotFound();
            }
            return View(MapEventToViewModel(@event));
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Event @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.Include(_ => _.ActivityType).FirstAsync(_ => _.EventId == id);
        }

        public EventViewModel MapEventToViewModel(Event @event)
        {
            return new EventViewModel()
            {
                ActivityTypeId = @event.ActivityTypeId,
                ActivityType = @event.ActivityType.Description,
                DateFrom = @event.DateFrom,
                DateTo = @event.DateTo,
                EventId = @event.EventId,
                IsActive = @event.IsActive
            };
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