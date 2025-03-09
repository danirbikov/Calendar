using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Calendar.Abstraction;

namespace Calendar.Models.DTO
{

    public class NoteDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ReminderTime { get; set; }
    }
}
