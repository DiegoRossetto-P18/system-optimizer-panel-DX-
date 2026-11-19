@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

del /s /f /q %windir%\temp\*.* >nul 2>&1
rd /s /q %windir%\temp >nul 2>&1
md %windir%\temp >nul 2>&1

del /s /f /q %windir%\Prefetch\*.* >nul 2>&1
rd /s /q %windir%\Prefetch >nul 2>&1
md %windir%\Prefetch >nul 2>&1

del /s /f /q %windir%\system32\dllcache\*.* >nul 2>&1
rd /s /q %windir%\system32\dllcache >nul 2>&1
md %windir%\system32\dllcache >nul 2>&1

del /s /f /q "%SystemDrive%\Temp"\*.* >nul 2>&1
rd /s /q "%SystemDrive%\Temp" >nul 2>&1
md "%SystemDrive%\Temp" >nul 2>&1

del /s /f /q %temp%\*.* >nul 2>&1
rd /s /q %temp% >nul 2>&1
md %temp% >nul 2>&1

del /s /f /q "%USERPROFILE%\Local Settings\History"\*.* >nul 2>&1
rd /s /q "%USERPROFILE%\Local Settings\History" >nul 2>&1
md "%USERPROFILE%\Local Settings\History" >nul 2>&1

del /s /f /q "%USERPROFILE%\Local Settings\Temporary Internet Files"\*.* >nul 2>&1
rd /s /q "%USERPROFILE%\Local Settings\Temporary Internet Files" >nul 2>&1
md "%USERPROFILE%\Local Settings\Temporary Internet Files" >nul 2>&1

del /s /f /q "%USERPROFILE%\Local Settings\Temp"\*.* >nul 2>&1
rd /s /q "%USERPROFILE%\Local Settings\Temp" >nul 2>&1
md "%USERPROFILE%\Local Settings\Temp" >nul 2>&1

del /s /f /q "%USERPROFILE%\Recent"\*.* >nul 2>&1
rd /s /q "%USERPROFILE%\Recent" >nul 2>&1
md "%USERPROFILE%\Recent" >nul 2>&1

del /s /f /q "%USERPROFILE%\Cookies"\*.* >nul 2>&1
rd /s /q "%USERPROFILE%\Cookies" >nul 2>&1
md "%USERPROFILE%\Cookies" >nul 2>&1