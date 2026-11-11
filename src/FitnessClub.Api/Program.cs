using FitnessClub.Api.Middleware;
using FitnessClub.Application.Services;
using FitnessClub.Contracts.Services;
using FitnessClub.Infrastructure.InMemory;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "FitnessClub API", Version = "v1" });
    var xml = Path.Combine(AppContext.BaseDirectory, "FitnessClub.Api.xml");
    if (File.Exists(xml)) o.IncludeXmlComments(xml);
});

builder.Services.AddCors(p => p.AddDefaultPolicy(b =>
    b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddInMemoryInfrastructure();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessClub API v1"));
app.MapControllers();
app.Run();

public partial class Program;