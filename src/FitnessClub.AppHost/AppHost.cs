var builder = DistributedApplication.CreateBuilder(args);

// Добавляем наш API в оркестратор
var api = builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api");

builder.Build().Run();