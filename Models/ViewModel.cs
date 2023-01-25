using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class ViewModel
    {
        public IEnumerable<Projects>? Projects { get; set; }
        public IEnumerable<Bugs>? Bugs { get; set; }

    }
}

