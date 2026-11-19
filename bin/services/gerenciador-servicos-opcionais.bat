@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

set "ACTION=%~1"
if "%ACTION%"=="" set "ACTION=desativar"

if /i "%ACTION%"=="desativar" (
    :: Spooler de Impressao (Para quem nao usa impressora)
    sc config Spooler start= disabled >nul 2>&1
    net stop Spooler >nul 2>&1

    :: Servico de Fax
    sc config Fax start= disabled >nul 2>&1
    net stop Fax >nul 2>&1

    :: Cartao Inteligente (Smart Card)
    sc config SCardSvr start= disabled >nul 2>&1
    net stop SCardSvr >nul 2>&1
) else if /i "%ACTION%"=="restaurar" (
    :: Restaura os servicos para o modo Automatico/Manual do Windows
    sc config Spooler start= auto >nul 2>&1
    net start Spooler >nul 2>&1

    sc config Fax start= demand >nul 2>&1
    sc config SCardSvr start= demand >nul 2>&1
)

exit /b 0