using Calendar.MediatR.Requests.Exports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
namespace Calendar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExportController : ODataController
{
    private readonly IMediator _mediator;

    public ExportController(IMediator mediator) => _mediator = mediator;


    [HttpGet]
    public async Task<string> ExportNoteToCsv()
    {
        var result = await _mediator.Send(new ExportNotesToCsvRequest());
        return result;
    }
}







