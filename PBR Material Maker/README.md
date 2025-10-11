# PBR Material Maker

Der **PBR Material Maker** ist ein einfach zu bedienendes Tool, um aus Texturen automatisch GLTF-Dateien f�r PBR-Materialien zu erstellen. Die Anwendung ist f�r Einsteiger geeignet und bietet eine intuitive Oberfl�che mit Drag & Drop Unterst�tzung.

---

## Was ist PBR?

**PBR** steht f�r *Physically Based Rendering*. Dabei werden Materialien so beschrieben, dass sie m�glichst realistisch im 3D-Rendering aussehen. Typische Texturarten sind:

- **Base Color** (Grundfarbe)
- **Normal** (Oberfl�chenstruktur)
- **Occlusion** (Schatten)
- **Roughness** (Rauheit)
- **Metallic** (Metallanteil)
- **Emission** (Selbstleuchten)
- **Alpha** (Transparenz)

---

## Funktionen im �berblick

### 1. Drag & Drop von Texturen

- Ziehe JPG- oder PNG-Dateien direkt auf die jeweiligen Felder (z.B. Base Color, Normal, etc.).
- Die Anwendung erkennt viele g�ngige Namensmuster aus Engines wie Unreal, Unity, Godot, O3DE, CryEngine, Stride, Flax, Snowdrop und mehr.

### 2. Automatisches Ausf�llen

- Wird eine Base Color Textur eingef�gt, werden passende Texturen f�r Normal, Occlusion, Roughness, Metallic, Emission und Alpha automatisch gesucht und zugeordnet.
- Fehlen einzelne Maps, werden sie aus der Base Color generiert.

### 3. Aufl�sung w�hlen

- W�hle eine Zielaufl�sung f�r die Texturen (z.B. 2048x2048, 1024x1024, ... oder eigene Werte).
- Die Texturen werden beim Speichern auf die gew�hlte Gr��e skaliert.

### 4. GLTF-Datei erzeugen

- Mit einem Klick auf **Save** werden alle Texturen und die GLTF-Datei im Unterordner `gltf_textures` gespeichert.
- Die GLTF-Datei enth�lt Verweise auf alle erzeugten Texturen.

### 5. Material Presets

- �ber die Materialauswahl k�nnen vordefinierte Einstellungen f�r verschiedene Materialien geladen werden.
- Die Werte f�r Normal, Roughness, Occlusion, Metallic, Emission und Alpha werden automatisch angepasst.

### 6. Konfiguration �ber material.json

- Die Datei `material.json` erlaubt die Anpassung der Effektst�rken und Presets.
- Beispiel f�r die wichtigsten Parameter:

| Parameter           | Typ      | Bereich      | Beschreibung                                      |
|---------------------|----------|-------------|---------------------------------------------------|
| NormalStrength      | float    | 0.0 - 1.0   | St�rke der Pr�gung/Normalmap                      |
| RoughnessStrength   | float    | 0.0 - 1.0   | St�rke der Rauheit                                |
| OcclusionStrength   | float    | 0.0 - 1.0   | St�rke der Schatten/Occlusion                     |
| MetallicThreshold   | int      | 0 - 255     | Schwellenwert f�r Metallisch                      |
| EmissionStrength    | float    | 0.0 - 1.0   | St�rke der Emission                               |
| AlphaStrength       | float    | 0.0 - 1.0   | St�rke der Transparenz/Alpha                      |
| BaseColorTint       | float[3] | 0.0 - 1.0   | Farb-Tint f�r die Grundfarbe                      |
| NormalMapType       | string   | "sobel"/... | Typ der Normalmap-Generierung                     |
| RoughnessInvert     | bool     | true/false  | Rauheit invertieren                               |
| MetallicIntensity   | float    | 0.0 - 1.0   | Intensit�t des Metall-Effekts                     |
| EmissionColor       | float[3] | 0.0 - 1.0   | Farbe f�r Emission                                |
| AlphaMode           | string   | "opaque"/...| Modus f�r Transparenz                             |

---

## Schritt-f�r-Schritt Anleitung

1. **Textur einf�gen:** Ziehe eine Textur (z.B. Base Color) auf das entsprechende Feld.
2. **Materialname:** Der Name wird automatisch aus dem Dateinamen vorgeschlagen, kann aber angepasst werden.
3. **Material Preset w�hlen:** Optional ein Preset ausw�hlen, um die Einstellungen zu �bernehmen.
4. **Aufl�sung w�hlen:** Die gew�nschte Zielgr��e f�r die Texturen ausw�hlen.
5. **Speichern:** Klicke auf **Save**, um alle Maps und die GLTF-Datei zu generieren.

---

## Unterst�tzte Textur-Endungen

Das Tool erkennt automatisch viele g�ngige Endungen, z.B.:

- **Normal:** `_normal`, `_nrm`, `_normal-ogl`, `_Normals`, `_normalmap`, ...
- **Metallic:** `_metallic`, `_metal`, `_metalTex`, ...
- **Height/Displacement:** `_height`, `_disp`, `_bump`, ...
- **Occlusion/AO:** `_occlusion`, `_ao`, `_ambientocclusion`, ...
- **Roughness:** `_roughness`, `_rough`, ...
- **Emission:** `_emission`, `_emissive`, `_glow`, ...
- **Alpha/Mask:** `_alpha`, `_opacity`, `_mask`, ...

---

## Hinweise f�r Einsteiger

- **Mindestens eine Base Color Textur ist erforderlich.**
- Fehlende Maps werden automatisch aus der Base Color berechnet.
- Die generierten Dateien findest du im Unterordner `gltf_textures`.
- Die Anwendung ist f�r Windows (.NET 10, C# 14.0, WPF) entwickelt.

---

## Lizenz

Siehe [LICENSE](../LICENSE) f�r weitere Informationen.

---

## Hilfe & Support

Bei Fragen oder Problemen kannst du ein Issue auf GitHub er�ffnen oder die Dokumentation im Repository lesen.
