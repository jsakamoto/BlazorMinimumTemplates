namespace Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test;
using static Toolbelt.Diagnostics.XProcess;

[SetUpFixture]
public class TestSetup
{
    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        // Build the Blazor Minimal Templates
        var projectDir = Toolbelt.FileIO.FindContainerDirToAncestor("Toolbelt.AspNetCore.Blazor.Minimum.Templates.csproj"); 
        var dotnetBuild = await Start("dotnet", "pack", projectDir).WaitForExitAsync();
        dotnetBuild.ExitCode.Is(0, message: dotnetBuild.Output);

        // Uninstall and reinstall the templates package
        var packagePath = Path.Combine(projectDir, "dist", $"Toolbelt.AspNetCore.Blazor.Minimum.Templates.{VersionInfo.VersionText}.nupkg");
        await Start("dotnet", "new uninstall Toolbelt.AspNetCore.Blazor.Minimum.Templates").WaitForExitAsync();
        var dotnetNewInstall = await Start("dotnet", $"new install \"{packagePath}\"").WaitForExitAsync();
        dotnetNewInstall.ExitCode.Is(0, message: dotnetNewInstall.Output);
    }
}
