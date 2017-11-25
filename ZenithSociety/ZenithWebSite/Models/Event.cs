using System;

namespace ZenithWebSite.Models
{
    public class Event
    {
        public int EventId { get; protected set; }
        public DateTime DateFrom { get; protected set; }
        public DateTime DateTo { get; protected set; }
        public ActivityType ActivityType { get; protected set; }
        public int ActivityTypeId { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public bool IsActive { get; protected set; }
        public string ActionByUsername { get; protected set; }
        public DateTime DateModified { get; protected set; }

        public Event()
        {
            CreationDate = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public Event(ActivityType activityType, DateTime dateFrom, DateTime dateTo, bool isActive, string actionByUsername) : this()
        {
            SetActivityType(activityType);
            SetDates(dateFrom, dateTo);
            if (isActive)
                Activate();
            SetActionByUsername(actionByUsername);
        }

        public Event(int activityTypeId, DateTime dateFrom, DateTime dateTo, bool isActive, string actionByUsername) : this()
        {
            SetActivityTypeId(activityTypeId);
            SetDates(dateFrom, dateTo);
            if (isActive)
                Activate();
            SetActionByUsername(actionByUsername);
        }

        public void SetActivityType(ActivityType activityType)
        {
            if (activityType == null)
                throw new ArgumentNullException("Activity Type is mandatory for Event entity");

            ActivityType = activityType;
            SetActivityTypeId(activityType.ActivityTypeId);
        }

        public void SetActivityTypeId(int activityTypeId)
        {
            ActivityTypeId = activityTypeId;
        }

        public void SetDates(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
                throw new Exception("Date From cannot be greater than Date To in Event entity");

            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Desactivate()
        {
            IsActive = false;
        }

        public void SetActionByUsername(string actionByUsername)
        {
            ActionByUsername = actionByUsername;
            DateModified = DateTime.Now;
        }
    }
}