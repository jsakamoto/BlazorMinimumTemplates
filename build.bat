@echo off
cd /d %~dp0
.\.bin\nuget.exe pack Toolbelt.AspNetCore.Blazor.Minimum.Template.nuspec -OutputDirectory dist