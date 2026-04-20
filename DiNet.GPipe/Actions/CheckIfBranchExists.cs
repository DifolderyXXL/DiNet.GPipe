using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.Scheduler.Interfaces;
using DiNet.GPipe.Scheduler.Primitives;
using DiNet.GPipe.Storaging.Interfaces;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiNet.GPipe.Actions;

public record class LocalRepositoryConfig(
    string path,
    string branch);

public class CheckIfBranchExists(Repository c_repository, LocalRepositoryConfig c_config) : IPipeAction
{
    private readonly Repository _repository = c_repository;
    private readonly LocalRepositoryConfig _config = c_config;

    public PipeActionResult Run()
    {
        if(_repository.Branches.Any(x => x.FriendlyName == _config.branch))
            return PipeActionResult.Success;

        return PipeActionResult.Failure;
    }
}

public class CheckIfUpdates(Repository c_repository, LocalRepositoryConfig c_config) : IPipeAction
{
    private readonly Repository _repository = c_repository;
    private readonly LocalRepositoryConfig _config = c_config;

    private string? _previousSha = null;

    public PipeActionResult Run()
    {
        var branch = _repository.Branches.FirstOrDefault(x => x.FriendlyName == _config.branch);

        if(branch == null)
            return PipeActionResult.Failure;


        var commit = branch.Reference.TargetIdentifier;
        var isSucceded = _previousSha != null && _previousSha != commit;

        _previousSha = commit;

        if (isSucceded)
            return PipeActionResult.Success;


        return PipeActionResult.Failure;
    }
}

public class LogMessageAction(string message) : IPipeAction
{
    private readonly string _message = message;

    public PipeActionResult Run()
    {
        Console.WriteLine(_message);
        return PipeActionResult.Success;
    }
}

public class IterationMessageAction(string message, int interaction) : IPipeAction
{
    private readonly string _message = message;
    private readonly int _interaction = interaction;
    private int _currentInteraction = 0;
    public PipeActionResult Run()
    {
        _currentInteraction++;
        if (_currentInteraction % _interaction == 0)
        {
            Console.WriteLine(_message);
            _currentInteraction = 0;
        }
        
        return PipeActionResult.Success;
    }
}


public class ApkBuildAction(IFileStorage fileStorage, IApkBuilder apkBuilder) : IPipeAction
{
    private readonly IFileStorage _fileStorage = fileStorage;
    private readonly IApkBuilder _apkBuilder = apkBuilder;

    public PipeActionResult Run()
    {
        var result = RunAsync();
        result.Wait();

        if (result.Result)
            return PipeActionResult.Success;
        return PipeActionResult.Failure;
    }

    private async Task<bool> RunAsync(CancellationToken token = default)
    {
        try
        {
            var destination = await _apkBuilder.BuildAsync(token);
            if (destination == null)
                return false;

            await _fileStorage.StoreFrom(destination, token);
        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.StackTrace);
            return false;
        }


        return true;
    }
}