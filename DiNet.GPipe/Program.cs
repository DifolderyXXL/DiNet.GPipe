using DiNet.GPipe.Actions;
using DiNet.GPipe.Scheduler;
using DiNet.GPipe.Scheduler.Builders;
using DiNet.GPipe.Scheduler.Domain;
using DiNet.GPipe.Scheduler.Interfaces;
using DiNet.GPipe.Scheduler.Primitives;
using DiNet.GPipe.Scheduler.Running;
using LibGit2Sharp;


var path = "~/";
var repository = new Repository();



var runner = new ScheduleRunner();
var pipeline = PipelineBuilder.Create()
    .Action(new CheckIfBranchExists("A"))
    .Action(new PipelineCheckerAction("B"))
    .Build();

runner.Add(new Schedule(pipeline, TimeSpan.FromSeconds(5)));

var pipeline2 = PipelineBuilder.Create()
    .Action(new PipelineCheckerAction("oe1"))
    .Action(new PipelineCheckerAction("oe2", 2))
    .Build();

var s = runner.Add(new Schedule(pipeline2, TimeSpan.FromSeconds(3), new(ScheduleExceptionPolicy.Destroy)));

s.OnException += e => Console.WriteLine(e.Message);

while (true)
{

}

