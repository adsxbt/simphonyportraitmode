@echo off
echo ╔════════════════════════════════════════════════════════════╗
echo ║            Recherche des DLLs Simphony manquantes          ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

echo [1/3] Recherche de PosCommonClasses.dll...
echo.

REM Créer le dossier lib s'il n'existe pas
if not exist "lib" mkdir lib

REM Chemins potentiels où Simphony peut être installé
set "PATHS=C:\Micros\Simphony\Bin C:\Micros\Simphony\Platform C:\Program Files\Micros\Simphony\Bin C:\Program Files (x86)\Micros\Simphony\Bin C:\Program Files\Oracle\Micros\Simphony\Bin C:\Program Files (x86)\Oracle\Micros\Simphony\Bin"

set "FOUND_POSCOMMON=0"

for %%P in (%PATHS%) do (
    if exist "%%P\PosCommonClasses.dll" (
        echo ✅ TROUVÉ: %%P\PosCommonClasses.dll
        echo    Copie vers lib\...
        copy "%%P\PosCommonClasses.dll" "lib\" >nul 2>&1
        if %ERRORLEVEL%==0 (
            echo    ✅ Copie réussie !
            set "FOUND_POSCOMMON=1"
        ) else (
            echo    ❌ Erreur lors de la copie
        )
        echo.
    )
)

REM Recherche récursive si pas trouvé dans les emplacements standards
if %FOUND_POSCOMMON%==0 (
    echo [2/3] Recherche récursive sur le disque C:...
    echo (Cela peut prendre quelques minutes...)
    
    for /f "delims=" %%i in ('dir /s /b "C:\PosCommonClasses.dll" 2^>nul') do (
        echo ✅ TROUVÉ: %%i
        echo    Copie vers lib\...
        copy "%%i" "lib\" >nul 2>&1
        if %ERRORLEVEL%==0 (
            echo    ✅ Copie réussie !
            set "FOUND_POSCOMMON=1"
            goto :found
        )
    )
)

:found
echo [3/3] Vérification finale...
echo.

REM Vérifier toutes les DLLs nécessaires
echo 🔍 État des DLLs requises :
if exist "lib\PosCommonClasses.dll" (
    echo    ✅ PosCommonClasses.dll
) else (
    echo    ❌ PosCommonClasses.dll MANQUANT !
)

if exist "lib\PosCore.dll" (
    echo    ✅ PosCore.dll
) else (
    echo    ❌ PosCore.dll
)

if exist "lib\Ops.dll" (
    echo    ✅ Ops.dll
) else (
    echo    ❌ Ops.dll
)

echo.
echo ╔════════════════════════════════════════════════════════════╗
if %FOUND_POSCOMMON%==1 (
    echo ║                    ✅ SUCCÈS !                            ║
    echo ║          Toutes les DLLs sont maintenant prêtes           ║
    echo ║            Vous pouvez compiler votre projet              ║
) else (
    echo ║                    ❌ ÉCHEC                               ║
    echo ║         PosCommonClasses.dll n'a pas été trouvé           ║
    echo ║          Vérifiez que Simphony est installé               ║
)
echo ╚════════════════════════════════════════════════════════════╝
echo.

if %FOUND_POSCOMMON%==0 (
    echo 💡 Solutions possibles :
    echo 1. Vérifiez que Simphony est installé
    echo 2. Cherchez manuellement PosCommonClasses.dll
    echo 3. Contactez votre administrateur Simphony
    echo 4. Copiez depuis un autre poste Simphony
)

pause 