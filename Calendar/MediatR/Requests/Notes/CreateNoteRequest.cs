using Calendar.Models;
using Calendar.Models.DTO;
using MediatR;

namespace Calendar.MediatR.Requests.Notes
{
    public class CreateNoteRequest : IRequest<bool> 
    { 
        public NoteDto Note { get; set; } 
    }
}
