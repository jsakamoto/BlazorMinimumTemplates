# Blazor Minimum Project Templates [![NuGet Package](https://img.shields.io/badge/nuget-v0.9.0-orange.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/0.9.0-preview3-19154-02/)

## Summary

This is project templates package for ASP.NET Core **"Blazor"** without JavaScript and CSS libraries, designed for dotnet CLI.

> Blazor is an experimental .NET web framework using **C#** and HTML that **runs in the browser**. [Learn More...](https://blazor.net/)

The Blazor application project, which is created by this template, contains only the minimum necessary files, like this.

![fig.1](https://github.com/jsakamoto/BlazorMinimumTemplates/raw/master/.assets/fig-001.png)

When you run this project, the following page will be displayed in a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/fig-002.png)

## System requirement

- [.NET Core 3.0 Preview 3 SDK (3.0.100-preview3-010431)](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## How to install

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates::0.9.0-preview3-19154-02
```

## How to use

If you want to create a new Blazor application project which standalone edition, type `dotnet new` command with a short name of the template "**blazormin**".

```shell
> dotnet new blazormin
```

If you want a ASP.NET Core hosted edition, use a short name of the template "**blazorhostedmin**".

```shell
> dotnet new blazorhostedmin
```

These commands are create a new project in current directory, and the project name is same as the current directory name.

See also: ["dotnet new command - .NET Core CLI" | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new)

## Known issues

There are a couple of known issues with this release that you may run into:

- **"It was not possible to find any compatible framework version. The specified framework 'Microsoft.NETCore.App', version '2.0.0' was not found."**: You may see this error when building a Blazor app because the IL linker currently requires .NET Core 2.x to run. To work around this issue, either install .NET Core 2.2 or disable IL linking by setting the `<BlazorLinkOnBuild>false</BlazorLinkOnBuild>` property in your project file.
- **"Unable to generate deps.json, it may have been already generated."**: You may see this error when running a standalone Blazor app and you haven't yet restored packages for any .NET Core apps. To workaround this issue create any .NET Core app (ex dotnet new console) and then rerun the Blazor app.

## Upgrade to Blazor 0.9.0 from 0.8.0

See also: **[Blazor 0.9.0 experimental release now available](https://devblogs.microsoft.com/aspnet/blazor-0-9-0-experimental-release-now-available/)**

## Upgrade to Blazor 0.8.0

See also: **[ASP.NET Blog - Blazor 0.8.0 experimental release now available](https://blogs.msdn.microsoft.com/webdev/2019/02/05/blazor-0-8-0-experimental-release-now-available/)**

## License

[The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
