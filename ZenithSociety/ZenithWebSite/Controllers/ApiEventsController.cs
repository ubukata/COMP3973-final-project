using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Data;
using ZenithWebSite.Models.EventViewModels;

namespace ZenithWebSite.Controllers
{
    [Route("api/events")]
    public class ApiEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApiEventsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("{dateFrom}/{dateTo}")]
        public async Task<IActionResult> Get(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                dateFrom = dateFrom.AddHours(0).AddMinutes(0).AddSeconds(0);
                dateTo = dateTo.AddHours(23).AddMinutes(59).AddSeconds(59);
                var events = await _context.Events.Where(_ =>
                    _.IsActive
                    && _.DateFrom >= dateFrom
                    && _.DateTo <= dateTo).Select(_ => new EventViewModel()
                    {
                        DateFrom = _.DateFrom,
                        StartTime = _.DateFrom.ToString("hh:mm tt"),
                        EndTime = _.DateTo.ToString("hh:mm tt"),
                        ActivityType = _.ActivityType.Description
                    }).ToArrayAsync();

                var groupedByDate = events
                    .GroupBy(_ => _.DateFrom.Value.Date)
                    .OrderBy(_ => _.Key)
                    .Select(_ => new
                    {
                        Date = _.Key.ToLongDateString(),
                        Events = _.ToArray()
                    });

                return Ok(groupedByDate);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
    }
}