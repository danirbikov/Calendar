using Calendar.Models;
using Calendar.Models.DTO;
using MediatR;

namespace Calendar.MediatR.Requests.Notes
{
    public class UpdateNoteRequest : IRequest<bool>
    { 
        public NoteDto Note { get; set; } 
    }
}
