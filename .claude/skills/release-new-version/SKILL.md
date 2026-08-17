---
name: release-new-version
description: Perform the new-version release work for the Blazor Minimum Templates NuGet package. Use when the user says "update to the new version", "create a new version", "release a new version", or similar, in any language. Retrieves the latest stable versions of the Microsoft.AspNetCore.Components NuGet packages and the .NET SDK, updates the package references in the templates and the version of the template package itself, runs the test suite, then commits and tags the release. Finally, if a preview or RC branch (netNN) exists, merges master into it with no fast forward and resolves the conflicts.
---

# Blazor Minimum Templates — New Version Release Procedure

This repository is the source of the NuGet package "Toolbelt.AspNetCore.Blazor.Minimum.Templates", a set of `dotnet new` project templates for Blazor.
Whenever a new patch version of ASP.NET Core is released, a new version of this template package must be created with its referenced package versions updated. This document describes that entire procedure.

> **Scope**: this skill handles **stable releases only**, and the release work is done **on the `master` branch**. Make sure `master` is checked out before starting. The one exception is Step 5, where the finished release is merged into the preview / RC branch if such a branch exists. If the user asks for a release targeting a **preview or RC version of .NET**, use the `release-new-preview-rc-version` skill instead — that work is done on a dedicated `netNN` branch.

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

### If some tests fail: rerun only the failed tests

Test failures in this suite are sometimes flaky (e.g., nondeterministic static-web-assets build races, especially when a preview .NET SDK is installed on the machine). When only a few tests fail, do **not** rerun the whole suite — rerun just the failed tests and see whether they pass this time.

The test project runs on the Microsoft.Testing.Platform (MTP) runner (`EnableNUnitRunner`), which supports the VSTest-style `--filter` option (NOT `--treenode-filter`), passed after `--`. Test names in this suite are parameterized (e.g., `DotNetNewAndBuild_Test(Auto,NoRouting,Layout,"net9.0",Solution)`); to match one exactly with the `Name~` (contains) operator, **escape the parentheses with a backslash** — commas and double quotes need no escaping. Unescaped parentheses are silently parsed as filter-grammar grouping and match ALL tests, so never omit the escapes. Example, rerunning that one failed test (quote the filter with single quotes in PowerShell):

```
dotnet test -- --filter 'Name~DotNetNewAndBuild_Test\(Auto,NoRouting,Layout,"net9.0",Solution\)'
```

- If multiple tests failed, combine the conditions with `|`:
  `--filter 'Name~Test_A\(...\)|Name~Test_B\(...\)'`
- Before the actual rerun, verify the filter matches exactly the intended test case(s) with `--list-tests` (fast, runs nothing): `dotnet test -- --list-tests --filter '...'` — the discovery summary prints the matched count (a filter matching 0 tests exits with code 8). Also confirm from the rerun output that the number of executed tests equals the number of intended test cases.
- **If all the failed tests pass on the rerun**: treat the original failures as flaky, consider the suite green, and proceed to the next step. Mention in the final report that flaky failures occurred and were cleared by a rerun.
- **If any test fails again on the rerun**: do not commit; investigate the failure and report it to the user.

## Step 4: Commit and tag

Once all tests pass:

1. Stage all the changed files and commit. Following the style of the past history, the commit message lists only the version lines that were updated:
   - Example (3 lines): `Updated package references to 8.0.27, 9.0.16, and 10.0.8`
   - Example (1 line): `Updated package references to 10.0.7`
2. Tag that commit in the `v.{new}` format (e.g., `git tag v.10.0.301`).

**Do not run `git push` or publish to NuGet (`dotnet nuget push`).** The user performs those steps manually.

## Step 5: Merge `master` into the preview / RC branch

If a preview / RC branch of a newer .NET major version exists, the stable release you just made must be carried over to it. Otherwise that branch keeps the old package references, and its next preview release ships stale versions.

### 5-1. Check whether such a branch exists

```
git branch --list "net[0-9]*"
```

Branches like `net6`, `net7`, `net8`, `net9` and `net10` are old and already closed. The target of this step is only a branch whose major version is **newer than the stable .NET major version** you released for. For example, right after a .NET 10 stable release, the target is `net11` if it exists. If there is no such branch, skip this whole step.

Make sure the working tree is clean before you start.

### 5-2. Merge with no fast forward

```
git switch netNN
git merge master --no-ff
```

Keep the default merge commit message, `Merge branch 'master' into netNN`. That is the convention used in the past history.

### 5-3. Resolve the conflicts

Four files conflict almost every time. The four template csproj files under `Content/` and `test/Blazor.Minimum.Templates.Test.csproj` merge on their own, because the `netNN.0` ItemGroup sits in a separate block from the stable ones. Do not hand edit them.

