var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume()
    .AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithReference(sql)
    .WaitFor(sql);

builder.Build().Run();
