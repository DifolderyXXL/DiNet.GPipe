using System;
using Microsoft.AspNetCore.SignalR;

namespace DiNet.GPipe.WebApi.Hubs;


public interface IBuildLogHub
{
    Task Log(string buildId, string message);
}

public class BuildLogHub : Hub
{
    public async Task JoinGroup(string builderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, builderId);
    }

    public async Task LeaveGroup(string builderId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, builderId);
    }
}
