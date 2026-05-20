using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.BuildingApplication.Handlers;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public record BuildInfo(
    string BuildId,
    string ProjectId,
    BuilderState state,
    DateTime Started);

public record BuildResult(Result<IApkFile> Result, DateTime Start, DateTime End, string BuildId, string ProjectId);
