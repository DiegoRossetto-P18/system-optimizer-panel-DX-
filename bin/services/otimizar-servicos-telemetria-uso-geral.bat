@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

:: Telemetria e Experiencia do Usuario Conectado
sc config DiagTrack start= disabled >nul 2>&1
net stop DiagTrack >nul 2>&1

:: Servico de Rastreamento de Diagnostico
sc config diagnosticshub.standardcollector.service start= disabled >nul 2>&1
net stop diagnosticshub.standardcollector.service >nul 2>&1

:: Relatorio de Erros do Windows (WER)
sc config WerSvc start= disabled >nul 2>&1
net stop WerSvc >nul 2>&1

:: Servico de Rastreamento de Links Alterados (Distributed Link Tracking)
sc config TrkWks start= disabled >nul 2>&1
net stop TrkWks >nul 2>&1

:: Servico Geolocalizacao (Se nao usar mapas/clima local)
sc config lfsvc start= disabled >nul 2>&1
net stop lfsvc >nul 2>&1

exit /b 0