using Calendar.Abstraction.Interfaces;
using Calendar.Data;
using Calendar.Jobs;
using Calendar.Repositories;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(opt =>
{
    opt.Select().Expand().Filter().Count().OrderBy().SetMaxTop(1000);
    opt.AddRouteComponents("api/odata", new EdmModel());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CalendarDb")));


builder.Services.AddScoped(typeof(IReadRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(GenericRepository<>));

builder.Services.AddSignalR();

builder.Services.AddHostedService<ReminderService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred migrating the DB.");
        }
    }
}

app.MapHub<NotificationSignalRHub>("/notificationHub");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
