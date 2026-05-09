var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.DiNet_GPipe_WebApi>("dinet-gpipe-webapi")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.DiNet_GPipe_Dashboard>("dinet-gpipe-dashboard")
    .WithHttpHealthCheck("/health")
    .WaitFor(api)
    .WithReference(api);

builder.Build().Run();
