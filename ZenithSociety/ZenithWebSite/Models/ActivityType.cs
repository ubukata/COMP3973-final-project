using System;

namespace ZenithWebSite.Models
{
    public class ActivityType
    {
        public int ActivityTypeId { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreationDate { get; protected set; }

        public ActivityType()
        {
            CreationDate = DateTime.Now;
        }

        public ActivityType(string description) : this()
        {
            SetDescription(description);
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("Description is mandatory for ActivityType");

            Description = description;
        }
    }
}