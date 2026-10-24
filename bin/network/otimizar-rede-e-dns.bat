@echo off
setlocal EnableExtensions
setlocal EnableDelayedExpansion

REG ADD HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\ /v TcpAckFrequency /t REG_DWORD /d 0 /f >nul 2>&1
REG ADD HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\ /v TCPNoDelay /t REG_DWORD /d 0 /f >nul 2>&1

netsh int tcp set global rsc=disabled >nul 2>&1
netsh int tcp set global rss=disabled >nul 2>&1
netsh int tcp set global ecncapability=enabled >nul 2>&1
netsh int tcp set global autotuninglevel=disabled >nul 2>&1

netsh advfirewall firewall add rule name="StopThrottling" dir=in action=block remoteip=173.194.55.0/24,206.111.0.0/16 enable=yes >nul 2>&1

netsh int tcp set heuristics disabled >nul 2>&1
netsh int tcp set global autotuninglevel=normal >nul 2>&1
netsh int tcp set global fastopen=enable >nul 2>&1
netsh int tcp set supplemental internet congestionprovider=ctcp >nul 2>&1

netsh interface ipv4 set dns name="Wi-Fi" static 1.1.1.1 >nul 2>&1
netsh interface ipv4 add dns name="Wi-Fi" 1.0.0.1 index=2 >nul 2>&1