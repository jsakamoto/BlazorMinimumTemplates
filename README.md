# Blazor (client-side) Minimum Project Templates [![NuGet Package](https://img.shields.io/badge/nuget-v0.9.0-orange.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/0.9.0-preview3-19154-02/)

## Summary

This is project templates package for ASP.NET Core **"Blazor"** without JavaScript and CSS libraries, designed for dotnet CLI.

> Blazor is a .NET web framework using **C#** and HTML that **runs in the browser**. [Learn More...](https://blazor.net/)

The Blazor application project, which is created by this template, contains only the minimum necessary files, like this.

![fig.1](https://github.com/jsakamoto/BlazorMinimumTemplates/raw/master/.assets/fig-001.png)

When you run this project, the following page will be displayed in a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/fig-002.png)

## System requirement

- [.NET Core 3.0 Preview 4 SDK (3.0.100-preview4-011223)](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## How to install

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates::3.0.0-preview4-19216-03b
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

## Upgrade to Blazor 3.0 Preview 4 from 0.9.0

See also: **[Blazor now in official preview!](https://devblogs.microsoft.com/aspnet/blazor-now-in-official-preview/)**

## Upgrade to Blazor 0.9.0 from 0.8.0

See also: **[Blazor 0.9.0 experimental release now available](https://devblogs.microsoft.com/aspnet/blazor-0-9-0-experimental-release-now-available/)**

## Upgrade to Blazor 0.8.0

See also: **[ASP.NET Blog - Blazor 0.8.0 experimental release now available](https://blogs.msdn.microsoft.com/webdev/2019/02/05/blazor-0-8-0-experimental-release-now-available/)**

## License

[The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
