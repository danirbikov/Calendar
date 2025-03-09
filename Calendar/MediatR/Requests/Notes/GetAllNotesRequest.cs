using Calendar.Models;
using MediatR;

namespace Calendar.MediatR.Requests.Notes
{
    public class GetAllNotesRequest : IRequest<IEnumerable<Note>> 
    {
        
    }  
}
