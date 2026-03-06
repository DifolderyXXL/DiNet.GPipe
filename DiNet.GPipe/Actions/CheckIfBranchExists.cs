using DiNet.GPipe.Scheduler.Interfaces;
using DiNet.GPipe.Scheduler.Primitives;
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
        if(_repository.Branches.Any(x => x.CanonicalName == _config.branch))
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
        var branch = _repository.Branches.FirstOrDefault(x => x.CanonicalName == _config.branch);

        if(branch == null)
            return PipeActionResult.Failure;

        var commit = branch.Commits.Last();
        if (_previousSha != null && _previousSha != commit.Sha)
            return PipeActionResult.Success;

        _previousSha = commit.Sha;

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