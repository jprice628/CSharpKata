@echo off

REM This could be simplified to a single dotnet run call, but it is slower.

set file=".\bin\Debug\net8.0\BinarySearch.exe"

if not exist %file% (
	echo Building executable...
	dotnet build
)

%file% %*