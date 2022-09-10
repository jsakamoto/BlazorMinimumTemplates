# Blazor WebAssembly App (client-side) Minimum Project Templates [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.AspNetCore.Blazor.Minimum.Templates.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/)

## Summary

This is project templates package for ASP.NET Core **"Blazor WebAssembly App"** (a.k.a "client-side Blazor") without JavaScript and CSS libraries, designed for dotnet CLI.

> Blazor WebAssembly (client-side Blazor) is a .NET web framework using **C#** and HTML that **runs in the browser**. [Learn More...](https://blazor.net/)

The Blazor WebAssembly application project, which is created by this template, contains only the minimum necessary files, like this.

![fig.1](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.2/fig-001.png)

When you run this project, the following page will be displayed in a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.2/fig-002.png)

## System requirement

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

## How to install

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates
```

## How to use

### .NET CLI

If you want to create a new Blazor WebAssembly application project which standalone edition, type `dotnet new` command with a short name of the template "**blazorwasmmin**".

```shell
> dotnet new blazorwasmmin
```

These commands create a new project in current directory, and the project name is same as the current directory name.

See also: ["dotnet new command - .NET Core CLI" | Microsoft Docs](https://docs.microsoft.com/dotnet/core/tools/dotnet-new)

You can see all the options for this project template with the following command.

```shell
> dotnet new blazorwasmmin --help
```

option | description
-------|-----------------
`--no-restore`    | If specified, skips the automatic restore of the project on create.
`--no-https`      | Whether to turn off HTTPS.
`-ho`, `--hosted` | If specified, includes an ASP.NET Core host for the Blazor WebAssembly app.
`-r`, `--routing` | If specified, enables routing for the Blazor WebAssembly app.
`-la`, `--layout` | If specified, enables shared layout for the Blazor WebAssembly app.
`-s`, `--solution` | If specified, adds a solution file for the standalone Blazor WebAssembly app.

For example, if you want an ASP.NET Core hosted edition, please specify the `--hosted` switch.

```shell
> dotnet new blazorwasmmin --hosted
```

### Visual Studio

Once you've installed this project template with the `dotnet new -i ...` command, you will see this project template in your Visual Studio's "Create new project" dialog.

![fig.3](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.2/fig-003.png)

When you proceed with the create a new project with the project template "Blazor WebAssembly App (minimal)", you can see the "Additional Information" dialog, and you can configure some options for the Blazor WebAssemnly app you are creating.

![fig.4](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.2/fig-004.png)

## License

- [The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
- The third party notice is [here.](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/THIRD-PARTY-NOTICES.txt)
- "FreeConverter.com" (https://www.freeconvert.com/png-to-svg) was used to vectorize Blazor logo.
