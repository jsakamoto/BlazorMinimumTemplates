using Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test.Internals;
using static Toolbelt.Diagnostics.XProcess;

namespace Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test;

[Parallelizable(ParallelScope.Children)]
public class BlazorMinTests
{
    public static IEnumerable<object[]> GetTestCases()
    {
        return from interactiveMode in new[] { InteractiveMode.None, InteractiveMode.Server, InteractiveMode.WebAssembly, InteractiveMode.Auto }
               from useRouting in new[] { RoutingOptions.NoRouting, RoutingOptions.Routing, }
               from useLayout in new[] { LayoutOptions.NoLayout, LayoutOptions.Layout }
               select new object[] { interactiveMode, useRouting, useLayout };
    }

    [TestCaseSource(nameof(GetTestCases))]
    public async Task DotNetNewAndBuild_Test(InteractiveMode interactiveMode, RoutingOptions routing, LayoutOptions layout)
    {
        Console.WriteLine($"{interactiveMode}, {routing} {layout}");

        using var workDir = new WorkDirectory();

        var args = string.Join(' ', new[] {
            "new", "blazormin",
            "-n", $"BlazorApp{Random.Shared.Next(0,1000):D4}",
            "-o", ".",
            "-f", "net8.0",
            "-int", interactiveMode.ToString(),
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