using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ZenithWebSite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public ApplicationUser() : base()
        {

        }

        public ApplicationUser(string userName, string firstName, string lastName, string email) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            base.SecurityStamp = System.Guid.NewGuid().ToString();
        }
    }
}
