using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Models
{
    public class Bugs
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Project { get; set; }

        [Required]
        public string? Ticket { get; set; }

        [Required]
        public string? Status { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        public string? Priority { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Projects { get; set; }

        [NotMapped]
        public List<SelectListItem> Priorities { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "High", Text = "High" },
            new SelectListItem { Value = "Intermediate", Text = "Intermediate" },
            new SelectListItem { Value = "Low", Text = "Low"  },
        };

        [NotMapped]
        public List<SelectListItem> Statuses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "In Progress", Text = "In Progress" },
            new SelectListItem { Value = "Pending", Text = "Pending" },
            new SelectListItem { Value = "Complete", Text = "Complete"  },
        };

    }
}

