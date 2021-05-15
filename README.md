# Blazor WebAssembly App (client-side) Minimum Project Templates [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.AspNetCore.Blazor.Minimum.Templates.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/)

## Summary

This is project templates package for ASP.NET Core **"Blazor WebAssembly App"** (a.k.a "client-side Blazor") without JavaScript and CSS libraries, designed for dotnet CLI.

> Blazor WebAssembly (client-side Blazor) is a .NET web framework using **C#** and HTML that **runs in the browser**. [Learn More...](https://blazor.net/)

The Blazor WebAssembly application project, which is created by this template, contains only the minimum necessary files, like this.

![fig.1](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/fig-001.png)

When you run this project, the following page will be displayed in a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/fig-002.png)

## System requirement

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

## How to install

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates::6.0.0-preview.3
```

## How to use

If you want to create a new Blazor WebAssembly application project which standalone edition, type `dotnet new` command with a short name of the template "**blazorwasmmin**".

```shell
> dotnet new blazorwasmmin
```

If you want a ASP.NET Core hosted edition, use a short name of the template "**blazorwasmhostedmin**".

```shell
> dotnet new blazorwasmhostedmin
```

These commands create a new project in current directory, and the project name is same as the current directory name.

See also: ["dotnet new command - .NET Core CLI" | Microsoft Docs](https://docs.microsoft.com/dotnet/core/tools/dotnet-new)

## Visual Studio Code support

The project files contain "build" & "launch" task configuration for Visual Studio Code.

Therefore, enter the "dotnet new blazorwasmmin" command on the terminal, and after you open the workspace by Visual Studio Code, you can execute "Start Without Debugging" (Ctrl + F5) task immediately. 

## Appendix: Upgrading guide to the newer version of Blazor

### Upgrade .NET 5 Release Candidat 1 to .NET 5 Release Candidate 2

See also: [ASP.NET Core updates in .NET 5 Release Candidate 2](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-5-release-candidate-2/)

### Upgrade .NET 5 Preview 8 to .NET 5 Release Candidate 1

See also: [ASP.NET Core updates in .NET 5 Release Candidate 1](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-5-release-candidate-1/)

### Upgrade Blazor 3.2 to .NET 5 Preview 8

See also: [ASP.NET Core updates in .NET 5 Preview 8](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-5-preview-8/)

## License

[The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
