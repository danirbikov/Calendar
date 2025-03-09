using Calendar.Models;
using Calendar.Models.DTO;
using MediatR;

namespace Calendar.MediatR.Requests.Exports
{
    public class ExportNotesToCsvRequest : IRequest<string> 
    { 
    }
}
