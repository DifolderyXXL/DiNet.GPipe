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
using DiNet.GPipe.Storaging.Settings;
using LibGit2Sharp;


var storage = @"C:\C#\Github\DiNet.GPipe\FetchedApkBuilds";
var projectPath = @"C:\AndroidStudio\rctschedule";
var project = new AndroidStudioProjectSettings(projectPath, ApkBuildType.Debug);
var jdk = new JdkSettings();
var namingSettings = new BuildFileNamingSettings(true, true);

IApkBuilder apkBuilder = new AndroidStudioApkBuilder(project, jdk);
IFileStorage fileStorage = new ApkBuildStorage(new(Path.Combine(storage, "Debug"), namingSettings));

//var taskMonitor = new TaskMonitor();

var config = new LocalRepositoryConfig(
    projectPath,
    "release");

var repository = new Repository(config.path);

var runner = new ScheduleRunner();
var pipeline = PipelineBuilder.Create()
    .Action(new IterationMessageAction("Iteration Batch;", 12))
    .Action(new CheckIfBranchExists(repository, config))
    .Action(new CheckIfUpdates(repository, config))
    .Action(new LogMessageAction("Updated, Running Apk Build"))
    .Action(new ApkBuildAction(fileStorage, apkBuilder))
    .Build();

var s = runner.Add(new Schedule(pipeline, TimeSpan.FromSeconds(5)));


s.OnException += e => Console.WriteLine(e.Message);

while (true)
{

}



