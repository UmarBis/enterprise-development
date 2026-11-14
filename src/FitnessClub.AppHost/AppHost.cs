var builder = DistributedApplication.CreateBuilder(args);

// Добавляем наш API в оркестратор
builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api");

builder.Build().Run();