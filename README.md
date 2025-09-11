# Blazor Minimum Project Templates

[![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.AspNetCore.Blazor.Minimum.Templates.svg)](https://www.nuget.org/packages/Toolbelt.AspNetCore.Blazor.Minimum.Templates/) [![Discord](https://img.shields.io/discord/798312431893348414?style=flat&logo=discord&logoColor=white&label=Blazor%20Community&labelColor=5865f2&color=gray)](https://discord.com/channels/798312431893348414/1202165955900473375)

## Summary

This is a project templates package for **"Blazor Server"**, **"Blazor WebAssembly"**, and **Full Stack "Blazor Web"** application without JavaScript and CSS libraries.

> Blazor is a framework for building **full stack web apps** by **C#**, without writing a line of JavaScript. [Learn More...](https://blazor.net/)

The Blazor application project created by these templates contains only the minimum necessary files, like this:

![fig.1](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.2/fig-001.png)

When you run this project, you will see the following page on a web browser.

![fig.2](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.4/fig-002.png)

## System requirement

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet/) ver.6, 7, 8, 9, 10 or later

## How to install

If you use .NET SDK **ver.7 or later**, please enter the following command to install the project templates.

```shell
> dotnet new install Toolbelt.AspNetCore.Blazor.Minimum.Templates::10.0.100-rc.1
```

If you use .NET SDK **ver.6**, please enter the following command to install the project templates.

```shell
> dotnet new -i Toolbelt.AspNetCore.Blazor.Minimum.Templates::10.0.100-rc.1
```

## How to use

### .NET CLI

If you want to create a new Blazor **Server app** project, type the `dotnet new` command with the short name of the template "**blazorservermin**".

```shell
> dotnet new blazorservermin
```

If you want to create a new Blazor **WebAssembly app** project which standalone edition, type the `dotnet new` command with the short name of the template "**blazorwasmmin**".

```shell
> dotnet new blazorwasmmin
```

If you want to create a new Blazor **Web app** project that is a new model since .NET 8, type the `dotnet new` command with the short name of the template "**blazormin**".

```shell
> dotnet new blazormin
```

These commands create a new project in the current directory, and the project name will be the same as the current directory name.

See also: ["dotnet new command - .NET Core CLI" | Microsoft Docs](https://docs.microsoft.com/dotnet/core/tools/dotnet-new)

You can see all the options for these project templates with the `--help` option, like below:

```shell
> dotnet new blazorwasmmin --help
```

option | description
-------|-----------------
`--no-restore`    | If specified, skips the automatic restore of the project on create.
`-f`, `--framework` {`net6.0`, `net7.0`, `net8.0`, or `net9.0`} |  The target framework for the project.
`--no-https`      | Whether to turn off HTTPS.
`-ho`, `--hosted` | **[Blazor WebAssembly Only]** If specified, includes an ASP.NET Core host for the Blazor WebAssembly app.
`-r`, `--routing` | If specified, enables routing for the Blazor app.
`--layout` | If specified, enables shared layout for the Blazor app.
`-int`, `--interactivity` {`none`, `server`, `webassembly`, or `auto`}    | **[Blazor Web app only]** Chooses which interactive render mode to use for interactive components. (The default value is `none`) 
`--localhost-tld` | If specified, uses the .dev.localhost TLD in the application URL for local development.
`-s`, `--solution` | **[.NET CLI only]** If specified, adds a solution file for the standalone Blazor WebAssembly or Blazor Server app.
`-sx`, `--solutionx` | **[.NET CLI only]** If specified, adds an XML solution file for the standalone Blazor WebAssembly or Blazor Server app.

For example, if you want an ASP.NET Core hosted Blazor WebAssembly app project, please specify the `--hosted` switch.

```shell
> dotnet new blazorwasmmin --hosted
```

### Visual Studio

Once you've installed this project template with the `dotnet new install ...` command, you will see these project templates in your Visual Studio's "Create new project" dialog.

![fig.3](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.3/fig-003.png)

When you proceed with creating a new project with the project template "Blazor Server App (minimal)", "Blazor WebAssembly App (minimal)", or "Blazor Web App (minimal)", you can see the "Additional Information" dialog, and you can configure some options for the Blazor app you are creating.

![fig.4](https://raw.githubusercontent.com/jsakamoto/BlazorMinimumTemplates/master/.assets/rev.3/fig-004.png)

## What is the difference between the empty template built-in .NET SDK?

- .NET 6.0 support
- Rich initial loading page indicator for Blazor WebAssembly. (The normal project template of Blazor WebAssembly on .NET SDK has an excellent progress indication loading page. but the empty project template only shows "Loading..." static text at the top left corner of the page.)
- No routing by default. (You can also enable routing support explicitly)
- No shared layout by default. (You can also enable shared layout explicitly)

In short, this "minimal" template will generate a **much simpler** project structure and files (by default, it doesn't even have routing code and shared layout code) than the Blazor empty template.

## License

- [The Unlicense](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/LICENSE)
- The third party notice is [here.](https://github.com/jsakamoto/BlazorMinimumTemplates/blob/master/THIRD-PARTY-NOTICES.txt)
- "FreeConverter.com" (https://www.freeconvert.com/png-to-svg) was used to vectorize Blazor logo.