| Conflicting file | How to resolve |
|---|---|
| `Version.props` | Keep the **branch side** (the preview / RC version, such as `11.0.100-preview.6`). `git checkout --ours Version.props` |
| `test/VersionInfo.cs` | Keep the **branch side**, same as above. `git checkout --ours test/VersionInfo.cs` |
| `README.md` | Keep the **branch side** for both install command examples. `git checkout --ours README.md` |
| `RELEASE-NOTES.txt` | Keep **both sides**. See below. |

> `--ours` means the branch you are on, which is `netNN`, and `--theirs` means `master`. This is the normal direction for `git merge`. Do not mix it up with the reversed meaning during a rebase.

**How to resolve `RELEASE-NOTES.txt`**

The conflict block looks like the following. The branch side holds all the preview entries, and the master side holds only the new stable entry.

```
<<<<<<< HEAD
v.11.0.100-preview.6
- Update: Updated the referenced packages to 11.0.0-preview.6

  ... (older preview entries) ...

v.11.0.100-preview.1
- Update: Add support for .NET 11.0 preview.
=======
v.10.0.400
- Update: Updated the referenced packages to 8.0.30, 9.0.19, and 10.0.11.
>>>>>>> master

v.10.0.302
  ... (common history) ...
```

Keep both sides in this order. Delete the `<<<<<<< HEAD` line, replace the `=======` line with a single blank line, and delete the `>>>>>>> master` line. Nothing else changes. The preview entries stay on top, the new stable entry comes right under them, and the common history follows. Never delete either side, and never reorder the entries by version number. The order here is the order of release time, not of version number, so a `v.11.0.100-preview.6` entry sitting above a `v.10.0.400` entry is correct.

### 5-4. Check the result before you commit the merge

1. Look for leftover conflict markers.

   ```
   git diff --check
   ```

   Do not grep the whole repository for `<<<<<<<`, because the skill files under `.claude/skills/` contain that string as sample text and will give a false hit.

2. Confirm that the merge did not overwrite the files that make the preview branch what it is. These must still hold the `netNN` values, not the values from `master`.

   - `Toolbelt.AspNetCore.Blazor.Minimum.Templates.csproj`, the `<TargetFramework>` element (`net11.0`, not `net10.0`)
   - `Version.props` and `test/VersionInfo.cs`, the preview / RC version
   - The `netNN.0` ItemGroups in the four template csproj files, with their wildcard versions such as `11.0.0-preview.6.*`
   - `Content/*/.template.config/template.json`, the framework choice list, which must still contain `netNN.0`
   - `test/Internals/TargetFramework.cs`, which must still contain the `NetNN` constant
   - `test/global.json`, if the branch has one

   A quick way to see this is `git diff master -- <path>`. The differences that remain against `master` are exactly the preview support of the branch.

3. Review the whole merge result with `git diff HEAD` (before the merge commit) or `git diff <branch tip before merge>`. The change coming from `master` should be limited to the stable package reference versions, the new `RELEASE-NOTES.txt` entry, and the test project packages. If anything else changed, you resolved something wrong.

4. Verify that the package still builds and keeps the preview version.

   ```
   dotnet pack Toolbelt.AspNetCore.Blazor.Minimum.Templates.csproj -o ./_out_mergecheck
   ```

   The output file name must be `Toolbelt.AspNetCore.Blazor.Minimum.Templates.{preview version}.nupkg`. If the file name carries the stable version instead, `Version.props` was resolved the wrong way. Delete the `_out_mergecheck` folder after the check, and do not commit it. This build needs the preview / RC SDK installed on the machine. If `dotnet --list-sdks` does not list it, skip this check and say so in the final report.

Then complete the merge commit.

### 5-5. What this step does not do

- Do **not** run the test suite on the preview branch here, and do **not** create a new version or a tag there. The merge only carries the stable update over. The next preview release is made later by the `release-new-preview-rc-version` skill, which starts from this merged state.
- Do **not** run `git push`.
- Switch back to `master` when the merge is done.

If the conflicts do not look like the ones described above, or if the checks in 5-4 fail, stop and report it to the user instead of guessing.

## Step 6: Final report

Report the following to the user:

- The updated package versions (per version line) and the new template package version
- The test project packages updated by `dotnet package update`
- The test results (number of tests)
- The commit and tag that were created
- Whether a preview / RC branch was merged, the merge commit, and how each conflict was resolved. If no such branch exists, say that nothing was merged.
- A reminder that pushing and publishing to NuGet are the user's manual steps. This includes the merge commit on the preview / RC branch.
