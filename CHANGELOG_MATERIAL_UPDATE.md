# Changelog - Material UI Update

## Änderungen vom 10. Oktober 2025

### Datei-Umbenennungen

Phase 1: Form-Dateien (bereits abgeschlossen)

- **GLTF_Designer_Dialog.cs** → **PBR-Material-Maker-Form.cs**
- **GLTF_Designer_Dialog.Designer.cs** → **PBR-Material-Maker-Form.Designer.cs**  
- **GLTF_Designer_Dialog.resx** → **PBR-Material-Maker-Form.resx**

Phase 2: Projekt-Dateien (neu abgeschlossen)

- **GLTF_Material_Maker.csproj** → **PBR_Material_Maker.csproj**
- **GLTF_Material_Maker.csproj.user** → **PBR_Material_Maker.csproj.user**
- **GLTF_Material_Maker.sln** → **PBR_Material_Maker.sln**
- Solution-Referenzen wurden entsprechend aktualisiert
- Projektname in der Solution von "GLTF Material Maker" zu "PBR Material Maker" geändert

Alle Projekt-Referenzen wurden entsprechend aktualisiert und die Kompilierung funktioniert einwandfrei. Die Namensgebung ist jetzt vollständig konsistent und folgt einem einheitlichen Schema.

### Icon-Update

- **PBR_Packer_Icon.ico** → **PBR_Material_Maker.ico**
- ApplicationIcon in der .csproj Datei wurde aktualisiert
- Content-Verweis in der Projektdatei wurde entsprechend geändert
- Alte Icon-Datei wurde entfernt
- Konsistente Icon-Namensgebung passend zum Projekt

### UI-Verbesserung: Material Preset Anzeige

- **Label korrigiert**: Zeigt jetzt nur "Material Preset:" als statische Überschrift
- **ComboBox-Anzeige verbessert**: Das aktuell ausgewählte Preset wird korrekt in der ComboBox selbst angezeigt
- **Entfernt**: Dynamische Label-Aktualisierung, die das Preset im Label anzeigte
- **Benutzerfreundlichkeit**: Klarere Trennung zwischen Label (Beschreibung) und ComboBox (aktueller Wert)
- **Fix**: ComboBox zeigt das StandardPBR-Preset jetzt sofort beim Start an, nicht erst nach manueller Auswahl
- **Initialisierung verbessert**: Event-Handler wird vor der Auswahl hinzugefügt und Text wird explizit gesetzt

### Technische Verbesserungen

- **SDK aktualisiert**: Von `Microsoft.NET.Sdk.WindowsDesktop` zu `Microsoft.NET.Sdk`
- **Warnung behoben**: NETSDK1137 Warnung über veraltetes WindowsDesktop SDK entfernt
- **Moderne .NET-Unterstützung**: Verwendet das aktuelle empfohlene SDK für Desktop-Anwendungen

### Entfernte Features

- **Material aktivieren Checkbox** wurde entfernt
  - Die Checkbox "Materialauswahl aktivieren" wurde vollständig aus der Benutzeroberfläche entfernt
  - Das Material ist jetzt immer aktiviert und bereit zur Verwendung

### Verbesserte Features

- **Material Preset Anzeige**
  - Das Label zeigt jetzt explizit das aktuelle Material-Preset an
  - Format: "Material Preset: StandardPBR"
  - Das Label wird automatisch aktualisiert, wenn ein anderes Preset ausgewählt wird

- **StandardPBR als Standard**
  - Das System wählt automatisch "StandardPBR" als Standard-Material-Preset aus
  - Falls "StandardPBR" nicht verfügbar ist, wird das erste verfügbare Preset gewählt
  - Die Material-Auswahl ist immer aktiviert und funktionsbereit

### Technische Änderungen

- Entfernung der `checkBoxMaterialSelect` aus dem Designer
- Entfernung des Event-Handlers `CheckBoxMaterialSelect_CheckedChanged`
- Vereinfachtes Layout ohne Checkbox
- Automatische Label-Aktualisierung bei Preset-Änderungen

### Benutzerfreundlichkeit

- Einfachere Benutzeroberfläche ohne unnötige Aktivierungsschritte
- Sofortiger Zugriff auf Material-Presets
- Klarere Anzeige des aktuell gewählten Presets

### Auswirkungen

- Benutzer müssen das Material nicht mehr manuell aktivieren
- Der Workflow ist jetzt direkter und intuitiver
- Alle Material-Funktionen sind sofort verfügbar
