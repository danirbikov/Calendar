using Calendar.Abstraction.Interfaces;
using Calendar.MediatR.Requests;
using Calendar.Models;
using CsvHelper;
using MediatR;
using System.Globalization;

namespace Calendar.MediatR.Commands.Exports
{
    public class ExportNotesToCsvRequestHandler : IRequestHandler<ExportNotesToCsvRequest, string>
    {
        private readonly IReadRepository<Note> _readRepository;

        public ExportNotesToCsvRequestHandler(IReadRepository<Note> readRepository)
        {
            _readRepository = readRepository;
        }
       
        public async Task<string> Handle(ExportNotesToCsvRequest request, CancellationToken cancellationToken)
        {
            var notes = await _readRepository.GetAllAsync();

            var fileName = $"calendar_{DateTime.Now:yyyyMMddHHmmss}.csv";
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory; 
            var exportDirectory = Path.Combine(baseDirectory, "wwwroot", "exports");

            Directory.CreateDirectory(exportDirectory);

            var filePath = Path.Combine(exportDirectory, fileName);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField(nameof(Note.Title));
                csv.WriteField(nameof(Note.Description));
                csv.WriteField(nameof(Note.CreationTime));
                csv.WriteField(nameof(Note.ReminderTime));
                csv.NextRecord();

                foreach (var note in notes)
                {
                    csv.WriteField(note.Title);
                    csv.WriteField(note.Description);
                    csv.WriteField(note.CreationTime);
                    csv.WriteField(note.ReminderTime);
                    csv.NextRecord();
                }

                var csvString = writer.ToString();
            }

            return filePath;

        }
    }


}
