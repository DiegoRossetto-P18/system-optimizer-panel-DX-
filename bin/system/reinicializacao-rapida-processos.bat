@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

:: 1. Esvaziar Area de Transferencia e Caches de Memoria Leve
echo off | clip
ipconfig /flushdns >nul 2>&1

:: 2. Encerrar Processos Ociosos e Runtime em Segundo Plano
taskkill /f /im RuntimeBroker.exe /fi "status eq NOT RESPONDING" >nul 2>&1
taskkill /f /im GameBarPresenceWriter.exe >nul 2>&1
taskkill /f /im SearchProtocolHost.exe >nul 2>&1
taskkill /f /im SearchFilterHost.exe >nul 2>&1
taskkill /f /im MicrosoftEdgeUpdate.exe >nul 2>&1

:: 3. Reiniciar o Windows Explorer para liberar Threads acumuladas
taskkill /f /im explorer.exe >nul 2>&1
timeout /t 2 /nobreak >nul
start explorer.exe

:: 4. Forçar Coleta de Lixo da Memoria de Trabalho do Sistema (Working Set Trim)
powershell -Command "[System.GC]::Collect(); [System.GC]::WaitForPendingFinalizers()" >nul 2>&1

exit /b 0