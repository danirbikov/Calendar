using AutoMapper;
using Calendar.Abstraction.Interfaces;
using Calendar.MediatR.Requests.Notes;
using Calendar.Models;
using Calendar.Models.DTO;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;

namespace Calendar.MediatR.Commands.Notes
{
    public class CreateNoteRequestHandler : IRequestHandler<CreateNoteRequest, bool>
    {
        private readonly IMapper _mapper;
        private readonly IWriteRepository<Note> _noteRepository;

        public CreateNoteRequestHandler(IWriteRepository<Note> noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

       
        public async Task<bool> Handle(CreateNoteRequest request, CancellationToken cancellationToken)
        {
            Note mappedItem = _mapper.Map<Note>(request.Note);
            Note item = await _noteRepository.AddAsync(mappedItem);

            return item == null ? false : true;
        }
    }


}
