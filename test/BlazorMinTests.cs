using Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test.Internals;
using static Toolbelt.Diagnostics.XProcess;

namespace Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test;

[Parallelizable(ParallelScope.Children)]
public class BlazorMinTests
{
    private static int _numberOfApp;

    public static IEnumerable<object[]> GetTestCases()
    {
        return from interactiveMode in new[] { InteractiveMode.None, InteractiveMode.Server, InteractiveMode.WebAssembly, InteractiveMode.Auto }
               from useRouting in new[] { RoutingOptions.NoRouting, RoutingOptions.Routing, }
               from useLayout in new[] { LayoutOptions.NoLayout, LayoutOptions.Layout }
               from framework in new[] { TargetFramework.Net8, TargetFramework.Net9, TargetFramework.Net10 }
               from solution in new[] { SolutionOptions.None, SolutionOptions.Solution, SolutionOptions.SolutionX }
               select new object[] { interactiveMode, useRouting, useLayout, framework, solution };
    }

    [TestCaseSource(nameof(GetTestCases))]
    public async Task DotNetNewAndBuild_Test(InteractiveMode interactiveMode, RoutingOptions routing, LayoutOptions layout, string framework, SolutionOptions solution)
    {
        Console.WriteLine($"{interactiveMode}, {routing}, {layout}, {framework}, {solution}");

        var numberOfApp = Interlocked.Increment(ref _numberOfApp);
        var appName = $"BlazorApp{numberOfApp:D4}";

        using var workDir = new WorkDirectory();

        var dotnetNewArgs = string.Join(' ', new[] {
            "new", "blazormin",
            "-n", appName,
            "-o", ".",
            "-f", framework,
            "-int", interactiveMode.ToString(),
            routing == RoutingOptions.Routing ? "-r" : "",
            layout == LayoutOptions.Layout ? "--layout" : "",
            solution.ToCommandLineSwitch(),
        }
        .Where(arg => !string.IsNullOrEmpty(arg)));

        // WHEN
        var dotnetNew = await Start("dotnet", dotnetNewArgs, workDir).WaitForExitAsync();
        dotnetNew.ExitCode.Is(0, message: dotnetNew.Output);

        // THEN: Verify the generated solution file
        var slnFileName = solution.ToSolutionFileName(appName, forcibly: true);
        var actualSlnFileName = Directory.GetFiles(workDir.Path, "*.sln*").Select(Path.GetFileName).DefaultIfEmpty("").Single();
        slnFileName.Is(actualSlnFileName);

        // THEN: Verify that the project can be built specifying the solution file
        var dotnetBuildArgs = $"build {slnFileName} -p CompressionEnabled=false";
        var dotnetBuild = await Start("dotnet", dotnetBuildArgs, workDir).WaitForExitAsync();
        dotnetBuild.ExitCode.Is(0, message: dotnetBuild.Output);
    }
}