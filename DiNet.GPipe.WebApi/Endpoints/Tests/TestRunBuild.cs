using DiNet.GPipe.BuildingApplication.Handlers;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;

namespace DiNet.GPipe.WebApi.Endpoints.Tests;

public class TestRunBuild : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("test/build", async (IEventBus bus, CancellationToken ct) =>
        {
            await bus.PublisthAsync(new BuildCommand(
                "rctschedule", 
                "feature/architecture", 
                @"https://github.com/DifolderyXXL/DiNet.RctSchedule.Widget",

                "22b53fb6f7fa397644f4df20cd357380d557888f",
                new BuildVersion(1, 1, 1),
                
                Guid.NewGuid()), ct);
        });
    }
}
