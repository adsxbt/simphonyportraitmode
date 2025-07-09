@echo off
echo Test de compilation rapide...
echo.

REM Vérifier si PowerShell est disponible
where powershell >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ❌ PowerShell non trouve !
    pause
    exit /b 1
)

REM Lancer le script PowerShell de test
powershell -ExecutionPolicy Bypass -File "Test-Compilation.ps1"

pause 