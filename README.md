# GLTF Packer

**Version:** 2.2.64  
**Target:** .NET 10 (Windows Forms & WPF)  
**Plattform:** Windows 7.0 und neuer

## Beschreibung

GLTF Packer ist ein Tool zum Packen von Texturen und Erstellen von GLTF-Dateien für PBR-Materialien.  
Es unterstützt das Laden, Bearbeiten und Exportieren von Texturen wie Base Color, Occlusion, Roughness, Metallic, Normal, Emission und Alpha.

## Features

- Unterstützung für alle gängigen PBR-Texturtypen
- Automatisches Erkennen und Zuordnen von Texturdateien
- Bildgrößenanpassung (Resize) mit verschiedenen Auflösungen
- Export als GLTF 2.0 mit korrekten Texturpfaden
- Emission, Alpha und weitere Spezialmaps werden unterstützt
- Kompatibel mit SecondLife und anderen Plattformen

## Systemvoraussetzungen

- Windows 7 oder neuer
- .NET 10 Desktop Runtime
- Keine Installation erforderlich, einfach ausführbar

## Installation

1. Repository herunterladen oder Release aus den GitHub-Releases beziehen.
2. Die Datei `GLTF Packer.exe` ausführen.

## Nutzung

1. Texturen per Drag & Drop in die jeweiligen Felder ziehen.
2. Materialnamen vergeben.
3. Auflösung wählen (optional).
4. Mit **Save** die GLTF-Datei und Texturen exportieren.

## Konfiguration

- Die Datei `material.json` wird beim ersten Start automatisch mit Standardwerten angelegt.
- Anpassungen können direkt in der Datei vorgenommen werden.


### Einstellmöglichkeiten

| Schlüssel            | Typ            | Beschreibung                                                                                 | Beispielwert         |
|----------------------|----------------|---------------------------------------------------------------------------------------------|----------------------|
| NormalStrength       | Zahl (float)   | Stärke des Normalmap-Effekts (Kantenprägung)                                                | 0.2                  |
| RoughnessStrength    | Zahl (float)   | Stärke des Roughness-Effekts (Rauheit)                                                      | 0.2                  |
| OcclusionStrength    | Zahl (float)   | Stärke des Occlusion-Effekts (Schatten)                                                     | 1.0                  |
| MetallicThreshold    | Zahl (int)     | Schwellenwert für Metallizität (ab welchem Grauwert als Metall interpretiert wird)           | 200                  |
| EmissionStrength     | Zahl (float)   | Stärke des Emissions-Effekts                                                                | 1.0                  |
| AlphaStrength        | Zahl (float)   | Stärke des Alpha-Kanals                                                                     | 1.0                  |
| BaseColorTint        | Array (float)  | Farb-Tint für Base Color (RGB-Multiplikatoren, z.B. [1.0, 0.8, 0.8] für rötlicheres Material)| [1.0, 1.0, 1.0]      |
| NormalMapType        | String         | Typ der Normalmap-Generierung ("sobel" für Kantenerkennung, "flat" für flache Map)           | "sobel"              |
| RoughnessInvert      | Boolean        | Roughness invertieren (true/false)                                                          | false                |
| MetallicIntensity    | Zahl (float)   | Intensität des Metall-Effekts                                                               | 1.0                  |
| EmissionColor        | Array (float)  | Farbe der Emission (RGB, z.B. [1.0, 1.0, 0.0] für gelb)                                     | [1.0, 1.0, 1.0]      |
| AlphaMode            | String         | Alpha-Modus ("opaque", "mask", "blend")                                                     | "opaque"             |

### Anpassung

- Öffne die Datei `material.json` mit einem Texteditor.
- Ändere die Werte nach Bedarf, z.B.:

  - **NormalStrength**  
    `"NormalStrength": 0.5`  
    Erhöht die Prägung der Normalmap (Kanten werden stärker hervorgehoben).

  - **RoughnessStrength**  
    `"RoughnessStrength": 0.8`  
    Material wirkt rauer, weniger glänzend.

  - **OcclusionStrength**  
    `"OcclusionStrength": 0.5`  
    Weniger starke Schatten, Material wirkt heller.

  - **MetallicThreshold**  
    `"MetallicThreshold": 100`  
    Bereits hellere Bereiche werden als Metall interpretiert.

  - **EmissionStrength**  
    `"EmissionStrength": 2.0`  
    Material leuchtet stärker.

  - **AlphaStrength**  
    `"AlphaStrength": 0.5`  
    Material wird halbtransparent.

  - **BaseColorTint**  
    `"BaseColorTint": [1.0, 0.7, 0.7]`  
    Material erhält einen rötlichen Farbton.

  - **NormalMapType**  
    `"NormalMapType": "flat"`  
    Erzeugt eine flache Normalmap ohne Kantenerkennung.

  - **RoughnessInvert**  
    `"RoughnessInvert": true`  
    Invertiert die Rauheit, helle Bereiche werden rau, dunkle glatt.

  - **MetallicIntensity**  
    `"MetallicIntensity": 0.5`  
    Metall-Effekt wird abgeschwächt.

  - **EmissionColor**  
    `"EmissionColor": [1.0, 1.0, 0.0]`  
    Material leuchtet gelb.

  - **AlphaMode**  
    `"AlphaMode": "blend"`  
    Material wird transparent dargestellt.  
    `"AlphaMode": "mask"` für Maskierung.  
    `"AlphaMode": "opaque"` für undurchsichtiges Material.

**Hinweis:**  
Nach dem Speichern der Änderungen die Anwendung neu starten, damit die neuen Einstellungen übernommen werden.

## Lizenz

Dieses Projekt steht unter der MIT-Lizenz.

---

