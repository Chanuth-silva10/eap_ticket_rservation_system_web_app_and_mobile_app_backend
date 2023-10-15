using TicketReservationManager.Models;
using TicketReservationManager.Services;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("TicketReservation");

builder.Services.Configure<DBConnection>(
    builder.Configuration.GetSection("MongoDB"));
    builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader()
                                                  .AllowAnyMethod();;
                      });
});
builder.Services.AddSingleton<AdminManagerService>();
builder.Services.AddSingleton<TravelarManagerService>();
builder.Services.AddSingleton<TrainService>();
builder.Services.AddSingleton<TrainScheduleService>();
builder.Services.AddSingleton<ReservationService>();

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
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
