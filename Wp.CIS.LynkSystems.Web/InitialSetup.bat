@echo off

IF ERRORLEVEL 1 GOTO NOT-THERE
echo cleaning
dotnet clean
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo setting dev variables
setx ASPNETCORE_ENVIRONMENT "Development"
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Cleaning NPM Cache
call npm cache clean -verbose
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Doing NPM Install
call npm install -verbose
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Doing webpack merge
call npm i -D webpack-merge
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Doing DotNet Restore
call dotnet restore
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Doing DotNet Build
call dotnet build
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Running webpack config
call webpack --config webpack.config.vendor.js 
cls

IF ERRORLEVEL 1 GOTO NOT-THERE
echo Running webpack
call webpack  
cls

echo Process done.. 
goto :choice

:choice
set /P c=Do you want to run web app from dotnet cli(Y/N) ?  
if /I "%c%" EQU "Y" goto :rundotnet
if /I "%c%" EQU "N" goto :do_exit
goto :choice

:rundotnet
call dotnet run
pause
cls

:do_exit
echo exiting...
EXIT

EXIT
:NOT-THERE
ECHO Got Error, re-run the process
PAUSE
EXIT