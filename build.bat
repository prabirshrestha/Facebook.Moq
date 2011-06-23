@ECHO off
PUSHD "%~dp0"

SET SolutionFile="%~dp0src/Facebook.Moq.sln"

SET MSBuild=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
IF NOT EXIST "%MSBuild%" (
	ECHO Installation of .NET Framework 4.0 is required to build this project
	ECHO http://www.microsoft.com/downloads/details.aspx?FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7
	START /d "~\iexplore.exe" http://www.microsoft.com/downloads/details.aspx?FamilyID=0a391abd-25c1-4fc0-919f-b21f31ab88b7
	EXIT /b 1
	GOTO END
)

"%MSBuild%" "%SolutionFile%" /target:rebuild /property:TargetFrameworkVersion=v4.0;Configuration=Release;RunTests=True

:END
POPD

pause