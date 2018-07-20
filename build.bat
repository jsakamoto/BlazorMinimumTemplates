@echo off
cd /d %~dp0
.\.bin\nuget.exe pack Toolbelt.AspNetCore.Blazor.Minimum.Templates.nuspec -OutputDirectory dist