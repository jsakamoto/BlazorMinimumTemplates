# Blazor WebAssembly App (client-side) Minimum Project Templates [![NuGet Package](https://img.shields.io/badge/nuget-v3.2.0%20Preview%202-orange.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/3.2.0-preview2.20160.5)

## Summary

This is project templates package for ASP.NET Core **"Blazor WebAssembly App"** (a.k.a "client-side Blazor") without JavaScript and CSS libraries, designed for dotnet CLI.

> Blazor WebAssembly (client-side Blazor) is a .NET web framework using **C#** and HTML that **runs in the browser**. [Learn More...](https://blazor.net/)

The Blazor WebAssembly application project, which is created by this template, contains only the minimum necessary files, like this.

![fig.1](https://github.com/jsakamoto/BlazorMinimumTemplates/raw/master/.assets/fig-001.png)

When you run this project, the following page will be displayed in a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/fig-002.png)

## System requirement

- [.NET Core 3.1 SDK (3.1.102)](https://dotnet.microsoft.com/download/dotnet-core/3.1) or later

## How to install

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates::3.2.0-preview2.20160.5
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

### Upgrade to Blazor 3.2 Preview 2

See also: [Blazor WebAssembly 3.2.0 Preview 2 release now available](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-preview-2-release-now-available/)

### Upgrade to Blazor 3.2 Preview 1

See also: [Blazor WebAssembly 3.2.0 Preview 1 release now available](https://devblogs.microsoft.com/aspnet/blazor-webassembly-3-2-0-preview-1-release-now-available/)

### Upgrade to Blazor 3.1 Preview 4

See also: [ASP.NET Core updates in .NET Core 3.1](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-core-3-1/)

### Upgrade to Blazor 3.1 Preview 2

See also: [ASP.NET Core updates in .NET Core 3.1 Preview 2](https://devblogs.microsoft.com/aspnet/asp-net-core-updates-in-net-core-3-1-preview-2/)

### Upgrade to Blazor 3.0 Preview 9 from Preview 8

See also: [ASP.NET Core and Blazor updates in .NET Core 3.0 Preview 9](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0-preview-9/)

### Upgrade to Blazor 3.0 Preview 8 from Preview 7

See also: [ASP.NET Core and Blazor updates in .NET Core 3.0 Preview 8](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0-preview-8/)

### Upgrade to Blazor 3.0 Preview 7 from Preview 6

See also: [ASP.NET Core and Blazor updates in .NET Core 3.0 Preview 7](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0-preview-7/)

### Upgrade to Blazor 3.0 Preview 6 from Preview 4, 5

See also: [ASP.NET Core and Blazor updates in .NET Core 3.0 Preview 6](https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0-preview-6/)

### Upgrade to Blazor 3.0 Preview 4 from 0.9.0

See also: [Blazor now in official preview!](https://devblogs.microsoft.com/aspnet/blazor-now-in-official-preview/)

### Upgrade to Blazor 0.9.0 from 0.8.0

See also: [Blazor 0.9.0 experimental release now available](https://devblogs.microsoft.com/aspnet/blazor-0-9-0-experimental-release-now-available/)

### Upgrade to Blazor 0.8.0

See also: [ASP.NET Blog - Blazor 0.8.0 experimental release now available](https://blogs.msdn.microsoft.com/webdev/2019/02/05/blazor-0-8-0-experimental-release-now-available/)

## License

[The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
