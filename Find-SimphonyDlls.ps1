# Find-SimphonyDlls.ps1
# Script PowerShell pour localiser et copier les DLLs Simphony manquantes

[CmdletBinding()]
param()

# Configuration des couleurs et du style
$Host.UI.RawUI.WindowTitle = "Recherche DLLs Simphony"

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

function Write-Info {
    param($Message)
    Write-Host "🔍 $Message" -ForegroundColor Yellow
}

function Write-Step {
    param($StepNumber, $Total, $Description)
    Write-Host "[$StepNumber/$Total] $Description..." -ForegroundColor Cyan
}

# En-tête
Write-Header "Recherche des DLLs Simphony manquantes"

# Étape 1 : Vérification et création du dossier lib
Write-Step 1 4 "Préparation du dossier lib"

if (-not (Test-Path "lib")) {
    New-Item -ItemType Directory -Path "lib" -Force | Out-Null
    Write-Success "Dossier lib créé"
} else {
    Write-Success "Dossier lib existe déjà"
}
Write-Host ""

# Étape 2 : Recherche dans les emplacements standards
Write-Step 2 4 "Recherche dans les emplacements standards"

$StandardPaths = @(
    "C:\Micros\Simphony\Bin",
    "C:\Micros\Simphony\Platform", 
    "C:\Program Files\Micros\Simphony\Bin",
    "C:\Program Files (x86)\Micros\Simphony\Bin",
    "C:\Program Files\Oracle\Micros\Simphony\Bin",
    "C:\Program Files (x86)\Oracle\Micros\Simphony\Bin"
)

$FoundPosCommon = $false
$PosCommonPath = ""

foreach ($Path in $StandardPaths) {
    $DllPath = Join-Path $Path "PosCommonClasses.dll"
    
    if (Test-Path $DllPath) {
        Write-Success "TROUVÉ: $DllPath"
        
        try {
            Copy-Item $DllPath "lib\" -Force
            Write-Success "Copie réussie vers lib\"
            $FoundPosCommon = $true
            $PosCommonPath = $DllPath
            break
        }
        catch {
            Write-Error "Erreur lors de la copie: $($_.Exception.Message)"
        }
    }
}

if (-not $FoundPosCommon) {
    Write-Info "Aucune DLL trouvée dans les emplacements standards"
}
Write-Host ""

# Étape 3 : Recherche récursive si nécessaire
if (-not $FoundPosCommon) {
    Write-Step 3 4 "Recherche récursive sur le disque C:"
    Write-Host "   (Cela peut prendre quelques minutes...)" -ForegroundColor Yellow
    
    try {
        $FoundFiles = Get-ChildItem -Path "C:\" -Name "PosCommonClasses.dll" -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1
        
        if ($FoundFiles) {
            $FullPath = "C:\$FoundFiles"
            Write-Success "TROUVÉ: $FullPath"
            
            try {
                Copy-Item $FullPath "lib\" -Force
                Write-Success "Copie réussie vers lib\"
                $FoundPosCommon = $true
                $PosCommonPath = $FullPath
            }
            catch {
                Write-Error "Erreur lors de la copie: $($_.Exception.Message)"
            }
        } else {
            Write-Error "Aucun fichier PosCommonClasses.dll trouvé sur le disque C:"
        }
    }
    catch {
        Write-Error "Erreur lors de la recherche récursive: $($_.Exception.Message)"
    }
    Write-Host ""
}

# Étape 4 : Vérification finale
Write-Step 4 4 "Vérification finale"

$RequiredDlls = @{
    "PosCommonClasses.dll" = "lib\PosCommonClasses.dll"
    "PosCore.dll" = "lib\PosCore.dll"
    "Ops.dll" = "lib\Ops.dll"
}

Write-Info "État des DLLs requises :"

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

Write-Host ""

# Résultat final
if ($FoundPosCommon -and $AllDllsPresent) {
    Write-Header "✅ SUCCÈS !"
    Write-Host "Toutes les DLLs sont maintenant prêtes" -ForegroundColor Green
    Write-Host "Vous pouvez compiler votre projet" -ForegroundColor Green
    Write-Host ""
    Write-Host "📂 Emplacement source: $PosCommonPath" -ForegroundColor Gray
    Write-Host "📂 Emplacement copie: $(Resolve-Path 'lib\PosCommonClasses.dll')" -ForegroundColor Gray
} elseif ($AllDllsPresent) {
    Write-Header "✅ TOUTES LES DLLs PRÉSENTES"
    Write-Host "Votre projet est prêt à être compilé !" -ForegroundColor Green
} else {
    Write-Header "❌ ÉCHEC"
    Write-Host "PosCommonClasses.dll n'a pas été trouvé" -ForegroundColor Red
    Write-Host "Vérifiez que Simphony est installé" -ForegroundColor Red
    Write-Host ""
    Write-Host "💡 Solutions possibles :" -ForegroundColor Yellow
    Write-Host "1. Vérifiez que Simphony est installé" -ForegroundColor White
    Write-Host "2. Cherchez manuellement PosCommonClasses.dll" -ForegroundColor White
    Write-Host "3. Contactez votre administrateur Simphony" -ForegroundColor White
    Write-Host "4. Copiez depuis un autre poste Simphony" -ForegroundColor White
    Write-Host "5. Téléchargez depuis Oracle Support" -ForegroundColor White
}

Write-Host ""
Write-Host "Appuyez sur une touche pour continuer..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown") 