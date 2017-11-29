using System;
using System.ComponentModel.DataAnnotations;

namespace ZenithWebSite.Models.EventViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        [Display(Name = "From")]
        [Required]
        public DateTime? DateFrom { get; set; }
        [Display(Name = "To")]
        [Required]
        public DateTime? DateTo { get; set; }
        [Display(Name = "Is Active")]
        [Required]
        public bool IsActive { get; set; }
        public string ActivityType { get; set; }
        [Display(Name = "Activity Type")]
        [Required]
        public int ActivityTypeId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public EventViewModel()
        {
            IsActive = true;
        }
    }
}