@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

IF "%1"=="1" (
    Reg Add HKCU\Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications /v GlobalUserDisabled /t REG_DWORD /d 1 /f >nul 2>&1
    exit /b
)

IF "%1"=="2" (
    Reg Add HKCU\Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications /v GlobalUserDisabled /t REG_DWORD /d 0 /f >nul 2>&1
    exit /b
)







