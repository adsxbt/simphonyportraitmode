# Extension SimphonyPortraitMode - Rotation d'écran avec interface utilisateur

## Description

Cette extension pour Oracle MICROS Simphony permet de gérer l'orientation de l'écran des terminaux de caisse avec une interface utilisateur intuitive. Un bouton de rotation est ajouté sur la page d'accueil permettant de changer l'orientation avant la connexion.

## Fonctionnalités

### ✨ Nouvelles fonctionnalités ajoutées

- **Bouton de rotation sur la page d'accueil** : Un bouton visuel est affiché sur l'écran de connexion Simphony
- **Interface de sélection d'orientation** : Dialogue permettant de choisir parmi 4 orientations :
  - 🖥️ Mode Paysage (par défaut)
  - 📱 Mode Portrait (90°)
  - 🔄 Mode Inversé (180°)
  - 📱 Portrait 270°
- **Sauvegarde automatique** : Le choix d'orientation est sauvegardé dans la base de données Simphony
- **Interface utilisateur moderne** : Boutons avec icônes et interface intuitive

### 🔧 Fonctionnalités existantes

- Rotation automatique basée sur la configuration `KioskRotation`
- Support des terminaux configurés comme Kiosk
- Gestion des erreurs avec logging
- Intégration complète avec l'API Simphony

## Installation

### Prérequis

- Oracle MICROS Simphony installé
- .NET Framework 4.8
- Visual Studio 2019 ou plus récent (pour la compilation)
- Permissions d'administration sur le terminal

### Étapes d'installation

1. **Compilation du projet**
   ```bash
   # Cloner ou copier les fichiers dans un dossier
   # Ouvrir SimphonyPortraitMode.sln dans Visual Studio
   # Compiler en mode Release
   ```

2. **Déploiement sur le terminal Simphony**
   ```
   # Copier les fichiers compilés dans le dossier d'extensions Simphony :
   C:\Micros\Simphony\Extensions\SimphonyPortraitMode\
   
   Fichiers nécessaires :
   - SimphonyPortraitMode.dll
   - SimphonyPortraitMode.pdb (optionnel, pour le debug)
   ```

3. **Configuration dans Simphony**
   - Ouvrir l'Enterprise Management Console (EMC)
   - Aller dans Configuration > Extensions
   - Ajouter l'extension `SimphonyPortraitMode`
   - Configurer les paramètres par poste de travail

## Configuration

### Paramètres Simphony (EMC)

Dans la section Extension Data de l'EMC, configurer pour chaque poste de travail :

| Paramètre | Valeur | Description |
|-----------|--------|-------------|
| `IsKiosk` | `true/false` | Active l'extension pour ce poste |
| `KioskRotation` | `0-3` | Orientation par défaut (0=Paysage, 1=Portrait90°, 2=Inversé, 3=Portrait270°) |

### Configuration automatique

L'extension peut également être configurée manuellement via l'interface utilisateur :

1. **Au démarrage de Simphony** : Le bouton de rotation apparaît en haut à droite de l'écran de connexion
2. **Clic sur le bouton** : Ouvre la fenêtre de sélection d'orientation
3. **Sélection d'orientation** : Applique immédiatement le changement et sauvegarde la préférence

## Utilisation

### Interface utilisateur

1. **Page d'accueil Simphony**
   - Un bouton bleu apparaît en haut à droite : `🖥️ Paysage` (selon l'orientation actuelle)
   - Le bouton affiche l'orientation actuelle avec une icône

2. **Sélection d'orientation**
   - Cliquer sur le bouton de rotation
   - Une fenêtre s'ouvre avec 4 options d'orientation
   - L'orientation actuelle est mise en surbrillance (vert)
   - Cliquer sur l'orientation souhaitée pour l'appliquer

3. **Confirmation**
   - Un message de confirmation s'affiche
   - L'orientation est appliquée immédiatement
   - Le paramètre est sauvegardé pour les prochaines sessions

### Rotation programmatique

Pour les développeurs, des méthodes sont disponibles :

```csharp
// Rotation manuelle vers une orientation spécifique
CheckDisplayOrientationSub(Orientation.Clockwise90);

// Rotation séquentielle (cycle entre toutes les orientations)
ToggleOrientation();

// Application d'une orientation avec sauvegarde
ApplyOrientation(Orientation.Default);
SaveOrientationChoice(Orientation.Default);
```

## Développement

### Structure du projet

```
SimphonyPortraitMode/
├── SimphonyPortraitMode.cs       # Classe principale avec UI
├── DisplayManager.cs             # Gestion des paramètres d'affichage
├── DisplaySettings.cs            # Structure des paramètres
├── Orientation.cs                # Énumération des orientations
├── SafeNativeMethods.cs          # API Windows natives
├── Properties/
│   ├── Resources.cs              # Ressources textuelles (générées)
│   └── Resources.resx            # Fichier de ressources
├── SimphonyPortraitModeFactory.cs # Factory pour l'extension
└── SimphonyPortraitMode.csproj   # Fichier de projet
```

### Dépendances

- **Simphony APIs**
  - `Micros.PosCore.Extensibility`
  - `Micros.Ops.Extensibility`
  - `Micros.PosCore.Extensibility.Ops`

- **.NET Framework**
  - `System.Windows.Forms` (pour l'interface utilisateur)
  - `System.Drawing` (pour les graphiques)
  - `System` (API de base)

### Compilation

```bash
# Via Visual Studio
1. Ouvrir SimphonyPortraitMode.sln
2. Sélectionner "Release" configuration
3. Build > Build Solution

# Via ligne de commande
msbuild SimphonyPortraitMode.sln /p:Configuration=Release
```

## Dépannage

### Problèmes courants

1. **Le bouton n'apparaît pas**
   - Vérifier que `IsKiosk` est défini sur `true`
   - Vérifier que l'extension est activée dans EMC
   - Redémarrer Simphony

2. **Erreur de rotation**
   - Vérifier les permissions d'administration
   - Consulter les logs Simphony pour plus de détails
   - Tester avec une résolution d'écran standard

3. **Extension ne se charge pas**
   - Vérifier que tous les fichiers sont dans le bon dossier
   - Vérifier la version .NET Framework
   - Consulter les logs d'événements Windows

### Logs

Les logs sont disponibles dans :
- **Simphony Log Viewer** : Messages de l'extension
- **Windows Event Viewer** : Erreurs système
- **EMC Logs** : Erreurs de configuration

## Support et développement

### Versions

- **Version actuelle** : 1.0.7537.14868
- **Compatibilité** : Simphony 2.x et plus récent
- **.NET Framework** : 4.8 (minimum 4.6.2)

### Contributions

Pour contribuer au développement :

1. Fork le projet
2. Créer une branche pour les modifications
3. Tester sur un environnement Simphony de développement
4. Soumettre une pull request

### Contact

Pour le support technique, consulter :
- Documentation officielle Oracle MICROS Simphony
- Forums de développeurs Simphony
- Support Oracle

---

## Licence

Ce projet est basé sur l'extension originale SimphonyPortraitMode d'Oracle Corporation. Les modifications apportées sont sous licence compatible avec les termes d'utilisation de Simphony.

**© 2018 Oracle Corporation - Extensions par la communauté** 