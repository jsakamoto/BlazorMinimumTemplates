---
name: release-new-preview-rc-version
description: Perform the new-version release work for the Blazor Minimum Templates NuGet package targeting a preview or release candidate (RC) version of .NET. Use when the user says "プレビューバージョンに対応した新バージョンを作成して", "RC バージョンに対応した新バージョンを作成して", "release a new preview version", "update to the new preview/RC", or similar. Finds the preview/RC .NET SDK, works on the dedicated "netNN" branch (merging master into it first if it is behind), updates the preview package references and the template package version, runs the test suite, then commits and tags the release on that branch.
---

# Blazor Minimum Templates — Preview / RC Version Release Procedure

This repository is the source of the NuGet package "Toolbelt.AspNetCore.Blazor.Minimum.Templates", a set of `dotnet new` project templates for Blazor.
While a new major version of .NET is in its preview / release candidate (RC) phase, preview releases of this template package are published from a dedicated Git branch (e.g., `net11` for .NET 11). This document describes that procedure.

## ⚠️ CRITICAL: preview / RC versions ARE the target here

This is **the exact opposite of the `release-new-version` skill**. That skill strictly forbids adopting versions with `-preview` / `-rc` suffixes — but in THIS procedure, the preview / RC versions are precisely what you are looking for and must adopt. Do not let the rules of the `release-new-version` skill leak into this work and cause you to skip or reject the preview/RC versions you came for. (The stable 8/9/10 version lines, on the other hand, are still maintained on `master` by the `release-new-version` skill and flow into the preview branch via merges — you do not update them directly here.)

## Versioning rules (prerequisite knowledge)

- `NN` below denotes the major version of the .NET preview/RC (e.g., `11` while .NET 11 is in preview).
- The version of this template package (the `<Version>` element in `Version.props`) is **the same value as the version number of the preview/RC .NET SDK** (e.g., if the .NET 11 SDK is `11.0.100-preview.4`, the package version is `11.0.100-preview.4`; an RC SDK `11.0.100-rc.1` gives `11.0.100-rc.1`).
- The `Microsoft.AspNetCore.Components.*` package references for the `netNN.0` target use a **floating wildcard version**: `11.0.0-preview.4.*` (or `11.0.0-rc.1.*`), so only the preview/RC number matters, not the exact build number.
- Work is done **on the `netNN` branch, never on `master`**.

## Step 1: Find the preview / RC .NET SDK

