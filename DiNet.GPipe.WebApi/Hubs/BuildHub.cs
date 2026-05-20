using System;
using Microsoft.AspNetCore.SignalR;

namespace DiNet.GPipe.WebApi.Hubs;


public interface IBuildLogHub
{
    Task Log(string buildId, string message);
}

public class BuildLogHub : Hub
{
}
