@echo off

IF ERRORLEVEL 1 GOTO NOT-THERE
echo cleaning
call npm config set strict-ssl false
call npm config set registry http://registry.npmjs.org/
call npm config set proxy http://mf-proxy1:8080
call npm config set https-proxy https://mf-proxy1:8080 
call npm config get
cls

EXIT
:NOT-THERE
ECHO Got Error, re-run the process
PAUSE
EXIT