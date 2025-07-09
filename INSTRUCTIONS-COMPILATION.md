# 🛠️ Instructions de Compilation - Extensions Simphony

## ✅ Corrections apportées

### 1. **Imports et directives**
- ✅ Ajouté `using SimphonyPortraitMode.Properties;` dans `SimphonyPortraitMode.cs`
- ✅ Ajouté `#nullable disable` dans tous les fichiers .cs
- ✅ Supprimé les commentaires de décompilation
- ✅ Corrections des références aux ressources

### 2. **Utilisation des ressources**
- ✅ Remplacement des chaînes codées en dur par les ressources du fichier `.resx`
- ✅ Utilisation de `Resources.UI_*` pour tous les textes d'interface
- ✅ Gestion des erreurs avec messages localisés

### 3. **Corrections de code**
- ✅ Amélioration de la méthode `ApplyOrientation()`
- ✅ Gestion d'erreurs dans le constructeur
- ✅ Correction de la logique de rotation
- ✅ Nettoyage des ressources dans `Destroy()`

## 🚀 Utilisation

### Étape 1: Localiser les DLLs Simphony

```powershell
# Méthode recommandée (PowerShell)
.\Find-SimphonyDlls.ps1

# Ou méthode simple (double-clic)
find-dlls.bat
```

### Étape 2: Tester la compilation

```powershell
# Test rapide
.\Test-Compilation.ps1

# Ou via batch
test-compilation.bat
```

### Étape 3: Compilation finale

```powershell
# Compilation Release
.\Compile-Extension.ps1

# Ou via batch
compile.bat
```

## 📋 Pré-requis

### Logiciels nécessaires
- ✅ **Visual Studio 2019+** ou **Build Tools for Visual Studio**
- ✅ **.NET Framework 4.8**
- ✅ **PowerShell 5.1+** (inclus dans Windows 10+)

### Fichiers requis
- ✅ **PosCommonClasses.dll** (trouvé automatiquement par `Find-SimphonyDlls.ps1`)
- ✅ **PosCore.dll** (fourni dans `lib/`)
- ✅ **Ops.dll** (fourni dans `lib/`)

## 🔧 Scripts disponibles

| Script | Description | Utilisation |
|--------|-------------|-------------|
| `Find-SimphonyDlls.ps1` | Recherche automatique des DLLs Simphony | **Première étape** |
| `Test-Compilation.ps1` | Test rapide de compilation | **Vérification** |
| `Compile-Extension.ps1` | Compilation complète avec options | **Production** |
| `*.bat` | Lanceurs simples pour chaque script PowerShell | **Simplicité** |

## 🛠️ Développement avec Visual Studio

### Ouvrir le projet
1. Lancer **Visual Studio**
2. Ouvrir `SimphonyPortraitMode.sln`
3. Configurer en mode **Release**
4. Sélectionner plateforme **Any CPU**

### Compiler
1. **Build** > **Build Solution** (Ctrl+Shift+B)
2. Vérifier dans **bin/Release/SimphonyPortraitMode.dll**

### Debug
1. Configurer en mode **Debug**
2. Activer **Just My Code** dans les options
3. Définir des points d'arrêt dans le code

## 🚨 Résolution de problèmes

### Erreur: "PosCommonClasses.dll non trouvé"
```powershell
# Solution automatique
.\Find-SimphonyDlls.ps1

# Solution manuelle
# Copier PosCommonClasses.dll dans lib/
```

### Erreur: "MSBuild non trouvé"
```powershell
# Utiliser Developer PowerShell for Visual Studio
# Ou Developer Command Prompt for Visual Studio
# Au lieu de PowerShell standard
```

### Erreur: "Execution Policy"
```powershell
# Débloquer temporairement
Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope CurrentUser

# Ou utiliser les scripts .bat à la place
```

### Erreur de compilation C#
```powershell
# Vérifier les erreurs avec
.\Test-Compilation.ps1

# Compilation verbose
.\Compile-Extension.ps1 -Verbose
```

## 📁 Structure finale

```
SimphonyPortraitMode/
├── 📄 SimphonyPortraitMode.cs        ← Classe principale ✅
├── 📄 DisplayManager.cs             ← Gestion affichage ✅
├── 📄 Resources.cs/.resx            ← Ressources UI ✅
├── 📄 Find-SimphonyDlls.ps1         ← Recherche DLLs ✅
├── 📄 Test-Compilation.ps1          ← Test rapide ✅
├── 📄 Compile-Extension.ps1         ← Compilation ✅
├── 📁 lib/
│   ├── 📚 PosCommonClasses.dll      ← DLL critique ✅
│   ├── 📚 PosCore.dll               ← DLL Simphony ✅
│   └── 📚 Ops.dll                   ← DLL Simphony ✅
└── 📁 bin/Release/
    └── 📚 SimphonyPortraitMode.dll   ← Extension finale ✅
```

## ✅ Validation

### Test de compilation réussi
- ✅ Pas d'erreurs de compilation
- ✅ DLL généré dans `bin/Release/`
- ✅ Taille du fichier > 0 octets
- ✅ Toutes les ressources incluses

### Test de fonctionnalité
- ✅ Bouton de rotation visible sur écran de connexion
- ✅ Dialogue de sélection d'orientation
- ✅ Sauvegarde des préférences
- ✅ Gestion d'erreurs

## 🎯 Déploiement

Une fois compilé avec succès :

1. **Copier** `bin/Release/SimphonyPortraitMode.dll` vers Simphony
2. **Configurer** l'extension dans EMC
3. **Redémarrer** Simphony
4. **Tester** la fonctionnalité

---

**💡 Astuce finale :** Utilisez d'abord `Test-Compilation.ps1` pour vérifier que tout fonctionne avant de compiler en mode Release ! 