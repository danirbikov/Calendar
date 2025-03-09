using MediatR;

namespace Calendar.MediatR.Requests.Notes
{
    public class DeleteNoteRequest : IRequest<bool>
    { 
        public Guid Id { get; set; } 
    }
}
