# Compile-Extension.ps1
# Script PowerShell pour compiler l'extension SimphonyPortraitMode

[CmdletBinding()]
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release",
    
    [ValidateSet("Any CPU", "x86", "x64")]
    [string]$Platform = "Any CPU",
    
    [switch]$Clean,
    [switch]$Verbose
)

# Configuration
$Host.UI.RawUI.WindowTitle = "Compilation Extension SimphonyPortraitMode"

function Write-Header {
    param($Text)
    Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
    Write-Host "║$($Text.PadLeft(($Text.Length + 60) / 2).PadRight(60))║" -ForegroundColor Cyan
    Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
    Write-Host ""
}

function Write-Success {
    param($Message)
    Write-Host "✅ $Message" -ForegroundColor Green
}

function Write-Error {
    param($Message)
    Write-Host "❌ $Message" -ForegroundColor Red
}

function Write-Warning {
    param($Message)
    Write-Host "⚠️ $Message" -ForegroundColor Yellow
}

function Write-Info {
    param($Message)
    Write-Host "🔍 $Message" -ForegroundColor Blue
}

function Write-Step {
    param($StepNumber, $Total, $Description)
    Write-Host "[$StepNumber/$Total] $Description..." -ForegroundColor Cyan
}

# En-tête
Write-Header "Compilation Extension Simphony"
Write-Host "Configuration: $Configuration | Plateforme: $Platform" -ForegroundColor Gray
Write-Host ""

# Étape 1 : Vérification des prérequis
Write-Step 1 6 "Vérification des prérequis"

# Vérifier MSBuild
try {
    $MSBuildPath = Get-Command msbuild -ErrorAction Stop
    Write-Success "MSBuild trouvé: $($MSBuildPath.Source)"
} catch {
    Write-Error "MSBuild non trouvé !"
    Write-Warning "Solutions possibles :"
    Write-Host "  • Utilisez 'Developer PowerShell for VS'" -ForegroundColor White
    Write-Host "  • Ou 'Developer Command Prompt for VS'" -ForegroundColor White
    Write-Host "  • Ou installez Build Tools for Visual Studio" -ForegroundColor White
    exit 1
}

# Vérifier le fichier solution
if (-not (Test-Path "SimphonyPortraitMode.sln")) {
    Write-Error "Fichier SimphonyPortraitMode.sln non trouvé !"
    Write-Warning "Assurez-vous d'être dans le bon dossier de projet"
    exit 1
}
Write-Success "Fichier solution trouvé"
Write-Host ""

# Étape 2 : Vérification des DLLs Simphony
Write-Step 2 6 "Vérification des DLLs Simphony"

$RequiredDlls = @{
    "PosCommonClasses.dll" = "lib\PosCommonClasses.dll"
    "PosCore.dll" = "lib\PosCore.dll"
    "Ops.dll" = "lib\Ops.dll"
}

$AllDllsPresent = $true
foreach ($Dll in $RequiredDlls.GetEnumerator()) {
    if (Test-Path $Dll.Value) {
        $FileInfo = Get-Item $Dll.Value
        $SizeMB = [math]::Round($FileInfo.Length / 1MB, 1)
        Write-Success "$($Dll.Key) (${SizeMB} MB)"
    } else {
        Write-Error "$($Dll.Key) MANQUANT !"
        $AllDllsPresent = $false
    }
}

if (-not $AllDllsPresent) {
    Write-Host ""
    Write-Error "Des DLLs essentielles manquent !"
    Write-Warning "Exécutez d'abord: .\Find-SimphonyDlls.ps1"
    exit 1
}
Write-Host ""

# Étape 3 : Nettoyage (si demandé)
if ($Clean) {
    Write-Step 3 6 "Nettoyage des fichiers précédents"
    
    $CleanDirs = @("bin", "obj")
    foreach ($Dir in $CleanDirs) {
        if (Test-Path $Dir) {
            Remove-Item $Dir -Recurse -Force
            Write-Success "Dossier $Dir nettoyé"
        }
    }
} else {
    Write-Step 3 6 "Nettoyage rapide"
    
    $ConfigDirs = @("bin\$Configuration", "obj")
    foreach ($Dir in $ConfigDirs) {
        if (Test-Path $Dir) {
            Remove-Item $Dir -Recurse -Force -ErrorAction SilentlyContinue
        }
    }
    Write-Success "Nettoyage terminé"
}
Write-Host ""

