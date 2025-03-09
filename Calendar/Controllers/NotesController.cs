using Calendar.MediatR.Requests;
using Calendar.MediatR.Requests.Notes;
using Calendar.Models;
using Calendar.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotesController(IMediator mediator) => _mediator = mediator;

   
    [HttpPost]
    public async Task<bool> Post(NoteDto note)
    {
        var result = await _mediator.Send(new CreateNoteRequest { Note = note });
        return result;
    }

    [HttpPut]
    public async Task<bool> Put(NoteDto note)
    {
        var result = await _mediator.Send(new UpdateNoteRequest { Note = note });
        return result;
    }

    [HttpDelete]
    public async Task<bool> Delete(Guid noteId)
    {
        var result = await _mediator.Send(new DeleteNoteRequest { Id = noteId });
        return result;
    }
}







