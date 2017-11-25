using System.ComponentModel.DataAnnotations;

namespace ZenithWebSite.Models.ActivityTypeViewModels
{
    public class ActivityTypeViewModel
    {
        public int ActivityTypeId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}