@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

IF "%1"=="1" (
    sc config GraphicsPerfSvc start= Disabled >nul 2>&1
    net stop GraphicsPerfSvc >nul 2>&1
    exit /b
)

IF "%1"=="2" (
    sc config GraphicsPerfSvc start= demand >nul 2>&1
    net start GraphicsPerfSvc >nul 2>&1
    exit /b
)