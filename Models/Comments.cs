using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
	public class Comments
	{
		[Key]
		public int Id { get; set; }

        [Required]
        public string? BugId { get; set; }

        
        public string? CreatedByUser { get; set; }

        [Required]
        public string? Comment { get; set; }

        [NotMapped]
        public List<string>? ticketList { get; set; }

        [NotMapped]
        public List<KeyValuePair<string, string>>? hashmap_tDetail { get; set; }

        [NotMapped]
        public List<KeyValuePair<string, string>>? hashmap { get; set; }
    }
}

