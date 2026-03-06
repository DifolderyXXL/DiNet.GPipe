using DiNet.GPipe.Actions;
using DiNet.GPipe.Scheduler;
using DiNet.GPipe.Scheduler.Builders;
using DiNet.GPipe.Scheduler.Domain;
using DiNet.GPipe.Scheduler.Interfaces;
using DiNet.GPipe.Scheduler.Primitives;
using DiNet.GPipe.Scheduler.Running;
using LibGit2Sharp;

var config = new LocalRepositoryConfig(
    "C:/TestRepositories/Test", 
    "release");

var repository = new Repository(config.path);

Console.WriteLine(string.Join("\n", repository.Branches.Select(x=>x.FriendlyName)));


var runner = new ScheduleRunner();
var pipeline = PipelineBuilder.Create()
    .Action(new CheckIfBranchExists(repository, config))
    .Action(new CheckIfUpdates(repository, config))
    .Action(new LogMessageAction("Updated"))
    .Build();

var s = runner.Add(new Schedule(pipeline, TimeSpan.FromSeconds(3)));


s.OnException += e => Console.WriteLine(e.Message);

while (true)
{

}

