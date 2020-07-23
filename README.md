# Blazor WebAssembly App (client-side) Minimum Project Templates [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.AspNetCore.Blazor.Minimum.Templates.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/)

## Summary

This is project templates package for ASP.NET Core **"Blazor WebAssembly App"** (a.k.a "client-side Blazor") without JavaScript and CSS libraries, designed for dotnet CLI.

> Blazor WebAssembly (client-side Blazor) is a .NET web framework using **C#** and HTML that **runs in the browser**. [Learn More...](https://blazor.net/)

The Blazor WebAssembly application project, which is created by this template, contains only the minimum necessary files, like this.

![fig.1](https://github.com/jsakamoto/BlazorMinimumTemplates/raw/master/.assets/fig-001.png)

When you run this project, the following page will be displayed in a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/fig-002.png)

## System requirement

- [.NET Core SDK (3.1.300 or later)](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## How to install

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates::3.2.1
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

Therefore, enter the "dotnet new blazorwasmmin" command on the terminal, and after you open the workspace by Visual Studio Code, you can execute "Start Without Debugging" (Ctrl   F5) task immediately. 

## Appendix: Upgrading guide to the newer version of Blazor

### Upgrade to Blazor 3.2 Release Candidate 1 to Officially Release

See also: [Blazor WebAssembly 3.2.0 now available](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-now-available/)

### Upgrade to Blazor 3.2 Release Candidate 1

See also: [Blazor WebAssembly 3.2.0 Release Candidate now available](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-release-candidate-now-available/)

### Upgrade to Blazor 3.2 Preview 5

See also: [Blazor WebAssembly 3.2.0 Preview 5 release now available](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-preview-5-release-now-available/)

### Upgrade to Blazor 3.2 Preview 4

See also: [Blazor WebAssembly 3.2.0 Preview 4 release now available](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-preview-4-release-now-available/)

## License

[The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
