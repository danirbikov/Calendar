using Calendar.Abstraction.Interfaces;
using Calendar.MediatR.Requests.Notes;
using Calendar.Models;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;

namespace Calendar.MediatR.Commands.Notes
{
    public class DeleteNoteRequestHandler : IRequestHandler<DeleteNoteRequest, bool>
    {
        private readonly IWriteRepository<Note> _noteRepository;

        public DeleteNoteRequestHandler(IWriteRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

       
        public async Task<bool> Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
        {
            var result = await _noteRepository.DeleteByIdAsync(request.Id);

            return result;
        }
    }


}
