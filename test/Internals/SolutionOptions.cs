namespace Toolbelt.AspNetCore.Blazor.Minimum.Templates.Test.Internals;

public enum SolutionOptions
{
    None,
    Solution,
    SolutionX
}

public static class SolutionOptionsExtensions
{
    /// <summary>
    /// Convert <see cref="SolutionOptions"/> to the "dotnet new" CLI switch, such as "", "-s", or "-sx".
    /// </summary>
    public static string ToCommandLineSwitch(this SolutionOptions options) => options switch
    {
        SolutionOptions.Solution => "-s",
        SolutionOptions.SolutionX => "-sx",
        _ => "",
    };

    /// <summary>
    /// Returns the solution file name based on the <see cref="SolutionOptions"/> and the application name, like "app.sln" or "app.slnx".
    /// </summary>
    /// <param name="forcibly"> If true, it will return "app.sln" even if the solution option is None.</param>
    public static string ToSolutionFileName(this SolutionOptions options, string appName, bool forcibly = false) => options switch
    {
        SolutionOptions.Solution => $"{appName}.sln",
        SolutionOptions.SolutionX => $"{appName}.slnx",
        _ => forcibly ? $"{appName}.sln" : "",
    };
}