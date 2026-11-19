@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion
set "params=%*"
cd /d "%~dp0" && ( if exist "%temp%\getadmin.vbs" del "%temp%\getadmin.vbs" ) && fsutil dirty query %systemdrive% 1>nul 2>nul || (  echo Set UAC = CreateObject^("Shell.Application"^) : UAC.ShellExecute "cmd.exe", "/k cd ""%~sdp0"" && %~s0 %params%", "", "runas", 1 >> "%temp%\getadmin.vbs" && "%temp%\getadmin.vbs" && exit /B )

del /s /f /q c:\windows\temp\*.* >nul 2>&1
rd /s /q c:\windows\temp >nul 2>&1
md c:\windows\temp >nul 2>&1
del /s /f /q C:\WINDOWS\Prefetch\*.* >nul 2>&1
del /s /f /q %temp%\*.* >nul 2>&1
rd /s /q %temp% >nul 2>&1
md %temp% >nul 2>&1

for /F "tokens=*" %%G in ('wevtutil.exe el') do (
    wevtutil.exe cl "%%G" >nul 2>&1
)

exit /b