# Étape 4 : Préparation des paramètres MSBuild
Write-Step 4 6 "Préparation de la compilation"

$MSBuildArgs = @(
    "SimphonyPortraitMode.sln"
    "/p:Configuration=$Configuration"
    "/p:Platform=`"$Platform`""
    "/nologo"
)

if ($Verbose) {
    $MSBuildArgs += "/verbosity:normal"
} else {
    $MSBuildArgs += "/verbosity:minimal"
}

Write-Info "Paramètres MSBuild: $($MSBuildArgs -join ' ')"
Write-Host ""

# Étape 5 : Compilation
Write-Step 5 6 "Compilation en cours"

$StartTime = Get-Date
try {
    $MSBuildProcess = Start-Process -FilePath "msbuild" -ArgumentList $MSBuildArgs -Wait -PassThru -NoNewWindow
    $Duration = (Get-Date) - $StartTime
    
    if ($MSBuildProcess.ExitCode -eq 0) {
        Write-Success "Compilation terminée en $([math]::Round($Duration.TotalSeconds, 1)) secondes"
    } else {
        Write-Error "Échec de compilation (Code: $($MSBuildProcess.ExitCode))"
        exit $MSBuildProcess.ExitCode
    }
} catch {
    Write-Error "Erreur lors de la compilation: $($_.Exception.Message)"
    exit 1
}
Write-Host ""

# Étape 6 : Vérification et résultats
Write-Step 6 6 "Vérification du résultat"

$OutputPath = "bin\$Configuration"
$MainDll = "$OutputPath\SimphonyPortraitMode.dll"

if (Test-Path $MainDll) {
    Write-Success "DLL principal généré avec succès !"
    
    # Informations sur les fichiers générés
    $OutputFiles = Get-ChildItem $OutputPath -File | Sort-Object Name
    Write-Host ""
    Write-Info "Fichiers générés dans $OutputPath :"
    
    foreach ($File in $OutputFiles) {
        $SizeKB = [math]::Round($File.Length / 1KB, 1)
        $Icon = switch ($File.Extension.ToLower()) {
            ".dll" { "📚" }
            ".pdb" { "🔍" }
            ".xml" { "📄" }
            default { "📎" }
        }
        Write-Host "   $Icon $($File.Name) (${SizeKB} KB)" -ForegroundColor White
    }
    
    Write-Host ""
    Write-Header "✅ COMPILATION RÉUSSIE !"
    
    Write-Host "🚀 Prochaines étapes :" -ForegroundColor Yellow
    Write-Host "   1. Copier SimphonyPortraitMode.dll vers Simphony" -ForegroundColor White
    Write-Host "      Target: C:\Micros\Simphony\Extensions\SimphonyPortraitMode\" -ForegroundColor Gray
    Write-Host "   2. Configurer l'extension dans EMC" -ForegroundColor White
    Write-Host "   3. Redémarrer Simphony" -ForegroundColor White
    Write-Host ""
    Write-Host "📂 Chemin complet : $(Resolve-Path $MainDll)" -ForegroundColor Gray
    
    # Proposer d'ouvrir le dossier
    Write-Host ""
    $OpenFolder = Read-Host "Ouvrir le dossier de sortie ? (O/N)"
    if ($OpenFolder -eq "O" -or $OpenFolder -eq "o") {
        Invoke-Item $OutputPath
    }
    
} else {
    Write-Error "Le fichier DLL principal n'a pas été généré !"
    Write-Warning "Vérifiez les erreurs de compilation ci-dessus"
    exit 1
}

Write-Host ""
Write-Host "Script terminé. Appuyez sur une touche pour continuer..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") 