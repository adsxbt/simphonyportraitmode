@echo off
title Compilation Extension SimphonyPortraitMode

echo ╔════════════════════════════════════════════════════════════╗
echo ║              Compilation Extension Simphony                ║
echo ║                 SimphonyPortraitMode.dll                   ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

echo [1/5] Vérification des prérequis...

REM Vérifier MSBuild
where msbuild >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ❌ MSBuild non trouvé !
    echo    Utilisez "Developer Command Prompt for VS"
    pause & exit /b 1
)
echo ✅ MSBuild trouvé

REM Vérifier les DLLs Simphony essentielles
echo.
echo [2/5] Vérification des DLLs Simphony...

set "ALL_DLLS_OK=1"

if exist "lib\PosCommonClasses.dll" (
    echo ✅ PosCommonClasses.dll
) else (
    echo ❌ PosCommonClasses.dll MANQUANT !
    echo    Exécutez d'abord: find-simphony-dlls.bat
    set "ALL_DLLS_OK=0"
)

if exist "lib\PosCore.dll" (
    echo ✅ PosCore.dll
) else (
    echo ❌ PosCore.dll manquant !
    set "ALL_DLLS_OK=0"
)

if exist "lib\Ops.dll" (
    echo ✅ Ops.dll
) else (
    echo ❌ Ops.dll manquant !
    set "ALL_DLLS_OK=0"
)

if %ALL_DLLS_OK%==0 (
    echo.
    echo ❌ Des DLLs essentielles manquent !
    echo    Exécutez find-simphony-dlls.bat d'abord
    pause & exit /b 1
)

echo.
echo [3/5] Nettoyage des anciens fichiers...
if exist "bin\Debug" rmdir /s /q "bin\Debug" 2>nul
if exist "bin\Release" rmdir /s /q "bin\Release" 2>nul
if exist "obj" rmdir /s /q "obj" 2>nul
echo ✅ Nettoyage terminé

echo.
echo [4/5] Compilation en cours...
echo Configuration: Release
echo Plateforme: Any CPU
echo.

msbuild SimphonyPortraitMode.sln /p:Configuration=Release /p:Platform="Any CPU" /verbosity:minimal /nologo

echo.
echo [5/5] Vérification du résultat...

if %ERRORLEVEL% EQU 0 (
    if exist "bin\Release\SimphonyPortraitMode.dll" (
        echo.
        echo ╔════════════════════════════════════════════════════════════╗
        echo ║                  ✅ COMPILATION RÉUSSIE !                  ║
        echo ╚════════════════════════════════════════════════════════════╝
        echo.
        
        echo 📁 Fichiers générés :
        for %%F in ("bin\Release\*.dll" "bin\Release\*.pdb") do (
            if exist "%%F" (
                echo    📄 %%~nxF
                for %%A in ("%%F") do echo       Taille: %%~zA octets
            )
        )
        
        echo.
        echo 🚀 Prochaines étapes :
        echo    1. Copier SimphonyPortraitMode.dll vers Simphony
        echo    2. Configurer l'extension dans EMC
        echo    3. Redémarrer Simphony
        echo.
        echo 📂 Chemin complet :
        echo    %CD%\bin\Release\SimphonyPortraitMode.dll
        echo.
        
        set /p choice="Ouvrir le dossier de sortie ? (O/N): "
        if /I "%choice%"=="O" explorer "bin\Release"
        
    ) else (
        echo ❌ Le fichier DLL n'a pas été généré !
    )
) else (
    echo.
    echo ╔════════════════════════════════════════════════════════════╗
    echo ║                  ❌ ÉCHEC DE COMPILATION                   ║
    echo ╚════════════════════════════════════════════════════════════╝
    echo.
    echo 🔍 Vérifiez les erreurs ci-dessus
    echo 💡 Solutions possibles :
    echo    • Erreurs de syntaxe C#
    echo    • Références manquantes
    echo    • Version .NET Framework
)

echo.
pause 