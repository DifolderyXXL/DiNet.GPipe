using DiNet.GPipe.Actions;
using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.JavaBuilder.Domain;
using DiNet.GPipe.JavaBuilder.Helpers;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.Scheduler;
using DiNet.GPipe.Scheduler.Builders;
using DiNet.GPipe.Scheduler.Domain;
using DiNet.GPipe.Scheduler.Interfaces;
using DiNet.GPipe.Scheduler.Primitives;
using DiNet.GPipe.Scheduler.Running;
using DiNet.GPipe.Storaging.Domain;
using DiNet.GPipe.Storaging.Interfaces;
using LibGit2Sharp;


var storage = @"C:\C#\Github\DiNet.GPipe\FetchedApkBuilds";
var project = new AndroidStudioProjectSettings(@"C:\AndroidStudio\rctschedule", ApkBuildType.Debug);
var jdk = new JdkSettings();

IApkBuilder apkBuilder = new AndroidStudioApkBuilder(project, jdk);
IFileStorage fileStorage = new ApkBuildStorage(new(Path.Combine(storage, "Debug")));

var taskMonitor = new TaskMonitor();

Console.WriteLine(new ApkBuildAction(taskMonitor, fileStorage, apkBuilder).Run());

taskMonitor.WaitAllTasksCompleted();

return;
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

