using DiNet.GPipe.JavaBuilder.Helpers;
using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Domain;

public class AndroidStudioApkBuilder(
    JdkSettings jdkSettings,
    SignedReleaseBuildOptions releaseBuildOptions)
{
    private readonly JdkSettings _jdkSettings = jdkSettings;

    public async Task<string?> BuildAsync(string projectPath, ApkBuildType buildType, CancellationToken token)
    {
        EnvironmentHelper.SetUpJdkEnvironment(_jdkSettings);
        EnvironmentHelper.SetUpSDKEnvironment(_jdkSettings);

        switch(buildType){
            case ApkBuildType.ReleaseSigned:

                await GradlewHelper.RunReleaseSignedBuild(projectPath, 
                    releaseBuildOptions.keystorePath, 
                    releaseBuildOptions.storePassword, 
                    releaseBuildOptions.keyAlias, 
                    releaseBuildOptions.keyPassword);
                break;
            case ApkBuildType.Debug:
                await GradlewHelper.RunCleanBuild(projectPath, buildType, token);
                break;
        }


        var path = AndroidStudioProjectExtensions.GetBuildApkPath(projectPath, buildType);

        return File.Exists(path) ? path : null;
    }
}

