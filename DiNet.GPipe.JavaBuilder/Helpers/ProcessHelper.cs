using System.Diagnostics;

namespace DiNet.GPipe.JavaBuilder.Helpers;
public static class ProcessHelper
{
    public static async Task RunProcess(
        string fileName, 
        string arguments, 
        string workingDirectory, 
        IProcessLogger? logger = null,
        CancellationToken token = default)
    {
        using (var process = new Process())
        {
            process.StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            process.OutputDataReceived += (sender, args) =>
            {
                logger?.LogData(sender, args.Data);
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                logger?.LogError(sender, args.Data);
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(token);
        }
    }
}

