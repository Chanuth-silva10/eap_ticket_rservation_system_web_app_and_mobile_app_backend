using TicketReservationManager.Models;
using TicketReservationManager.Services;

var builder = WebApplication.CreateBuilder(args);

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("TicketReservation");

builder.Services.Configure<DBConnection>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<AdminManagerService>();
builder.Services.AddSingleton<TravelarManagerService>();
builder.Services.AddSingleton<TrainService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Logger.LogInformation("Ticket Reservation Manager Started");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
