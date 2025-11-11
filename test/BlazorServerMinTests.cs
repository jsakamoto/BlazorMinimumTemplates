using Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test.Internals;
using static Toolbelt.Diagnostics.XProcess;

namespace Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test;

[Parallelizable(ParallelScope.Children)]
public class BlazorServerMinTests
{
    private static int _numberOfApp;

    public static IEnumerable<object[]> GetTestCases()
    {
        return from useRouting in new[] { RoutingOptions.NoRouting, RoutingOptions.Routing, }
               from useLayout in new[] { LayoutOptions.NoLayout, LayoutOptions.Layout }
               from framework in new[] { TargetFramework.Net8, TargetFramework.Net9, TargetFramework.Net10 }
               from solution in new[] { SolutionOptions.None, SolutionOptions.Solution, SolutionOptions.SolutionX }
               select new object[] { useRouting, useLayout, framework, solution };
    }

    [TestCaseSource(nameof(GetTestCases))]
    public async Task DotNetNewAndBuild_Test(RoutingOptions routing, LayoutOptions layout, string framework, SolutionOptions solution)
    {
        Console.WriteLine($"{routing}, {layout}, {framework}, {solution}");

        var numberOfApp = Interlocked.Increment(ref _numberOfApp);
        var appName = $"BlazorServerApp{numberOfApp:D4}";

        using var workDir = new WorkDirectory();

        var dotnetNewArgs = string.Join(' ', new[] {
            "new", "blazorservermin",
            "-n", appName,
            "-o", ".",
            "-f", framework,
            routing == RoutingOptions.Routing ? "-r" : "",
            layout == LayoutOptions.Layout ? "--layout" : "",
            solution.ToCommandLineSwitch(),
        }
        .Where(arg => !string.IsNullOrEmpty(arg)));

        // WHEN
        var dotnetNew = await Start("dotnet", dotnetNewArgs, workDir).WaitForExitAsync();
        dotnetNew.ExitCode.Is(0, message: dotnetNew.Output);

        // THEN: Verify the generated solution file
        var slnFileName = solution.ToSolutionFileName(appName);
        var actualSlnFileName = Directory.GetFiles(workDir.Path, "*.sln*").Select(Path.GetFileName).DefaultIfEmpty("").Single();
        slnFileName.Is(actualSlnFileName);

        // THEN: Verify that the project can be built specifying the solution file
        var dotnetBuildArgs = $"build {slnFileName} -p CompressionEnabled=false";
        var dotnetBuild = await Start("dotnet", dotnetBuildArgs, workDir).WaitForExitAsync();
        dotnetBuild.ExitCode.Is(0, message: dotnetBuild.Output);
    }
}
