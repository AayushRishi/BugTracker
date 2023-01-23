using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class Projects
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string? Project { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Contributors { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Contributorss { get; set; }

    }
}

