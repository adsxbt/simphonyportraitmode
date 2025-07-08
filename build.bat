@echo off
echo ========================================
echo  Extension SimphonyPortraitMode - Build
echo ========================================
echo.

REM Vérifier si MSBuild est disponible
where msbuild >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERREUR: MSBuild n'est pas trouvé dans le PATH
    echo Assurez-vous que Visual Studio ou Build Tools sont installés
    pause
    exit /b 1
)

echo Nettoyage des fichiers précédents...
if exist "bin\Release" rmdir /s /q "bin\Release"
if exist "bin\Debug" rmdir /s /q "bin\Debug"
if exist "obj" rmdir /s /q "obj"

echo.
echo Compilation en mode Release...
msbuild SimphonyPortraitMode.sln /p:Configuration=Release /p:Platform="Any CPU" /verbosity:minimal

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo  COMPILATION RÉUSSIE !
    echo ========================================
    echo.
    echo Fichiers générés dans bin\Release\:
    dir /b bin\Release\*.dll bin\Release\*.pdb 2>nul
    echo.
    echo Instructions de déploiement :
    echo 1. Copier SimphonyPortraitMode.dll vers le dossier d'extensions Simphony
    echo 2. Configurer l'extension dans EMC
    echo 3. Redémarrer Simphony
    echo.
) else (
    echo.
    echo ========================================
    echo  ERREUR DE COMPILATION !
    echo ========================================
    echo Vérifiez les erreurs ci-dessus et corrigez-les
    echo.
)

pause 