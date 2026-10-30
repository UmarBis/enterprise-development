using FitnessClub.Api.Infrastructure;
using FitnessClub.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Для Aspire

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем нашу In-Memory базу (Singleton) и Сервис аналитики
builder.Services.AddSingleton<InMemoryRepository>();
builder.Services.AddScoped<AnalyticsService>();

var app = builder.Build();

app.MapDefaultEndpoints(); // Для Aspire

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
