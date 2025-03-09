using Microsoft.AspNetCore.SignalR;
using Calendar.Models;
using Calendar.Abstraction.Interfaces;

namespace Calendar.Jobs
{

    public class ReminderService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        private readonly ILogger<ReminderService> _logger;

        public ReminderService(IServiceProvider serviceProvider, ILogger<ReminderService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(CheckForReminders, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); 
            return Task.CompletedTask;
        }

        private async void CheckForReminders(object state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var readRepository = scope.ServiceProvider.GetRequiredService<IReadRepository<Note>>();
                var writeRepository = scope.ServiceProvider.GetRequiredService<IWriteRepository<Note>>();

                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationSignalRHub>>();

                var notes = await readRepository.GetAllAsync();
                var reminderNotes = notes.Where(n => n.ReminderTime <= DateTime.UtcNow && n.IsNotified == false).ToList();

                foreach (var note in reminderNotes)
                {
                    try
                    {
                        await hubContext.Clients.All.SendAsync("ReceiveNotification", $"Напоминание: {note.Title}");
                        note.IsNotified = true; 
                        await writeRepository.UpdateAsync(note);
                        _logger.LogInformation($"Notification '{note.Title}' sent for note: {note.Id}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error sending notification '{note.Title}' for note: {note.Id}");
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
