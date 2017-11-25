using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZenithWebSite.Models;

namespace ZenithWebSite.Data
{
    public class MockData
    {
        private readonly RoleManager<IdentityRole> _roleMgr;
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly ApplicationDbContext _applicationContext;

        public MockData(UserManager<ApplicationUser> userMgr, RoleManager<IdentityRole> roleMgr, ApplicationDbContext applicationContext)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _applicationContext = applicationContext;
        }

        public async Task Seed()
        {
            if (!(await _roleMgr.RoleExistsAsync("Admin")))
            {
                var role = new IdentityRole("Admin");
                await _roleMgr.CreateAsync(role);
            }
            if (!(await _roleMgr.RoleExistsAsync("Member")))
            {
                var role = new IdentityRole("Member");
                await _roleMgr.CreateAsync(role);
            }

            var userA = _userMgr.FindByEmailAsync("a@a.a").Result;
            var userM = _userMgr.FindByEmailAsync("m@m.m").Result;

            if (userA == null)
            {
                userA = new ApplicationUser("a", "a", "a", "a@a.a");

                await _userMgr.CreateAsync(userA, "P@$$w0rd!");
                await _userMgr.AddToRoleAsync(userA, "Admin");
            }
            if (userM == null)
            {
                userM = new ApplicationUser("m", "m", "m", "m@m.m");

                await _userMgr.CreateAsync(userM, "P@$$w0rd!");
                await _userMgr.AddToRoleAsync(userM, "Member");
            }

            if (!_applicationContext.ActivityTypes.Any())
            {
                _applicationContext.ActivityTypes.AddRange(new[] {new ActivityType("Senior�s  Golf Tournament"),
                                                                    new ActivityType("Leadership General Assembly Meeting"),
                                                                    new ActivityType("Youth Bowling Tournament"),
                                                                    new ActivityType("Young ladies cooking lessons"),
                                                                    new ActivityType("Youth craft lessons"),
                                                                    new ActivityType("Youth choir practice"),
                                                                    new ActivityType("Lunch"),
                                                                    new ActivityType("Pancake Breakfast"),
                                                                    new ActivityType("Swimming Lessons for the youth"),
                                                                    new ActivityType("Swimming Exercise for parents"),
                                                                    new ActivityType("Bingo Tournament"),
                                                                    new ActivityType("BBQ Lunch"),
                                                                    new ActivityType("Garage Sale")});
                _applicationContext.SaveChanges();
            }

            if (!_applicationContext.Events.Any())
            {
                var activitiesList = _applicationContext.ActivityTypes.ToArray();
                _applicationContext.Events.AddRange(new[] {
                    // week oct 8
                    new Event(activitiesList[0], new DateTime(2017, 10, 10, 8, 30, 0), new DateTime(2017, 10, 10, 10, 30, 0), true, "a"),
                    new Event(activitiesList[1], new DateTime(2017, 10, 11, 8, 30, 0), new DateTime(2017, 10, 11, 10, 30, 0), true, "a"),
                    new Event(activitiesList[2], new DateTime(2017, 10, 13, 17, 30, 0), new DateTime(2017, 10, 13, 19, 15, 0), true, "a"),
                    new Event(activitiesList[3], new DateTime(2017, 10, 13, 19, 0, 0), new DateTime(2017, 10, 13, 20, 0, 0), true, "a"),
                    new Event(activitiesList[4], new DateTime(2017, 10, 14, 8, 30, 0), new DateTime(2017, 10, 14, 10, 30, 0), true, "a"),
                    new Event(activitiesList[5], new DateTime(2017, 10, 14, 10, 30, 0), new DateTime(2017, 10, 14, 12, 0, 0), true, "a"),
                    new Event(activitiesList[6], new DateTime(2017, 10, 14, 12, 0, 0), new DateTime(2017, 10, 14, 13, 30, 0), true, "a"),
                    new Event(activitiesList[7], new DateTime(2017, 10, 15, 7, 30, 0), new DateTime(2017, 10, 15, 8, 30, 0), true, "a"),
                    new Event(activitiesList[8], new DateTime(2017, 10, 15, 8, 30, 0), new DateTime(2017, 10, 15, 10, 30, 0), true, "a"),
                    new Event(activitiesList[9], new DateTime(2017, 10, 15, 8, 30, 0), new DateTime(2017, 10, 15, 10, 30, 0), true, "a"),
                    new Event(activitiesList[10], new DateTime(2017, 10, 15, 10, 30, 0), new DateTime(2017, 10, 15, 12, 0, 0), true, "a"),
                    new Event(activitiesList[11], new DateTime(2017, 10, 15, 12, 0, 0), new DateTime(2017, 10, 15, 13, 0, 0), true, "a"),
                    new Event(activitiesList[12], new DateTime(2017, 10, 15, 13, 0, 0), new DateTime(2017, 10, 15, 18, 0, 0), true, "a"),
                    // week oct 15
                    new Event(activitiesList[0], new DateTime(2017, 10, 17, 8, 30, 0), new DateTime(2017, 10, 17, 10, 30, 0), true, "a"),
                    new Event(activitiesList[1], new DateTime(2017, 10, 18, 8, 30, 0), new DateTime(2017, 10, 18, 10, 30, 0), true, "a"),
                    new Event(activitiesList[2], new DateTime(2017, 10, 20, 17, 30, 0), new DateTime(2017, 10, 20, 19, 15, 0), true, "a"),
                    new Event(activitiesList[3], new DateTime(2017, 10, 20, 19, 0, 0), new DateTime(2017, 10, 20, 20, 0, 0), true, "a"),
                    new Event(activitiesList[4], new DateTime(2017, 10, 21, 8, 30, 0), new DateTime(2017, 10, 21, 10, 30, 0), true, "a"),
                    new Event(activitiesList[5], new DateTime(2017, 10, 21, 10, 30, 0), new DateTime(2017, 10, 21, 12, 0, 0), true, "a"),
                    new Event(activitiesList[6], new DateTime(2017, 10, 21, 12, 0, 0), new DateTime(2017, 10, 21, 13, 30, 0), true, "a"),
                    new Event(activitiesList[7], new DateTime(2017, 10, 22, 7, 30, 0), new DateTime(2017, 10, 22, 8, 30, 0), true, "a"),
                    new Event(activitiesList[8], new DateTime(2017, 10, 22, 8, 30, 0), new DateTime(2017, 10, 22, 10, 30, 0), true, "a"),
                    new Event(activitiesList[9], new DateTime(2017, 10, 22, 8, 30, 0), new DateTime(2017, 10, 22, 10, 30, 0), true, "a"),
                    new Event(activitiesList[10], new DateTime(2017, 10, 22, 10, 30, 0), new DateTime(2017, 10, 22, 12, 0, 0), true, "a"),
                    new Event(activitiesList[11], new DateTime(2017, 10, 22, 12, 0, 0), new DateTime(2017, 10, 22, 13, 0, 0), true, "a"),
                    new Event(activitiesList[12], new DateTime(2017, 10, 22, 13, 0, 0), new DateTime(2017, 10, 22, 18, 0, 0), true, "a"),
                    // week oct 22
                    new Event(activitiesList[0], new DateTime(2017, 10, 24, 8, 30, 0), new DateTime(2017, 10, 24, 10, 30, 0), true, "a"),
                    new Event(activitiesList[1], new DateTime(2017, 10, 25, 8, 30, 0), new DateTime(2017, 10, 25, 10, 30, 0), true, "a"),
                    new Event(activitiesList[2], new DateTime(2017, 10, 27, 17, 30, 0), new DateTime(2017, 10, 27, 19, 15, 0), true, "a"),
                    new Event(activitiesList[3], new DateTime(2017, 10, 27, 19, 0, 0), new DateTime(2017, 10, 27, 20, 0, 0), true, "a"),
                    new Event(activitiesList[4], new DateTime(2017, 10, 28, 8, 30, 0), new DateTime(2017, 10, 28, 10, 30, 0), true, "a"),
                    new Event(activitiesList[5], new DateTime(2017, 10, 28, 10, 30, 0), new DateTime(2017, 10, 28, 12, 0, 0), true, "a"),
                    new Event(activitiesList[6], new DateTime(2017, 10, 28, 12, 0, 0), new DateTime(2017, 10, 28, 13, 30, 0), true, "a"),
                    new Event(activitiesList[7], new DateTime(2017, 10, 29, 7, 30, 0), new DateTime(2017, 10, 29, 8, 30, 0), true, "a"),
                    new Event(activitiesList[8], new DateTime(2017, 10, 29, 8, 30, 0), new DateTime(2017, 10, 29, 10, 30, 0), true, "a"),
                    new Event(activitiesList[9], new DateTime(2017, 10, 29, 8, 30, 0), new DateTime(2017, 10, 29, 10, 30, 0), true, "a"),
                    new Event(activitiesList[10], new DateTime(2017, 10, 29, 10, 30, 0), new DateTime(2017, 10, 29, 12, 0, 0), true, "a"),
                    new Event(activitiesList[11], new DateTime(2017, 10, 29, 12, 0, 0), new DateTime(2017, 10, 29, 13, 0, 0), true, "a"),
                    new Event(activitiesList[12], new DateTime(2017, 10, 29, 13, 0, 0), new DateTime(2017, 10, 29, 18, 0, 0), true, "a")
                });
                _applicationContext.SaveChanges();
            }
        }
    }
}