# Test-Compilation.ps1
# Script de test rapide pour vérifier la compilation

Write-Host "🔍 Test de compilation rapide..." -ForegroundColor Cyan
Write-Host ""

# Vérifier MSBuild
try {
    $MSBuildPath = Get-Command msbuild -ErrorAction Stop
    Write-Host "✅ MSBuild trouvé: $($MSBuildPath.Source)" -ForegroundColor Green
} catch {
    Write-Host "❌ MSBuild non trouvé ! Utilisez Developer PowerShell for VS" -ForegroundColor Red
    exit 1
}

# Vérifier les DLLs
$RequiredDlls = @("lib\PosCommonClasses.dll", "lib\PosCore.dll", "lib\Ops.dll")
$AllDllsPresent = $true

foreach ($Dll in $RequiredDlls) {
    if (Test-Path $Dll) {
        Write-Host "✅ $Dll" -ForegroundColor Green
    } else {
        Write-Host "❌ $Dll MANQUANT !" -ForegroundColor Red
        $AllDllsPresent = $false
    }
}

if (-not $AllDllsPresent) {
    Write-Host ""
    Write-Host "❌ Exécutez d'abord: .\Find-SimphonyDlls.ps1" -ForegroundColor Red
    exit 1
}

# Test de compilation
Write-Host ""
Write-Host "🔨 Test de compilation..." -ForegroundColor Cyan

$MSBuildArgs = @(
    "SimphonyPortraitMode.sln"
    "/p:Configuration=Debug"
    "/p:Platform=`"Any CPU`""
    "/nologo"
    "/verbosity:normal"
)

try {
    $MSBuildProcess = Start-Process -FilePath "msbuild" -ArgumentList $MSBuildArgs -Wait -PassThru -NoNewWindow
    
    if ($MSBuildProcess.ExitCode -eq 0) {
        Write-Host ""
        Write-Host "✅ COMPILATION RÉUSSIE !" -ForegroundColor Green
        Write-Host "   Tous les fichiers se compilent correctement" -ForegroundColor Green
        
        if (Test-Path "bin\Debug\SimphonyPortraitMode.dll") {
            $FileInfo = Get-Item "bin\Debug\SimphonyPortraitMode.dll"
            Write-Host "   DLL généré: $($FileInfo.Length) octets" -ForegroundColor Green
        }
    } else {
        Write-Host ""
        Write-Host "❌ ERREURS DE COMPILATION !" -ForegroundColor Red
        Write-Host "   Code d'erreur: $($MSBuildProcess.ExitCode)" -ForegroundColor Red
    }
} catch {
    Write-Host ""
    Write-Host "❌ Erreur lors du lancement de MSBuild: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host ""
Write-Host "Test terminé." -ForegroundColor Gray 