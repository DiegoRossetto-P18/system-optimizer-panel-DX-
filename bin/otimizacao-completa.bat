@echo off
chcp 65001 >nul
title Otimizacao Completa do Sistema - Log Detalhado

set "LOG_FILE=%~dp0otimizacao_log.txt"

echo ======================================================= > "%LOG_FILE%"
echo [%DATE% %TIME%] INÍCIO DA OTIMIZAÇÃO COMPLETA >> "%LOG_FILE%"
echo ======================================================= >> "%LOG_FILE%"
echo. >> "%LOG_FILE%"

echo [%TIME%] [1/5] Executando Limpeza de Temporários... >> "%LOG_FILE%"
call "%~dp0cleanup\limpeza-profunda-pastas-temp.bat" >> "%LOG_FILE%" 2>&1
echo [%TIME%] [1/5] status: FINALIZADO >> "%LOG_FILE%"
echo ------------------------------------------------------- >> "%LOG_FILE%"

echo [%TIME%] [2/5] Otimizando Rede e DNS... >> "%LOG_FILE%"
call "%~dp0network\otimizar-rede-e-dns.bat" >> "%LOG_FILE%" 2>&1
echo [%TIME%] [2/5] status: FINALIZADO >> "%LOG_FILE%"
echo ------------------------------------------------------- >> "%LOG_FILE%"

echo [%TIME%] [3/5] Aplicando Ajustes de Registro... >> "%LOG_FILE%"
call "%~dp0registry\system-responsiveness-gaming.bat" >> "%LOG_FILE%" 2>&1
echo [%TIME%] [3/5] status: FINALIZADO >> "%LOG_FILE%"
echo ------------------------------------------------------- >> "%LOG_FILE%"

echo [%TIME%] [4/5] Otimizando Armazenamento e Memória... >> "%LOG_FILE%"
call "%~dp0storage\configurar-uso-memoria-ntfs.bat" >> "%LOG_FILE%" 2>&1
echo [%TIME%] [4/5] status: FINALIZADO >> "%LOG_FILE%"
echo ------------------------------------------------------- >> "%LOG_FILE%"

echo [%TIME%] [5/5] Reiniciando Processos e Interface... >> "%LOG_FILE%"
call "%~dp0system\reinicializacao-rapida-processos.bat" >> "%LOG_FILE%" 2>&1
echo [%TIME%] [5/5] status: FINALIZADO >> "%LOG_FILE%"
echo ------------------------------------------------------- >> "%LOG_FILE%"

echo. >> "%LOG_FILE%"
echo ======================================================= >> "%LOG_FILE%"
echo [%DATE% %TIME%] OTIMIZAÇÃO CONCLUÍDA COM SUCESSO! >> "%LOG_FILE%"
echo ======================================================= >> "%LOG_FILE%"

exit /b 0