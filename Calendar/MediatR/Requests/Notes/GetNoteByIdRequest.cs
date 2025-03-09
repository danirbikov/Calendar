using Calendar.Models;
using MediatR;

namespace Calendar.MediatR.Requests.Notes
{
    public class GetNoteByIdRequest : IRequest<Note?> 
    { 
        public Guid Id { get; set; } 
    }
}
