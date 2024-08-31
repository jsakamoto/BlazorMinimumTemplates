using Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test.Internals;
using static Toolbelt.Diagnostics.XProcess;

namespace Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test;

[Parallelizable(ParallelScope.Children)]
public class BlazorWasmMinTests
{
    private static int _numberOfApp;

    public static IEnumerable<object[]> GetTestCases()
    {
        return from useRouting in new[] { RoutingOptions.NoRouting, RoutingOptions.Routing, }
               from useLayout in new[] { LayoutOptions.NoLayout, LayoutOptions.Layout }
               from framework in new[] { TargetFramework.Net6, TargetFramework.Net8 }
               select new object[] { useRouting, useLayout, framework };
    }

    [TestCaseSource(nameof(GetTestCases))]
    public async Task DotNetNewAndBuild_Test(RoutingOptions routing, LayoutOptions layout, string framework)
    {
        Console.WriteLine($"{routing}, {layout}, {framework}");

        var numberOfApp = Interlocked.Increment(ref _numberOfApp);

        using var workDir = new WorkDirectory();

        var args = string.Join(' ', new[] {
            "new", "blazorwasmmin",
            "-n", $"BlazorWasmApp{numberOfApp:D4}",
            "-o", ".",
            "-f", framework,
            routing == RoutingOptions.Routing ? "-r" : "",
            layout == LayoutOptions.Layout ? "--layout" : "",
        }
        .Where(arg => !string.IsNullOrEmpty(arg)));

        var dotnetNew = await Start("dotnet", args, workDir).WaitForExitAsync();
        dotnetNew.ExitCode.Is(0, message: dotnetNew.Output);

        var dotnetBuild = await Start("dotnet", "build", workDir).WaitForExitAsync();
        dotnetBuild.ExitCode.Is(0, message: dotnetBuild.Output);
    }
}