1. Fetch https://dotnet.microsoft.com/en-us/download/dotnet and identify the .NET version currently in preview / RC (e.g., ".NET 11.0" marked as Preview or Release Candidate). If no preview/RC version of .NET exists at the moment, report that to the user and stop.
2. Fetch the version-specific page (e.g., https://dotnet.microsoft.com/en-us/download/dotnet/11.0) and determine the latest preview/RC SDK version (e.g., `11.0.100-preview.4`). This becomes the new template package version.
   > Fallback: refer to the `latest-sdk` field of https://builds.dotnet.microsoft.com/dotnet/release-metadata/11.0/releases.json.
3. Verify on https://api.nuget.org/v3-flatcontainer/microsoft.aspnetcore.components/index.json that a matching preview/RC version of the NuGet package exists (e.g., a version starting with `11.0.0-preview.4.`). The csproj files will reference it in the wildcard form `11.0.0-preview.4.*`.

## Step 2: Check out the `netNN` branch

Make sure the working tree is clean first.

- If the branch `netNN` (e.g., `net11`) already exists, simply check it out.
- If it does not exist yet (i.e., this is the very first preview release for this major version), create it from `master` (`git switch -c netNN master`). In this first-time case, the templates do not yet support `netNN.0` at all, and a substantial one-time setup is required: adding `netNN.0` ItemGroups to the four template csproj files, adding the framework choice to the three `.template.config/template.json` files, bumping the `TargetFramework` of `Toolbelt.AspNetCore.Blazor.Minimum.Templates.csproj` and the test project, and adding the new framework to the test code (`test/Internals/TargetFramework.cs` and the test case sources). Study the diff of the commit `b4dadcd` ("Add support for .NET 11.0 preview and bump version to 11.0.100-preview.1") on the `net11` branch as the reference model, and replicate it for the new major version.

## Step 3: Merge `master` into the branch if it is behind

Check whether the branch is behind `master`:

```
git log --oneline netNN..master
```

If this lists any commits, merge `master` into the `netNN` branch with **no fast-forward**:

```
git merge master --no-ff
```

(The default merge commit message, `Merge branch 'master' into netNN`, is the convention used in the history.)

This merge will almost certainly produce conflicts. Resolve them carefully, file by file:

- **`README.md`, `Version.props`, `test/VersionInfo.cs`**: keep the **preview branch side** as-is (these carry the preview version, e.g. `11.0.100-preview.3`; do NOT take the stable version from `master`).
- **`RELEASE-NOTES.txt`**: keep **both** sides, paying close attention to the version numbers of the entries: the preview-version entries (from the branch) must come **above** the entries coming from `master` — i.e., "use preview before master". The result must read, from top to bottom: all `v.NN.x-preview/rc` entries, then the new stable entries from `master`, then the older common history.
- **Template csproj files** (under `Content/`): keep the `netNN.0` ItemGroups from the branch AND take the updated stable (net8.0 / net9.0 / net10.0) versions from `master`. These usually auto-merge without conflict since the `netNN.0` ItemGroups are separate blocks.
- **`test/Blazor.Minimum.Templates.Test.csproj`**: take the `master` side (it carries the freshly updated test package versions).
- Any other conflict: think it through carefully and resolve it in whatever way preserves both the preview support of the branch and the improvements from `master`.

After resolving all conflicts, complete the merge commit. Then verify the result builds correctly in spirit: grep for leftover conflict markers (`<<<<<<<`) before committing.

If the branch is NOT behind `master`, skip this step entirely.

## Step 4: Update the files for the new preview/RC version

Equivalent to Step 2 of the `release-new-version` skill, but for the preview/RC version. `{new}` is the SDK version from Step 1 (e.g., `11.0.100-preview.4`), `{old}` is the current version in `Version.props` (e.g., `11.0.100-preview.3`). `{pkg}` is the preview/RC package version prefix (e.g., `11.0.0-preview.4`).

If `Version.props` already equals `{new}` (no new preview/RC since the last release) and the merge in Step 3 was not needed either, report that there is nothing to do and stop. If only the merge happened but no new SDK exists, consult the user about whether to release with a 4th-segment build number appended to `{old}` (this case has no precedent in the history).

### 4-1. Template csproj files (4 files)

In each file, rewrite the `Version` attribute of the `<PackageReference>` elements **only inside the ItemGroups conditioned on `'$(Framework)' == 'netNN.0'`** to `{pkg}.*` (wildcard form, e.g., `11.0.0-preview.4.*`). Do not touch the net8.0 / net9.0 / net10.0 ItemGroups (they are maintained on `master`), nor the frozen net6.0 / net7.0 ones.

| File | PackageReference(s) to update |
|---|---|
| `Content/BlazorMIn/BlazorMin.1/BlazorMin.1.csproj` | `Microsoft.AspNetCore.Components.WebAssembly.Server` |
| `Content/BlazorMIn/BlazorMin.1.Client/BlazorMin.1.Client.csproj` | `Microsoft.AspNetCore.Components.WebAssembly` |
| `Content/BlazorWasmMin/Client/BlazorWasmMin.1.Client.csproj` | `Microsoft.AspNetCore.Components.WebAssembly` and `...WebAssembly.DevServer` |
| `Content/BlazorWasmMin/Server/BlazorWasmMin.1.Server.csproj` | `Microsoft.AspNetCore.Components.WebAssembly.Server` |

### 4-2. `Version.props`

`<Version>{old}</Version>` → `<Version>{new}</Version>`

### 4-3. `test/VersionInfo.cs`

`VersionText = "{old}"` → `VersionText = "{new}"`

### 4-4. `README.md`

`Toolbelt.AspNetCore.Blazor.Minimum.Templates::{old}` → `::{new}` (2 occurrences, in the install command examples).

### 4-5. `RELEASE-NOTES.txt`

Add the following entry at the top of the file (insert above the existing entries, separated by one blank line). Following the convention of past preview entries, there is no trailing period:

```
v.{new}
- Update: Updated the referenced packages to {pkg}
```

### 4-6. Do NOT run `dotnet package update`

Unlike the `release-new-version` skill, do not update the test project's package references here. They are maintained on `master` and arrive on this branch via the merge in Step 3. Updating them here would cause unnecessary divergence.

### 4-7. Check for leftovers

Use Grep to verify that the old version string `{old}` no longer remains anywhere in the repository (occurrences in the history entries of RELEASE-NOTES.txt, and anything under bin/obj/dist/work/TestResults, may be ignored).

## Step 5: Verify with the test suite

Building the `netNN.0` templates requires the preview/RC .NET SDK to be installed locally. Check with `dotnet --list-sdks` first; if the required SDK version (`{new}`) is not installed, stop and ask the user to install it — do not attempt to install an SDK yourself.

Then run `dotnet test` in the `test` folder.

- This test suite builds the template package with `dotnet pack`, reinstalls it on the local machine with `dotnet new install`, and then verifies project generation, build, and execution for each combination of template × options. **It can take tens of minutes to complete**, so launch it as a background task and wait for it to finish.

### If some tests fail: rerun only the failed tests

Test failures in this suite are sometimes flaky (e.g., nondeterministic static-web-assets build races, which are all the more likely here since a preview/RC .NET SDK is in use). When only a few tests fail, do **not** rerun the whole suite — rerun just the failed tests and see whether they pass this time.

The test project runs on the Microsoft.Testing.Platform (MTP) runner (`EnableNUnitRunner`), which supports the VSTest-style `--filter` option (NOT `--treenode-filter`), passed after `--`. Test names in this suite are parameterized (e.g., `DotNetNewAndBuild_Test(Auto,NoRouting,Layout,"net11.0",Solution)`); to match one exactly with the `Name~` (contains) operator, **escape the parentheses with a backslash** — commas and double quotes need no escaping. Unescaped parentheses are silently parsed as filter-grammar grouping and match ALL tests, so never omit the escapes. Example, rerunning that one failed test (quote the filter with single quotes in PowerShell):

```
dotnet test -- --filter 'Name~DotNetNewAndBuild_Test\(Auto,NoRouting,Layout,"net11.0",Solution\)'
```

- If multiple tests failed, combine the conditions with `|`:
  `--filter 'Name~Test_A\(...\)|Name~Test_B\(...\)'`
- Before the actual rerun, verify the filter matches exactly the intended test case(s) with `--list-tests` (fast, runs nothing): `dotnet test -- --list-tests --filter '...'` — the discovery summary prints the matched count (a filter matching 0 tests exits with code 8). Also confirm from the rerun output that the number of executed tests equals the number of intended test cases.
- **If all the failed tests pass on the rerun**: treat the original failures as flaky, consider the suite green, and proceed to the next step. Mention in the final report that flaky failures occurred and were cleared by a rerun.
- **If any test fails again on the rerun**: do not commit; investigate the failure and report it to the user.

## Step 6: Commit and tag (on the `netNN` branch)

Once all tests pass, confirm you are still on the `netNN` branch, then:

1. Stage all the changed files and commit. Following the style of the past history:
   - `Update package references to 11.0.0-preview.4`
2. Tag that commit in the `v.{new}` format (e.g., `git tag v.11.0.100-preview.4`).

**Do not run `git push` or publish to NuGet (`dotnet nuget push`).** The user performs those steps manually.

## Step 7: Final report

Report the following to the user:

- The new preview/RC SDK version found, and the new template package version
- Whether a merge from `master` was performed, and how each conflict was resolved
- The test results (number of tests)
- The commit and tag that were created (and on which branch)
- A reminder that pushing and publishing to NuGet are the user's manual steps
