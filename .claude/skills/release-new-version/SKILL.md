---
name: release-new-version
description: Perform the new-version release work for the Blazor Minimum Templates NuGet package. Use when the user says "新バージョンに更新して", "新バージョンを作成して", "update to the new version", "release a new version", or similar. Retrieves the latest stable versions of the Microsoft.AspNetCore.Components NuGet packages and the .NET SDK, updates the package references in the templates and the version of the template package itself, runs the test suite, then commits and tags the release.
---

# Blazor Minimum Templates — New Version Release Procedure

This repository is the source of the NuGet package "Toolbelt.AspNetCore.Blazor.Minimum.Templates", a set of `dotnet new` project templates for Blazor.
Whenever a new patch version of ASP.NET Core is released, a new version of this template package must be created with its referenced package versions updated. This document describes that entire procedure.

## Versioning rules (prerequisite knowledge)

- The version of this template package (the `<Version>` element in `Version.props`) must be **the same value as the version number of the latest stable .NET SDK** (e.g., if .NET SDK 10.0.301 is the latest stable, the package version is `10.0.301`).
- However, when releasing an update of package references only, while the .NET SDK version has not changed, use **the SDK version plus a build number (a 4th segment)**. For example: after releasing `10.0.301`, if new NuGet package versions appear while the SDK is still `10.0.301`, the new package version is `10.0.301.1`; one more such release would be `10.0.301.2`.
- The `Microsoft.AspNetCore.Components.*` packages referenced by the templates must be updated to the latest stable version of each of the major version lines **8, 9, and 10**.
- The reference versions for net6.0 / net7.0 (6.0.x, 7.0.20) are **frozen because those runtimes are out of support. Never change them.**
- Never adopt preview / rc versions. Only stable versions are eligible.

## Step 1: Gather the new version information

### 1-a. Latest stable versions of the NuGet packages

Fetch https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.components/index.json (a JSON list of all versions of Microsoft.AspNetCore.Components in ascending order). From this list, determine the latest stable version for each of the major version lines 8, 9, and 10 (e.g., `8.0.27`, `9.0.16`, `10.0.8`). Ignore any version that has a pre-release suffix such as `-preview` or `-rc`.

> Fallback if the URL above is unavailable: refer to https://www.nuget.org/packages/Microsoft.AspNetCore.Components/atom.xml (an Atom feed of recent releases).

### 1-b. Latest stable version of the .NET SDK

For the .NET SDK with the largest major version among the **stable** releases (currently .NET 10), fetch https://dotnet.microsoft.com/en-us/download/dotnet/10.0 with WebFetch and determine the latest **stable** SDK version (e.g., `10.0.301`). Do not adopt preview / rc versions.

> Fallback if the version cannot be determined from that page: refer to the `latest-sdk` field of https://builds.dotnet.microsoft.com/dotnet/release-metadata/10.0/releases.json (verify that it is a stable version with no suffix such as `-preview`).

### 1-c. Compare with the current state and decide the new package version

Read the current version in `Version.props` and the current reference versions in `Content/BlazorMIn/BlazorMin.1.Client/BlazorMin.1.Client.csproj`, and compare them with the values obtained in Steps 1-a / 1-b.

- **If neither the package references nor the SDK version has changed**: no work is needed. Report that to the user and stop.
- **If the package references have new versions but the SDK version has not changed**: the new package version is "SDK version + build number". That is, if the current `Version.props` says `10.0.301`, the new version is `10.0.301.1`; if it already says `10.0.301.1`, increment the build number to `10.0.301.2`.
- If only some of the major version lines have updates (e.g., only 10.0.x is new), update only those lines.

## Step 2: Modify the files

Modify the following files. `{new}` is the new package version decided in Step 1-c (normally the SDK version obtained in Step 1-b; if the SDK has not changed, the version with the build number appended). `{old}` is the current version in `Version.props`.

### 2-1. Template csproj files (4 files)

In each file, rewrite the `Version` attribute of the `<PackageReference>` elements inside the ItemGroups conditioned on `'$(Framework)' == 'net8.0'` / `'net9.0'` / `'net10.0'` to the latest version of each line obtained in Step 1-a. **Do not touch the net6.0 / net7.0 ItemGroups.**

| File | PackageReference(s) to update |
|---|---|
| `Content/BlazorMIn/BlazorMin.1/BlazorMin.1.csproj` | `Microsoft.AspNetCore.Components.WebAssembly.Server` × 3 lines |
| `Content/BlazorMIn/BlazorMin.1.Client/BlazorMin.1.Client.csproj` | `Microsoft.AspNetCore.Components.WebAssembly` × 3 lines |
| `Content/BlazorWasmMin/Client/BlazorWasmMin.1.Client.csproj` | `Microsoft.AspNetCore.Components.WebAssembly` and `...WebAssembly.DevServer` × 3 lines |
| `Content/BlazorWasmMin/Server/BlazorWasmMin.1.Server.csproj` | `Microsoft.AspNetCore.Components.WebAssembly.Server` × 3 lines |

(`Content/BlazorServerMin` has no PackageReference, so it is out of scope.)

### 2-2. `Version.props`

`<Version>{old}</Version>` → `<Version>{new}</Version>`

### 2-3. `test/VersionInfo.cs`

`VersionText = "{old}"` → `VersionText = "{new}"`

### 2-4. `README.md`

`Toolbelt.AspNetCore.Blazor.Minimum.Templates::{old}` → `::{new}` (2 occurrences, in the install command examples).

### 2-5. `RELEASE-NOTES.txt`

Add the following entry at the top of the file (insert above the existing entries, separated by one blank line):

```
v.{new}
- Update: Updated the referenced packages to 8.0.27, 9.0.16, and 10.0.8.
```

- List only the version lines that were actually updated, in ascending order.
  - All 3 lines updated: `8.0.27, 9.0.16, and 10.0.8`
  - Only 1 line updated: `10.0.7`

### 2-6. Update the packages referenced by the test project

Run the following in the `test` folder to update all NuGet packages referenced by the test project (Microsoft.NET.Test.Sdk, NUnit, etc.) to their latest versions:

```
dotnet package update
```

This rewrites `test/Blazor.Minimum.Templates.Test.csproj`. Include this change in the release commit as well.

### 2-7. Check for leftovers

Use Grep to verify that the old version string `{old}` no longer remains anywhere in the repository (occurrences in the history entries of RELEASE-NOTES.txt, and anything under bin/obj/dist/work/TestResults, may be ignored).

## Step 3: Verify with the test suite

Run `dotnet test` in the `test` folder.

- This test suite builds the template package with `dotnet pack`, reinstalls it on the local machine with `dotnet new install`, and then verifies project generation, build, and execution for each combination of template × options. **It can take tens of minutes to complete**, so launch it as a background task and wait for it to finish.
- If any test fails, do not commit; investigate the failure and report it to the user.

## Step 4: Commit and tag

Once all tests pass:

1. Stage all the changed files and commit. Following the style of the past history, the commit message lists only the version lines that were updated:
   - Example (3 lines): `Updated package references to 8.0.27, 9.0.16, and 10.0.8`
   - Example (1 line): `Updated package references to 10.0.7`
2. Tag that commit in the `v.{new}` format (e.g., `git tag v.10.0.301`).

**Do not run `git push` or publish to NuGet (`dotnet nuget push`).** The user performs those steps manually.

## Step 5: Final report

Report the following to the user:

- The updated package versions (per version line) and the new template package version
- The test project packages updated by `dotnet package update`
- The test results (number of tests)
- The commit and tag that were created
- A reminder that pushing and publishing to NuGet are the user's manual steps
