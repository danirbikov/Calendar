using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Calendar.Abstraction;

namespace Calendar.Models
{
    public class Note : BaseEntity
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime ReminderTime { get; set; }
        [Required]
        public bool IsNotified { get; set; } = false;
    }
}
