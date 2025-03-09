using Calendar.Abstraction.Interfaces;
using Calendar.MediatR.Requests.Notes;
using Calendar.Models;
using MediatR;

namespace Calendar.MediatR.Commands.Notes
{
    public class UpdateNoteRequestHandler : IRequestHandler<UpdateNoteRequest, bool>
    {
        private readonly IWriteRepository<Note> _noteWriteRepository;
        private readonly IReadRepository<Note> _noteReadRepository;

        public UpdateNoteRequestHandler(IWriteRepository<Note> noteWriteRepository, IReadRepository<Note> noteReadRepository)
        {
            _noteWriteRepository = noteWriteRepository;
            _noteReadRepository = noteReadRepository;
        }

       
        public async Task<bool> Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
        {
            var dbItem = await _noteReadRepository.GetByIdAsync(request.Note.Id);

            dbItem.Title = request.Note.Title;
            dbItem.Description = request.Note.Description;
            dbItem.CreationTime = request.Note.CreationTime;
            dbItem.ReminderTime = request.Note.ReminderTime;

            await _noteWriteRepository.UpdateAsync(dbItem);              

            return true;
        }
    }


}
