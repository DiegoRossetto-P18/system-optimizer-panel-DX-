@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion
set "params=%*"
cd /d "%~dp0" && ( if exist "%temp%\getadmin.vbs" del "%temp%\getadmin.vbs" ) && fsutil dirty query %systemdrive% 1>nul 2>nul || ( echo Set UAC = CreateObject^("Shell.Application"^) : UAC.ShellExecute "cmd.exe", "/k cd ""%~sdp0"" && %~s0 %params%", "", "runas", 1 >> "%temp%\getadmin.vbs" && "%temp%\getadmin.vbs" && exit /B )

powercfg /list | find "(performance)" > NUL
if %ERRORLEVEL% == 0 (
    goto SetActive
) else (
    goto DuplicateScheme
)

:DuplicateScheme
powercfg.exe /setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c >nul 2>&1
goto SetActive

:SetActive
for /f "tokens=3,4" %%i in ('powercfg /list') do (
    if "%%j" == "(excellent performance)" powercfg /SETACTIVE %%i >nul 2>&1
)

:EOF
exit /b