@echo off
echo Lancement de la compilation de l'extension Simphony...
echo.

REM Vérifier si PowerShell est disponible
where powershell >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ❌ PowerShell non trouve !
    echo Veuillez installer PowerShell ou utiliser Windows 10+
    pause
    exit /b 1
)

REM Lancer le script PowerShell de compilation
powershell -ExecutionPolicy Bypass -File "Compile-Extension.ps1"

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo ❌ Erreur lors de l'execution du script PowerShell
    pause
) 