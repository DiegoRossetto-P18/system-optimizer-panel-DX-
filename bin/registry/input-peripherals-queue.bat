@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

set "PROFILE=%~1"
if "%PROFILE%"=="" set "PROFILE=equilibrado"

if /i "%PROFILE%"=="equilibrado" (
    :: Fila Equilibrada (Recomendado): Mouse 26, Teclado 26
    Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\mouclass\Parameters" /v "MouseDataQueueSize" /t REG_DWORD /d "26" /f >nul 2>&1
    Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "KeyboardDataQueueSize" /t REG_DWORD /d "26" /f >nul 2>&1
) else if /i "%PROFILE%"=="extremo" (
    :: Fila Extrema (Competitivo/eSports): Mouse 16, Teclado 16
    Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\mouclass\Parameters" /v "MouseDataQueueSize" /t REG_DWORD /d "16" /f >nul 2>&1
    Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "KeyboardDataQueueSize" /t REG_DWORD /d "16" /f >nul 2>&1
) else if /i "%PROFILE%"=="padrao" (
    :: Valor Nativo do Windows: Mouse 100, Teclado 100
    Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\mouclass\Parameters" /v "MouseDataQueueSize" /t REG_DWORD /d "100" /f >nul 2>&1
    Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "KeyboardDataQueueSize" /t REG_DWORD /d "100" /f >nul 2>&1
)

:: Parametros fixos de estabilidade
Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "ConnectMultiplePorts" /t REG_DWORD /d "0" /f >nul 2>&1
Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "KeyboardDeviceBaseName" /t REG_SZ /d "KeyboardClass" /f >nul 2>&1
Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "MaximumPortsServiced" /t REG_DWORD /d "3" /f >nul 2>&1
Reg.exe add "HKLM\SYSTEM\CurrentControlSet\Services\kbdclass\Parameters" /v "SendOutputToAllPorts" /t REG_DWORD /d "1" /f >nul 2>&1