using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Linq;
namespace PBR_Material_Maker
{
    public partial class MainForm : Form
    {
        bool dropValid = false;
        string dropFilename;
        private dynamic materialConfig;
        private TextBox textBoxMaterialName;

        #region Konstruktor & Initialisierung
        public MainForm()
        {          
            InitializeComponent();
            TopMost = true; // Fenster immer im Vordergrund

            Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = appVersion.Major + "." + appVersion.Minor + "." + appVersion.Build;



            // Standard-Konfiguration
            var defaultConfig = new
            {
                NormalStrength = 0.20,
                RoughnessStrength = 0.20,
                OcclusionStrength = 1.0,
                MetallicThreshold = 200,
                EmissionStrength = 1.0,
                AlphaStrength = 1.0,
                BaseColorTint = new float[] { 1.0f, 1.0f, 1.0f },
                NormalMapType = "sobel",
                RoughnessInvert = false,
                MetallicIntensity = 1.0f,
                EmissionColor = new float[] { 1.0f, 1.0f, 1.0f },
                AlphaMode = "opaque"
            };

            // Konfiguration laden oder anlegen
            if (File.Exists("material.json"))
                materialConfig = JsonConvert.DeserializeObject(File.ReadAllText("material.json"));
            else
            {
                materialConfig = defaultConfig;
                File.WriteAllText("material.json", JsonConvert.SerializeObject(defaultConfig, Formatting.Indented));
            }

            if (materialConfig.Materials != null)
            {
                comboBoxMaterialSelect.Items.Clear();
                foreach (var mat in materialConfig.Materials)
                {
                    comboBoxMaterialSelect.Items.Add((string)mat.MaterialName);
                }
                
                // Event-Handler zuerst hinzufügen
                comboBoxMaterialSelect.SelectedIndexChanged += ComboBoxMaterialSelect_SelectedIndexChanged;
                
                // StandardPBR als Standard setzen und immer aktiviert lassen
                int standardIdx = comboBoxMaterialSelect.Items.IndexOf("StandardPBR");
                if (standardIdx >= 0)
                {
                    comboBoxMaterialSelect.SelectedIndex = standardIdx;
                }
                else if (comboBoxMaterialSelect.Items.Count > 0)
                {
                    comboBoxMaterialSelect.SelectedIndex = 0;
                }
                
                // Material-Auswahl immer aktiviert lassen
                comboBoxMaterialSelect.Enabled = true;
                
                // ComboBox-Text explizit setzen, damit das ausgewählte Preset angezeigt wird
                comboBoxMaterialSelect.Text = comboBoxMaterialSelect.SelectedItem?.ToString();
                
                // UI-Elements initial aktualisieren
                UpdateUIFromMaterialConfig();
            }
            else
            {
                comboBoxMaterialSelect.SelectedIndexChanged += ComboBoxMaterialSelect_SelectedIndexChanged;
            }

            // ToolTips für alle relevanten Controls setzen
            toolTip1.SetToolTip(labelORM_AORadius,
                GetTooltip(
                    "AO radius/strength for ORM map. Affects shadow softness.",
                    "Radius/Stärke der AO für die ORM-Map. Beeinflusst die Weichheit der Schatten.",
                    "Rayon/force AO pour la carte ORM. Influence la douceur des ombres.",
                    "Radio/intensidad AO para el mapa ORM. Afecta la suavidad de las sombras."));
            toolTip1.SetToolTip(trackBarORM_AORadius,
                GetTooltip(
                    "AO radius/strength for ORM map. 0 = soft, 1 = hard.",
                    "Radius/Stärke der AO für die ORM-Map. 0 = weich, 1 = hart.",
                    "Rayon/force AO pour la carte ORM. 0 = doux, 1 = dur.",
                    "Radio/intensidad AO para el mapa ORM. 0 = suave, 1 = duro."));
            toolTip1.SetToolTip(textBoxORM_AORadius,
                GetTooltip(
                    "Numeric value for AO radius/strength (0.0 - 1.0).",
                    "Numerischer Wert für AO-Radius/Stärke (0.0 - 1.0).",
                    "Valeur numérique pour le rayon/la force AO (0.0 - 1.0).",
                    "Valor numérico para radio/intensidad AO (0.0 - 1.0)."));
            toolTip1.SetToolTip(labelORM_AOContrast,
                GetTooltip(
                    "AO contrast for ORM map. 1 = standard, >1 = harder, <1 = softer.",
                    "Kontrast der AO für die ORM-Map. 1 = Standard, >1 = härter, <1 = weicher.",
                    "Contraste AO pour la carte ORM. 1 = standard, >1 = plus dur, <1 = plus doux.",
                    "Contraste AO para el mapa ORM. 1 = estándar, >1 = más duro, <1 = más suave."));
            toolTip1.SetToolTip(trackBarORM_AOContrast,
                GetTooltip(
                    "AO contrast for ORM map. 0 = very soft, 2 = very hard.",
                    "Kontrast der AO für die ORM-Map. 0 = sehr weich, 2 = sehr hart.",
                    "Contraste AO pour la carte ORM. 0 = très doux, 2 = très dur.",
                    "Contraste AO para el mapa ORM. 0 = muy suave, 2 = muy duro."));
            toolTip1.SetToolTip(textBoxORM_AOContrast,
                GetTooltip(
                    "Numeric value for AO contrast (0.0 - 2.0).",
                    "Numerischer Wert für AO-Kontrast (0.0 - 2.0).",
                    "Valeur numérique pour le contraste AO (0.0 - 2.0).",
                    "Valor numérico para contraste AO (0.0 - 2.0)."));
            toolTip1.SetToolTip(labelORM_Blend,
                GetTooltip(
                    "Blending between AO and roughness for ORM map.",
                    "Blending zwischen AO und Roughness für die ORM-Map.",
                    "Fusion entre AO et rugosité pour la carte ORM.",
                    "Mezcla entre AO y rugosidad para el mapa ORM."));
            toolTip1.SetToolTip(trackBarORM_Blend,
                GetTooltip(
                    "Blending between AO and roughness. 0 = only AO, 1 = only roughness.",
                    "Blending zwischen AO und Roughness. 0 = nur AO, 1 = nur Roughness.",
                    "Fusion entre AO et rugosité. 0 = seulement AO, 1 = seulement rugosité.",
                    "Mezcla entre AO y rugosidad. 0 = solo AO, 1 = solo rugosidad."));
            toolTip1.SetToolTip(textBoxORM_Blend,
                GetTooltip(
                    "Numeric value for blending (0.0 - 1.0).",
                    "Numerischer Wert für Blending (0.0 - 1.0).",
                    "Valeur numérique pour la fusion (0.0 - 1.0).",
                    "Valor numérico para mezcla (0.0 - 1.0)."));
            toolTip1.SetToolTip(comboBoxMaterialSelect,
                GetTooltip(
                    "Select a material preset.",
                    "Wähle ein Material-Preset aus.",
                    "Sélectionnez un préréglage de matériau.",
                    "Selecciona un preajuste de material."));
            toolTip1.SetToolTip(textBoxMaterialName,
                GetTooltip(
                    "Material name for textures and GLTF file.",
                    "Materialname für die Texturen und GLTF-Datei.",
                    "Nom du matériau pour les textures et le fichier GLTF.",
                    "Nombre del material para las texturas y el archivo GLTF."));
            toolTip1.SetToolTip(comboBoxResolution,
                GetTooltip(
                    "Select the target resolution for textures.",
                    "Wähle die Zielauflösung für die Texturen.",
                    "Sélectionnez la résolution cible pour les textures.",
                    "Selecciona la resolución objetivo para las texturas."));
            toolTip1.SetToolTip(buttonSave,
                GetTooltip(
                    "Packs images and creates a GLTF file.",
                    "Packt Bilder und erstellt eine GLTF-Datei.",
                    "Emballe les images et crée un fichier GLTF.",
                    "Empaqueta imágenes y crea un archivo GLTF."));
            toolTip1.SetToolTip(buttonClear,
                GetTooltip(
                    "Clear all images.",
                    "Alle Bilder löschen.",
                    "Effacer toutes les images.",
                    "Borrar todas las imágenes."));
            toolTip1.SetToolTip(pictureBoxBaseColor,
                GetTooltip(
                    "The RGB data of base color. Any alpha in this image will be stripped.",
                    "Die RGB-Daten der Basisfarbe. Jegliches Alpha in diesem Bild wird entfernt.",
                    "Les données RVB de la couleur de base. Toute transparence sera supprimée.",
                    "Los datos RGB del color base. Cualquier canal alfa será eliminado."));
            toolTip1.SetToolTip(pictureBoxAlpha,
                GetTooltip(
                    "Alpha data will be packed into base color. Pass a greyscale image. Alpha will be extracted from the red channel. Leave blank for no alpha channel.",
                    "Alphadaten werden in die Basisfarbe gepackt. Übergeben Sie ein Graustufenbild. Alpha wird aus dem Rotkanal extrahiert. Leer lassen für keinen Alphakanal.",
                    "Les données alpha seront intégrées à la couleur de base. Utilisez une image en niveaux de gris. L'alpha sera extrait du canal rouge. Laissez vide pour aucun canal alpha.",
                    "Los datos alfa se empaquetarán en el color base. Usa una imagen en escala de grises. El alfa se extraerá del canal rojo. Déjalo vacío para no tener canal alfa."));
            toolTip1.SetToolTip(pictureBoxOcclusion,
                GetTooltip(
                    "Occlusion data for ORM map. Can be left blank.",
                    "Okklusionsdaten für die ORM-Karte. Kann leer gelassen werden.",
                    "Données d'occlusion pour la carte ORM. Peut être laissé vide.",
                    "Datos de oclusión para el mapa ORM. Puede dejarse en blanco."));
            toolTip1.SetToolTip(pictureBoxRoughness,
                GetTooltip(
                    "Roughness data for ORM map. Can be left blank.",
                    "Rauheitsdaten für die ORM-Karte. Kann leer gelassen werden.",
                    "Données de rugosité pour la carte ORM. Peut être laissé vide.",
                    "Datos de rugosidad para el mapa ORM. Puede dejarse en blanco."));
            toolTip1.SetToolTip(pictureBoxMetallic,
                GetTooltip(
                    "Metalness data for ORM map. Can be left blank.",
                    "Metallizitätsdaten für die ORM-Karte. Kann leer gelassen werden.",
                    "Données de métallicité pour la carte ORM. Peut être laissé vide.",
                    "Datos de metalicidad para el mapa ORM. Puede dejarse en blanco."));
            toolTip1.SetToolTip(pictureBoxNormal,
                GetTooltip(
                    "Normal map. Can be left blank.",
                    "Normalenkarte. Kann leer gelassen werden.",
                    "Carte des normales. Peut être laissé vide.",
                    "Mapa de normales. Puede dejarse en blanco."));
            toolTip1.SetToolTip(pictureBoxEmission,
                GetTooltip(
                    "Emission map. Can be left blank.",
                    "Emissionskarte. Kann leer gelassen werden.",
                    "Carte d'émission. Peut être laissé vide.",
                    "Mapa de emisión. Puede dejarse en blanco."));
            toolTip1.SetToolTip(checkBoxKeepOntop,
                GetTooltip(
                    "Keep window on top.",
                    "Fenster immer im Vordergrund halten.",
                    "Garder la fenêtre au-dessus.",
                    "Mantener la ventana siempre encima."));
            toolTip1.SetToolTip(labelVersion,
                GetTooltip(
                    $"Programmversion: {appVersion.Major}.{appVersion.Minor}.{appVersion.Build}",
                    $"Programmversion: {appVersion.Major}.{appVersion.Minor}.{appVersion.Build}",
                    $"Version du programme : {appVersion.Major}.{appVersion.Minor}.{appVersion.Build}",
                    $"Versión del programa: {appVersion.Major}.{appVersion.Minor}.{appVersion.Build}"
                ));

            if (textBoxMaterialName == null)
            {
                textBoxMaterialName = new TextBox();
                textBoxMaterialName.BackColor = SystemColors.ControlDarkDark;
                textBoxMaterialName.Location = new Point(13, 847);
                textBoxMaterialName.Name = "textBoxMaterialName";
                textBoxMaterialName.Size = new Size(96, 23); // Angepasste Größe 96, 23
                textBoxMaterialName.TabIndex = 20;
                toolTip1.SetToolTip(textBoxMaterialName, "Material Name.\r\nAs shown in SecondLife.");
                this.Controls.Add(textBoxMaterialName);
            }

            // Initialize all TrackBar event handlers for real-time updates
            // This must be called after InitializeComponent() has created all controls
            InitializeTrackBarEventHandlers();
        }

        /// <summary>
        /// Recursively allow drag and drop on all PictureBox controls within the given control collection.
        /// </summary>
        private static void AllowAllPictureBoxDragDrop(IEnumerable controlCollection)
        {
            foreach (var control in controlCollection)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.AllowDrop = true;
                }
                if (control is Panel panel)
                {
                    AllowAllPictureBoxDragDrop(panel.Controls);
                }
            }
        }

        /// <summary>
        /// Event handler for the Keep On Top checkbox.
        /// Toggles the TopMost property of the form.
        /// </summary>
        private void CheckBoxKeepOntop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkBoxKeepOntop.Checked;
        }

        /// <summary>
        /// Recursively clears all PictureBox controls within the given control collection.
        /// Disposes of any images to free resources.
        /// </summary>
        private void ClearAllPictureBoxes(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is PictureBox pb)
                {
                    if (pb.Image != null)
                    {
                        pb.Image.Dispose();
                        pb.Image = null;
                    }
                    pb.ImageLocation = null;
                }
                // Rekursiv für Panels und andere Container
                if (control.HasChildren)
                {
                    ClearAllPictureBoxes(control.Controls);
                }
            }
        }

        /// <summary>
        /// Checks if the given text ends with any of the specified endings, ignoring case and certain character differences.
        /// </summary>
        private bool EndsWithAnyCaseInsensitiveFlexible(string text, string[] endings)
        {
            string normText = text.ToLower().Replace("-", "_").Trim('_', ' ');
            foreach (var ending in endings)
            {
                string normEnding = ending.ToLower().Replace("-", "_").Trim('_', ' ');
                if (normText.EndsWith(normEnding)) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the given text ends with any of the specified endings, ignoring case.
        /// </summary>
        private bool EndsWithAnyCaseInsensitive(string text, string[] endings)
        {
            text = text.ToLower();
            foreach (var ending in endings)
            {
                if (text.EndsWith(ending.ToLower())) return true;
            }
            return false;
        }

        // Konstruktor & Initialisierung ende
        #endregion Konstruktor & Initialisierung

        #region UI- und Event-Handler

        /// <summary>
        /// Event handler for the Save button.
        /// Handles saving the material with optional resizing.
        /// </summary>
        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            if (pictureBoxBaseColor.ImageLocation == null)
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Base Color Required");
                return;
            }

            this.UseWaitCursor = true;
            if (progressBarMain != null)
            {
                progressBarMain.Visible = true;
                progressBarMain.Style = ProgressBarStyle.Marquee;
            }

            #region Get/Parse resize parameter
            bool resize = false;
            int resizeX = 1024;
            int resizeY = 1024;
            if (comboBoxResolution.SelectedIndex != 0)
            {
                resize = true;
                string strRes = comboBoxResolution.Text;

                if (strRes == "")
                {
                    strRes = comboBoxResolution.Items[comboBoxResolution.SelectedIndex].ToString();
                }
                this.UseWaitCursor = false;
                MessageBox.Show("strRes " + strRes);
                if (strRes.Contains("*"))
                {
                    string[] tmp = strRes.Split('*');
                    if (tmp.Length != 2)
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Enter a valid resolution in the format 1024 * 1024", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        try
                        {
                            resizeX = int.Parse(tmp[0]);
                            resizeY = int.Parse(tmp[1]);
                        }
                        catch (Exception)
                        {
                            this.UseWaitCursor = false;
                            MessageBox.Show("Enter a valid resolution in the format 1024 * 1024", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    this.UseWaitCursor = false;
                    MessageBox.Show("Enter a valid resolution in the format 1024 * 1024", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            #endregion

            string txtBefore = buttonSave.Text;
            buttonSave.Text = "Please Wait";
            Enabled = false;
            UseWaitCursor = true;

            string resourceData = Encoding.UTF8.GetString(Properties.Resources.pavement_03_4k_TEST2);
            JObject o = JObject.Parse(resourceData);

            string gltf_output_dir = Path.Combine(Path.GetDirectoryName(pictureBoxBaseColor.ImageLocation), "gltf_textures");
            Directory.CreateDirectory(gltf_output_dir);

            // Bitmap-Objekte erzeugen
            Bitmap col = new Bitmap(pictureBoxBaseColor.ImageLocation);

            // Neue Felder auslesen
            float[] baseColorTint = (materialConfig.BaseColorTint is JArray jarr)
                ? jarr.ToObject<float[]>()
                : (materialConfig.BaseColorTint ?? new float[] { 1.0f, 1.0f, 1.0f });
            string normalMapType = materialConfig.NormalMapType ?? "sobel";
            bool roughnessInvert = materialConfig.RoughnessInvert ?? false;
            float metallicIntensity = materialConfig.MetallicIntensity ?? 1.0f;
            float[] emissionColor = (materialConfig.EmissionColor is JArray emissionJarr)
                ? emissionJarr.ToObject<float[]>()
                : (materialConfig.EmissionColor ?? new float[] { 1.0f, 1.0f, 1.0f });
            string alphaMode = materialConfig.AlphaMode ?? "opaque";

            // BaseColorTint anwenden
            ApplyBaseColorTint(col, baseColorTint);

            // NormalMapType verwenden
            Bitmap nrm;
            if (pictureBoxNormal.ImageLocation != null && File.Exists(pictureBoxNormal.ImageLocation))
            {
                nrm = new Bitmap(pictureBoxNormal.ImageLocation);
            }
            else
            {
                if (normalMapType == "sobel")
                    nrm = GenerateNormalMap(col, (float)materialConfig.NormalStrength);
                else
                    nrm = GenerateFlatNormal(col.Width, col.Height); // Beispiel für anderen Typ
            }

            // RoughnessInvert verwenden
            Bitmap bRoughness = GenerateRoughnessMap(col, (float)materialConfig.RoughnessStrength, roughnessInvert);

            // MetallicIntensity verwenden
            Bitmap bMetallic = GenerateMetallicMap(col, (int)materialConfig.MetallicThreshold, metallicIntensity);

            // EmissionColor verwenden
            Bitmap emission = GenerateEmissionMap(col, (float)materialConfig.EmissionStrength, emissionColor);
            Bitmap alpha = GenerateAlphaMap(col, (float)materialConfig.AlphaStrength);
            Bitmap bOcclusion = GenerateOcclusionMap(col, (float)materialConfig.OcclusionStrength);

            Bitmap nrmResized = nrm, bOcclusionResized = bOcclusion, bRoughnessResized = bRoughness, bMetallicResized = bMetallic, emissionResized = emission, alphaResized = alpha, colResized = col;
            if (resize)
            {
                nrmResized = ResizeImage(nrm, resizeX, resizeY);
                bOcclusionResized = ResizeImage(bOcclusion, resizeX, resizeY);
                bRoughnessResized = ResizeImage(bRoughness, resizeX, resizeY);
                bMetallicResized = ResizeImage(bMetallic, resizeX, resizeY);
                emissionResized = ResizeImage(emission, resizeX, resizeY);
                alphaResized = ResizeImage(alpha, resizeX, resizeY);
                colResized = ResizeImage(col, resizeX, resizeY);
            }


            // Speichern der Einzelmaps
            nrmResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_nrm.png"), System.Drawing.Imaging.ImageFormat.Png);
            bOcclusionResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_occ.png"), System.Drawing.Imaging.ImageFormat.Png);
            bRoughnessResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_rough.png"), System.Drawing.Imaging.ImageFormat.Png);
            bMetallicResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_metal.png"), System.Drawing.Imaging.ImageFormat.Png);
            emissionResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_emission.png"), System.Drawing.Imaging.ImageFormat.Png);
            alphaResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_alpha.png"), System.Drawing.Imaging.ImageFormat.Png);
            colResized.Save(Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_col.png"), System.Drawing.Imaging.ImageFormat.Png);

            // ORM-Map erzeugen und speichern, wenn alle Maps vorhanden sind
            try
            {
                if (bOcclusionResized != null && bRoughnessResized != null && bMetallicResized != null)
                {
                    Bitmap orm = GenerateORMMap(bOcclusionResized, bRoughnessResized, bMetallicResized);
                    string ormPath = Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_orm.png");
                    orm.Save(ormPath, System.Drawing.Imaging.ImageFormat.Png);
                    orm.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler beim Erzeugen der ORM-Map (ButtonSave_Click): " + ex.Message);
            }

            // Dispose der ggf. neu erzeugten Bitmaps
            if (resize)
            {
                nrmResized.Dispose();
                bOcclusionResized.Dispose();
                bRoughnessResized.Dispose();
                bMetallicResized.Dispose();
                emissionResized.Dispose();
                alphaResized.Dispose();
                colResized.Dispose();
            }

            // Dispose der Original-Bitmaps
            nrm.Dispose();
            bOcclusion.Dispose();
            bRoughness.Dispose();
            bMetallic.Dispose();
            emission.Dispose();
            alpha.Dispose();
            col.Dispose();

            string orm_file_path = Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_orm.png");
            orm_file_path = Utils.GetRelativePath(pictureBoxBaseColor.ImageLocation, orm_file_path).Replace(@"\", "/").TrimStart('.');
            string col_file_path = Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_col.png");
            col_file_path = Utils.GetRelativePath(pictureBoxBaseColor.ImageLocation, col_file_path).Replace(@"\", "/").TrimStart('.');
            string nrm_file_path = Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_nrm.png");
            nrm_file_path = Utils.GetRelativePath(pictureBoxBaseColor.ImageLocation, nrm_file_path).Replace(@"\", "/").TrimStart('.');

            o["images"][0]["mimeType"] = "image/png";
            o["images"][1]["mimeType"] = "image/png";
            o["images"][2]["mimeType"] = "image/png";

            o["images"][1]["uri"] = "." + col_file_path;
            o["images"][2]["uri"] = "." + orm_file_path;
            if (pictureBoxNormal.ImageLocation != null)
            {
                o["images"][0]["uri"] = "." + nrm_file_path;
            }
            else
            {
                o["materials"][0]["normalTexture"] = null;
            }
            o["materials"][0]["name"] = textBoxMaterialName.Text;

            // Ergänzen Sie die GLTF-Generierung, damit alle erzeugten PNG-Dateien in die images- und textures-Arrays aufgenommen werden.
            // Beispiel: Nach dem bisherigen Hinzufügen der drei Standardbilder, fügen Sie die weiteren Bilder hinzu:

            // Alpha-Map
            ((JArray)o["images"]).Add(JToken.FromObject(new
            {
                mimeType = "image/png",
                name = textBoxMaterialName.Text + "_alpha",
                uri = "./gltf_textures/" + textBoxMaterialName.Text + "_alpha.png"
            }));
            ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));

            // Emission-Map (falls nicht schon vorhanden)
            ((JArray)o["images"]).Add(JToken.FromObject(new
            {
                mimeType = "image/png",
                name = textBoxMaterialName.Text + "_emission",
                uri = "./gltf_textures/" + textBoxMaterialName.Text + "_emission.png"
            }));
            ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));

            // Metallic-Map
            ((JArray)o["images"]).Add(JToken.FromObject(new
            {
                mimeType = "image/png",
                name = textBoxMaterialName.Text + "_metal",
                uri = "./gltf_textures/" + textBoxMaterialName.Text + "_metal.png"
            }));
            ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));

            // Occlusion-Map
            ((JArray)o["images"]).Add(JToken.FromObject(new
            {
                mimeType = "image/png",
                name = textBoxMaterialName.Text + "_occ",
                uri = "./gltf_textures/" + textBoxMaterialName.Text + "_occ.png"
            }));
            ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));

            // Roughness-Map
            ((JArray)o["images"]).Add(JToken.FromObject(new
            {
                mimeType = "image/png",
                name = textBoxMaterialName.Text + "_rough",
                uri = "./gltf_textures/" + textBoxMaterialName.Text + "_rough.png"
            }));
            ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));

            // Danach können Sie die neuen Texturen in den Material-Abschnitt referenzieren, z.B. für alpha, emission, metallic, roughness, occlusion usw.

            if (pictureBoxEmission.ImageLocation != null)
            {
                string emission_file_path_local = Path.Combine(gltf_output_dir, textBoxMaterialName.Text + "_emission.png");
                using (Bitmap emissionBmp = new Bitmap(pictureBoxEmission.ImageLocation))
                {
                    Bitmap emissionBmpResized = emissionBmp;
                    if (resize) emissionBmpResized = ResizeImage(emissionBmp, resizeX, resizeY);
                    emissionBmpResized.Save(emission_file_path_local);
                    if (resize) emissionBmpResized.Dispose();
                }
                string emission_file_path_relative = Utils.GetRelativePath(pictureBoxBaseColor.ImageLocation, emission_file_path_local).Replace(@"\", "/").TrimStart('.');
                o["images"].Last.AddAfterSelf(JToken.FromObject(new ImageToken(emission_file_path_relative)));
                o["textures"].Last.AddAfterSelf(JToken.FromObject(new SourceToken(3)));
                o["materials"][0]["emissiveTexture"] = JToken.FromObject(new IndexToken(3));
                float[] emissiveFactor = new float[] { 1.0f, 1.0f, 1.0f };
                o["materials"][0]["emissiveFactor"] = JArray.FromObject(emissiveFactor);
            }

            // Nach dem Speichern der Einzeltexturen und dem Hinzufügen zu images/textures,
            // müssen die Texturen auch im Material-Abschnitt referenziert werden:

            // Beispiel für Metallic-Roughness-Occlusion (ORM):
            o["materials"][0]["pbrMetallicRoughness"] = new JObject
            {
                ["baseColorTexture"] = new JObject { ["index"] = 1 }, // Annahme: Index 1 ist baseColor
                ["metallicRoughnessTexture"] = new JObject { ["index"] = ((JArray)o["textures"]).Count - 3 }, // Annahme: drittletzte Textur ist metallic/rough
                ["metallicFactor"] = materialConfig.MetallicIntensity,
                ["roughnessFactor"] = materialConfig.RoughnessStrength
            };

            // Emissive-Map korrekt referenzieren:
            o["materials"][0]["emissiveTexture"] = new JObject { ["index"] = ((JArray)o["textures"]).Count - 4 }; // Annahme: viertletzte Textur ist emission
            o["materials"][0]["emissiveFactor"] = JArray.FromObject(emissionColor);

            // Occlusion separat referenzieren:
            o["materials"][0]["occlusionTexture"] = new JObject { ["index"] = ((JArray)o["textures"]).Count - 2 }; // Annahme: vorletzte Textur ist occlusion

            // Alpha-Map ggf. als extra property (z.B. für Maskierung):
            o["materials"][0]["alphaMode"] = alphaMode;

            o["materials"][0]["doubleSided"] = false;
            Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            string strVers = +appVersion.Major + "." + appVersion.Minor + "." + appVersion.Build;
            o["asset"]["generator"] = "PBR Material Maker " + strVers;
            o["asset"]["version"] = "2.0";

            string gltf_path = Path.Combine(Path.GetDirectoryName(pictureBoxBaseColor.ImageLocation), textBoxMaterialName.Text + ".gltf");
            File.WriteAllText(gltf_path, JsonConvert.SerializeObject(o, Formatting.Indented));

            UseWaitCursor = false;
            buttonSave.Text = "Saved!";
            this.Refresh();
            await Task.Delay(3000);
            Enabled = true;
            buttonSave.Text = txtBefore;

            if (progressBarMain != null)
            {
                progressBarMain.Visible = false;
            }
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Event handler for the Batch Save button.
        /// </summary>
        private async void ButtonBatchSave_Click(object sender, EventArgs e)
        {
            // Verzeichnis und Suffixe für BaseColor-Texturen bestimmen
            if (pictureBoxBaseColor.ImageLocation == null)
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Base Color Required");
                return;
            }

            this.UseWaitCursor = true;
            if (progressBarMain != null)
            {
                progressBarMain.Visible = true;
                progressBarMain.Style = ProgressBarStyle.Marquee;
            }

            string baseDir = Path.GetDirectoryName(pictureBoxBaseColor.ImageLocation);
            string[] baseColorSuffixes = new string[] { "_albedo", "_base", "_color", "_col", "_diffuse", "_diff", "_basecol", "_basecolor" };
            string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".tif", ".tiff", ".tga", ".exr", ".hdr" };

            // Alle Bilddateien im Verzeichnis sammeln
            var allFiles = Directory.GetFiles(baseDir)
                .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLower()))
                .ToArray();

            // Alle BaseColor-Texturen finden
            var baseColorFiles = allFiles.Where(f =>
                baseColorSuffixes.Any(suf => Path.GetFileNameWithoutExtension(f).ToLower().EndsWith(suf))
            ).ToList();
            if (baseColorFiles.Count == 0)
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Keine BaseColor-Texturen im Verzeichnis gefunden.");
                return;
            }

            // Resize-Parameter wie gehabt
            bool resize = false;
            int resizeX = 1024;
            int resizeY = 1024;
            if (comboBoxResolution.SelectedIndex != 0)
            {
                resize = true;
                string strRes = comboBoxResolution.Text;
                if (strRes == "")
                {
                    strRes = comboBoxResolution.Items[comboBoxResolution.SelectedIndex].ToString();
                }
                if (strRes.Contains("*"))
                {
                    string[] tmp = strRes.Split('*');
                    if (tmp.Length == 2)
                    {
                        try
                        {
                            resizeX = int.Parse(tmp[0]);
                            resizeY = int.Parse(tmp[1]);
                        }
                        catch (Exception)
                        {
                            this.UseWaitCursor = false;
                            MessageBox.Show("Enter a valid resolution in the format 1024 * 1024", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Enter a valid resolution in the format 1024 * 1024", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    this.UseWaitCursor = false;
                    MessageBox.Show("Enter a valid resolution in the format 1024 * 1024", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string txtBefore = buttonSave.Text;
            buttonSave.Text = "Please Wait";
            Enabled = false;
            UseWaitCursor = true;

            string gltf_output_dir = Path.Combine(baseDir, "gltf_textures");
            Directory.CreateDirectory(gltf_output_dir);

            // Für jede BaseColor-Textur im Verzeichnis
            foreach (var baseColorFile in baseColorFiles)
            {
                string matName = Path.GetFileNameWithoutExtension(baseColorFile);
                // Suffix entfernen
                foreach (var suf in baseColorSuffixes)
                {
                    if (matName.ToLower().EndsWith(suf))
                    {
                        matName = matName.Substring(0, matName.Length - suf.Length);
                        break;
                    }
                }

                // Passende weitere Maps suchen
                string FindMap(string[] suffixes)
                {
                    foreach (var file in allFiles)
                    {
                        string name = Path.GetFileNameWithoutExtension(file).ToLower();
                        foreach (var suf in suffixes)
                        {
                            if (name == matName.ToLower() + suf)
                                return file;
                            if (name.EndsWith(suf) && name.StartsWith(matName.ToLower()))
                                return file;
                        }
                    }
                    return null;
                }
                string normalFile = FindMap(new string[] { "_normal", "_norm", "_nrml", "_nrm", "_nor", "_n", "_normalmap", "_nm", "_Normals", "_NormalMap", "_nor", "_normals", "_normal_map", "_nmap", "_nml", "_nmlmap", "_nrmTex", "_nrmTexture", "_normal-ogl" });
                string occFile = FindMap(new string[] { "_ambient", "_occlusion", "_ao", "_ambientocclusion", "_Occ", "_Occlusion", "_AO", "_aoTex", "_aoTexture", "_ambient_occlusion", "_occlusionmap", "_occlusion_map" });
                string metalFile = FindMap(new string[] { "_metallic", "_metalness", "_mtl", "_metal", "_Metal", "_Metallic", "_metalTex", "_metalTexture", "_metal_map", "_metallicmap", "_metallic_map" });
                string roughFile = FindMap(new string[] { "_roughness", "_rough", "_roug", "_rgh", "_Rough", "_Roughness", "_roughTex", "_roughTexture", "_rough_map", "_roughnessmap", "_roughness_map" });
                string emissionFile = FindMap(new string[] { "_emission", "_emiss", "_emit", "_Emissive", "_Emiss", "_emissiveTex", "_emissiveTexture", "_emissive_map", "_emissionmap", "_emission_map", "_Glow", "_glow", "_illum", "_illumination" });
                string alphaFile = FindMap(new string[] { "_alpha", "_transparency", "_Opacity", "_opacity", "_mask", "_Mask", "_transTex", "_transTexture", "_alpha_map", "_alphamap", "_alphaMap" });

                // GLTF-Template laden
                string resourceData = Encoding.UTF8.GetString(Properties.Resources.pavement_03_4k_TEST2);
                JObject o = JObject.Parse(resourceData);

                // Bitmap-Objekte erzeugen
                Bitmap col = new Bitmap(baseColorFile);
                float[] baseColorTint = (materialConfig.BaseColorTint is JArray jarr)
                    ? jarr.ToObject<float[]>()
                    : (materialConfig.BaseColorTint ?? new float[] { 1.0f, 1.0f, 1.0f });
                string normalMapType = materialConfig.NormalMapType ?? "sobel";
                bool roughnessInvert = materialConfig.RoughnessInvert ?? false;
                float metallicIntensity = materialConfig.MetallicIntensity ?? 1.0f;
                float[] emissionColor = (materialConfig.EmissionColor is JArray emissionJarr)
                    ? emissionJarr.ToObject<float[]>()
                    : (materialConfig.EmissionColor ?? new float[] { 1.0f, 1.0f, 1.0f });
                string alphaMode = materialConfig.AlphaMode ?? "opaque";

                ApplyBaseColorTint(col, baseColorTint);

                Bitmap nrm = null;
                if (normalFile != null && File.Exists(normalFile))
                    nrm = new Bitmap(normalFile);
                else
                    nrm = (normalMapType == "sobel") ? GenerateNormalMap(col, (float)materialConfig.NormalStrength) : GenerateFlatNormal(col.Width, col.Height);

                Bitmap bRoughness = (roughFile != null && File.Exists(roughFile)) ? new Bitmap(roughFile) : GenerateRoughnessMap(col, (float)materialConfig.RoughnessStrength, roughnessInvert);
                Bitmap bMetallic = (metalFile != null && File.Exists(metalFile)) ? new Bitmap(metalFile) : GenerateMetallicMap(col, (int)materialConfig.MetallicThreshold, metallicIntensity);
                Bitmap emission = (emissionFile != null && File.Exists(emissionFile)) ? new Bitmap(emissionFile) : GenerateEmissionMap(col, (float)materialConfig.EmissionStrength, emissionColor);
                Bitmap alpha = (alphaFile != null && File.Exists(alphaFile)) ? new Bitmap(alphaFile) : GenerateAlphaMap(col, (float)materialConfig.AlphaStrength);
                Bitmap bOcclusion = (occFile != null && File.Exists(occFile)) ? new Bitmap(occFile) : GenerateOcclusionMap(col, (float)materialConfig.OcclusionStrength);

                Bitmap nrmResized = nrm, bOcclusionResized = bOcclusion, bRoughnessResized = bRoughness, bMetallicResized = bMetallic, emissionResized = emission, alphaResized = alpha, colResized = col;
                if (resize)
                {
                    nrmResized = ResizeImage(nrm, resizeX, resizeY);
                    bOcclusionResized = ResizeImage(bOcclusion, resizeX, resizeY);
                    bRoughnessResized = ResizeImage(bRoughness, resizeX, resizeY);
                    bMetallicResized = ResizeImage(bMetallic, resizeX, resizeY);
                    emissionResized = ResizeImage(emission, resizeX, resizeY);
                    alphaResized = ResizeImage(alpha, resizeX, resizeY);
                    colResized = ResizeImage(col, resizeX, resizeY);
                }


                // Speichern der Einzelmaps
                nrmResized.Save(Path.Combine(gltf_output_dir, matName + "_nrm.png"), System.Drawing.Imaging.ImageFormat.Png);
                bOcclusionResized.Save(Path.Combine(gltf_output_dir, matName + "_occ.png"), System.Drawing.Imaging.ImageFormat.Png);
                bRoughnessResized.Save(Path.Combine(gltf_output_dir, matName + "_rough.png"), System.Drawing.Imaging.ImageFormat.Png);
                bMetallicResized.Save(Path.Combine(gltf_output_dir, matName + "_metal.png"), System.Drawing.Imaging.ImageFormat.Png);
                emissionResized.Save(Path.Combine(gltf_output_dir, matName + "_emission.png"), System.Drawing.Imaging.ImageFormat.Png);
                                // Zeige die zuletzt generierte Emission-Map in der UI an (nur für das aktuell geladene Bild)
                                if (baseColorFile == pictureBoxBaseColor.ImageLocation)
                                {
                                    pictureBoxEmission.ImageLocation = Path.Combine(gltf_output_dir, matName + "_emission.png");
                                }
                alphaResized.Save(Path.Combine(gltf_output_dir, matName + "_alpha.png"), System.Drawing.Imaging.ImageFormat.Png);
                colResized.Save(Path.Combine(gltf_output_dir, matName + "_col.png"), System.Drawing.Imaging.ImageFormat.Png);

                // ORM-Map erzeugen und speichern, wenn alle Maps vorhanden sind
                try
                {
                    if (bOcclusionResized != null && bRoughnessResized != null && bMetallicResized != null)
                    {
                        Bitmap orm = GenerateORMMap(bOcclusionResized, bRoughnessResized, bMetallicResized);
                        string ormPath = Path.Combine(gltf_output_dir, matName + "_orm.png");
                        orm.Save(ormPath, System.Drawing.Imaging.ImageFormat.Png);
                        // Zeige die zuletzt generierte ORM-Map in der UI an (nur für den letzten Durchlauf relevant)
                        if (baseColorFile == pictureBoxBaseColor.ImageLocation)
                        {
                            pictureBoxORM.ImageLocation = ormPath;
                        }
                        pictureBoxORM.ImageLocation = ormPath;
                        orm.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Erzeugen der ORM-Map (BatchSave, {matName}): " + ex.Message);
                }

                if (resize)
                {
                    nrmResized.Dispose();
                    bOcclusionResized.Dispose();
                    bRoughnessResized.Dispose();
                    bMetallicResized.Dispose();
                    emissionResized.Dispose();
                    alphaResized.Dispose();
                    colResized.Dispose();
                }
                nrm.Dispose();
                bOcclusion.Dispose();
                bRoughness.Dispose();
                bMetallic.Dispose();
                emission.Dispose();
                alpha.Dispose();
                col.Dispose();

                string orm_file_path = Path.Combine(gltf_output_dir, matName + "_orm.png");
                orm_file_path = Utils.GetRelativePath(baseColorFile, orm_file_path).Replace(@"\", "/").TrimStart('.');
                string col_file_path = Path.Combine(gltf_output_dir, matName + "_col.png");
                col_file_path = Utils.GetRelativePath(baseColorFile, col_file_path).Replace(@"\", "/").TrimStart('.');
                string nrm_file_path = Path.Combine(gltf_output_dir, matName + "_nrm.png");
                nrm_file_path = Utils.GetRelativePath(baseColorFile, nrm_file_path).Replace(@"\", "/").TrimStart('.');

                o["images"][0]["mimeType"] = "image/png";
                o["images"][1]["mimeType"] = "image/png";
                o["images"][2]["mimeType"] = "image/png";

                o["images"][1]["uri"] = "." + col_file_path;
                o["images"][2]["uri"] = "." + orm_file_path;
                if (normalFile != null)
                {
                    o["images"][0]["uri"] = "." + nrm_file_path;
                }
                else
                {
                    o["materials"][0]["normalTexture"] = null;
                }
                o["materials"][0]["name"] = matName;

                // Weitere Texturen wie gehabt hinzufügen
                ((JArray)o["images"]).Add(JToken.FromObject(new { mimeType = "image/png", name = matName + "_alpha", uri = "./gltf_textures/" + matName + "_alpha.png" }));
                ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));
                ((JArray)o["images"]).Add(JToken.FromObject(new { mimeType = "image/png", name = matName + "_emission", uri = "./gltf_textures/" + matName + "_emission.png" }));
                ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));
                ((JArray)o["images"]).Add(JToken.FromObject(new { mimeType = "image/png", name = matName + "_metal", uri = "./gltf_textures/" + matName + "_metal.png" }));
                ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));
                ((JArray)o["images"]).Add(JToken.FromObject(new { mimeType = "image/png", name = matName + "_occ", uri = "./gltf_textures/" + matName + "_occ.png" }));
                ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));
                ((JArray)o["images"]).Add(JToken.FromObject(new { mimeType = "image/png", name = matName + "_rough", uri = "./gltf_textures/" + matName + "_rough.png" }));
                ((JArray)o["textures"]).Add(JToken.FromObject(new { source = ((JArray)o["images"]).Count - 1 }));

                // Material-Referenzen wie gehabt
                o["materials"][0]["pbrMetallicRoughness"] = new JObject
                {
                    ["baseColorTexture"] = new JObject { ["index"] = 1 },
                    ["metallicRoughnessTexture"] = new JObject { ["index"] = ((JArray)o["textures"]).Count - 3 },
                    ["metallicFactor"] = materialConfig.MetallicIntensity,
                    ["roughnessFactor"] = materialConfig.RoughnessStrength
                };
                o["materials"][0]["emissiveTexture"] = new JObject { ["index"] = ((JArray)o["textures"]).Count - 4 };
                o["materials"][0]["emissiveFactor"] = JArray.FromObject(emissionColor);
                o["materials"][0]["occlusionTexture"] = new JObject { ["index"] = ((JArray)o["textures"]).Count - 2 };
                o["materials"][0]["alphaMode"] = alphaMode;
                o["materials"][0]["doubleSided"] = false;
                Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;
                string strVers = appVersion.Major + "." + appVersion.Minor + "." + appVersion.Build;
                o["asset"]["generator"] = "PBR Material Maker " + strVers;
                o["asset"]["version"] = "2.0";

                string gltf_path = Path.Combine(baseDir, matName + ".gltf");
                File.WriteAllText(gltf_path, JsonConvert.SerializeObject(o, Formatting.Indented));
            }

            UseWaitCursor = false;
            buttonSave.Text = "Batch Saved!";
            this.Refresh();
            await Task.Delay(3000);
            Enabled = true;
            buttonSave.Text = txtBefore;

            if (progressBarMain != null)
            {
                progressBarMain.Visible = false;
            }
            this.UseWaitCursor = false;
        }

        /// <summary>
        /// Event handler for the Clear button.
        /// </summary>
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ClearAllPictureBoxes(this.Controls);
            buttonSave.Enabled = pictureBoxBaseColor.ImageLocation != null;
            
            // PBR-Vorschau nach Clear aktualisieren
            UpdatePBRPreview();
        }

        /// <summary>
        /// Event handler for the Batch Copy button.
        /// </summary>
        private void ButtonBatchCopy_Click(object sender, EventArgs e)
        {
            using (var sourceDialog = new FolderBrowserDialog())
            using (var targetDialog = new FolderBrowserDialog())
            {
                sourceDialog.Description = "Quellverzeichnis für Batch Copy auswählen";
                targetDialog.Description = "Zielverzeichnis für Batch Copy auswählen";

                if (sourceDialog.ShowDialog() == DialogResult.OK)
                {
                    if (targetDialog.ShowDialog() == DialogResult.OK)
                    {
                        string sourcePath = sourceDialog.SelectedPath;
                        string targetPath = targetDialog.SelectedPath;

                        try
                        {
                            CopyAllImagesFlat(sourcePath, targetPath);
                            this.UseWaitCursor = false;
                            MessageBox.Show("Batch Copy abgeschlossen!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            this.UseWaitCursor = false;
                            MessageBox.Show("Fehler beim Kopieren: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        } 

        /// <summary>
        /// When the user is dragging a file or folder over any picturebox.
        /// </summary>
        private void PictureBoxDragEnter(object sender, DragEventArgs e)
        {
            try
            {
                Debug.WriteLine("DragEnter");
                Array data = e.Data.GetData("FileDrop") as Array;
                dropFilename = ((string[])data)[0];

                string ext = Path.GetExtension(dropFilename).ToLower();
                if ((ext == ".jpg") || (ext == ".jpeg") || (ext == ".png") || (ext == ".bmp") || (ext == ".tif") || (ext == ".tiff") || (ext == ".tga") || (ext == ".exr") || (ext == ".hdr"))
                {
                    e.Effect = DragDropEffects.Copy;
                    dropValid = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception during drag enter: " + ex.ToString());
            }
            e.Effect = DragDropEffects.None;
            dropValid = false;
        }

        /// <summary>
        /// When the user drops a file or folder onto any picturebox.
        /// </summary>
        private void PictureBoxDragDrop(object sender, DragEventArgs e)
        {
            if (dropValid)
            {
                ((PictureBox)sender).ImageLocation = dropFilename;
                if (sender == pictureBoxBaseColor)
                {
                    string fileName = Path.GetFileNameWithoutExtension(dropFilename);
                    string[] ext_base_col = new string[] { "_albedo", "_base", "_color", "_col", "_diffuse", "_diff", "_basecol", "_basecolor" };
                    int i = ext_base_col.Length;
                    while (i-- > 0)
                    {
                        if (fileName.ToLower().EndsWith(ext_base_col[i]))
                        {
                            fileName = fileName.Substring(0, fileName.Length - ext_base_col[i].Length);
                        }
                    }
                    textBoxMaterialName.Text = fileName;
                    Autofill_from_color(textBoxMaterialName.Text, Path.GetDirectoryName(dropFilename), Path.GetExtension(dropFilename).ToLower());
                }
                
                // PBR-Vorschau aktualisieren
                UpdatePBRPreview();
            }
            buttonSave.Enabled = pictureBoxBaseColor.ImageLocation != null;
        }

        /// <summary>
        /// Ensures that the form width is at least minWidth.
        /// </summary>
        private void Form1_Shown(object sender, EventArgs e)
        {
            Ensure_width();
            comboBoxResolution.SelectedIndex = 0;
        }

        /// <summary>
        /// When the form loads.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMaterialPresets();
            var cfg = Program.ProgramConfig;
            Height = cfg.WindowHeight;
            Width = cfg.WindowWidth;
            
            // Event-Handler für den "Generiere Fehlende Maps" Button
            buttonGenerateMaps.Click += ButtonGenerateMaps_Click;
            
            // Initiale PBR-Vorschau rendern
            UpdatePBRPreview();
        }

        /// <summary>
        /// When the form is closing.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.SaveProgramConfig();
        }
        
        /// <summary>
        /// When the form resizing ends.
        /// </summary>
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Ensure_width();
            Program.ProgramConfig.WindowHeight = Height;
            Program.ProgramConfig.WindowWidth = Width;
        }

        /// <summary>
        /// When the selected index in the resolution combobox changes.
        /// </summary>
        private void ComboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxResolution.SelectedIndex == (comboBoxResolution.Items.Count - 1))
            {
                // Selected Custom Resolution
                comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxResolution.SelectedText = "1024 * 1024";
                comboBoxResolution.SelectAll();

            }
            else
            {
                comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
        
        #endregion UI- und Event-Handler

        #region Material- und Bildverarbeitung

        /// <summary>
        /// Autofill texture slots based on the base color texture name.
        /// </summary>
        private void Autofill_from_color(string mat_name, string dir, string extension)
        {
            this.UseWaitCursor = true;

            // Suche alle Dateien mit der gewünschten Extension im Verzeichnis
            string search_pattern = "*" + extension;
            string[] files = Directory.GetFiles(dir, search_pattern);

            // Standard-Endungen aus verschiedenen Engines inkl. Ergänzungen
            string[] ext_normals = new string[] {
                "_normal", "_norm", "_nrml", "_nrm", "_nor", "_n", "_normalmap", "_nm",
                "_Normals", "_NormalMap", "_nor", "_normals", "_normal_map", "_nmap", "_nml", "_nmlmap",
                "_nrmTex", "_nrmTexture", "_normal-ogl",
                "_1K-JPG_NormalGL", "_2K-JPG_NormalGL", "_1K-PNG_NormalGL", "_2K-PNG_NormalGL"
            };
            string[] ext_occlusion = new string[] {
                "_ambient", "_occlusion", "_ao", "_ambientocclusion", "_Occ", "_Occlusion", "_AO", "_aoTex", "_aoTexture",
                "_ambient_occlusion", "_occlusionmap", "_occlusion_map",
                "_1K-JPG_AmbientOcclusion", "_2K-JPG_AmbientOcclusion", "_1K-PNG_AmbientOcclusion", "_2K-PNG_AmbientOcclusion", "_AmbientOcclusion"
            };
            string[] ext_metallic = new string[] {
                "_metallic", "_metalness", "_mtl", "_metal", "_Metal", "_Metallic", "_metalTex", "_metalTexture",
                "_metal_map", "_metallicmap", "_metallic_map",
                "_1K-JPG_Metallic", "_2K-JPG_Metallic", "_1K-PNG_Metallic", "_2K-PNG_Metallic"
            };
            string[] ext_roughness = new string[] {
                "_roughness", "_rough", "_roug", "_rgh", "_Rough", "_Roughness", "_roughTex", "_roughTexture",
                "_rough_map", "_roughnessmap", "_roughness_map",
                "_1K-JPG_Roughness", "_2K-JPG_Roughness", "_1K-PNG_Roughness", "_2K-PNG_Roughness"
            };
            string[] ext_emission = new string[] {
                "_emission", "_emiss", "_emit", "_Emissive", "_Emiss", "_emissiveTex", "_emissiveTexture",
                "_emissive_map", "_emissionmap", "_emission_map", "_Glow", "_glow", "_illum", "_illumination",
                "_1K-JPG_Emission", "_2K-JPG_Emission", "_1K-PNG_Emission", "_2K-PNG_Emission"
            };
            string[] ext_alpha = new string[] {
                "_alpha", "_transparency", "_Opacity", "_opacity", "_mask", "_Mask", "_transTex", "_transTexture",
                "_alpha_map", "_alphamap", "_alphaMap",
                "_1K-JPG_Alpha", "_2K-JPG_Alpha", "_1K-PNG_Alpha", "_2K-PNG_Alpha"
            };
            string[] ext_height = new string[] {
                "_height", "_Height", "_disp", "_displacement", "_bump", "_bumpmap",
                "_1K-JPG_Opacity", "_2K-JPG_Opacity", "_1K-PNG_Opacity", "_2K-PNG_Opacity"
            };

            foreach (string file in files)
            {
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_normals)) { pictureBoxNormal.ImageLocation = file; continue; }
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_occlusion)) { pictureBoxOcclusion.ImageLocation = file; continue; }
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_metallic)) { pictureBoxMetallic.ImageLocation = file; continue; }
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_roughness)) { pictureBoxRoughness.ImageLocation = file; continue; }
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_emission)) { pictureBoxEmission.ImageLocation = file; continue; }
                // Falls Emission gefunden, direkt anzeigen (wird oben schon gemacht)
                // Zeige die generierte ORM-Map direkt an
                string ormPath = Path.Combine(dir, mat_name + "_orm.png");
                if (File.Exists(ormPath))
                {
                    pictureBoxORM.ImageLocation = ormPath;
                }
                // Zeige die generierte Emission-Map direkt an
                string emissionPath = Path.Combine(dir, mat_name + "_emission.png");
                if (File.Exists(emissionPath))
                {
                    pictureBoxEmission.ImageLocation = emissionPath;
                }
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_alpha)) { pictureBoxAlpha.ImageLocation = file; continue; }
                if (EndsWithAnyCaseInsensitiveFlexible(Path.GetFileNameWithoutExtension(file), ext_height)) { /* Hier können Sie z.B. ein PictureBox für Height/Displacement zuweisen */ continue; }
                // Kann man hier ORM Integrieren?
            }            
            
            // ORM-Map erzeugen, wenn alle Maps vorhanden sind
            if (pictureBoxOcclusion.ImageLocation != null && pictureBoxRoughness.ImageLocation != null && pictureBoxMetallic.ImageLocation != null)
            {
                try
                {
                    using (Bitmap occ = new Bitmap(pictureBoxOcclusion.ImageLocation))
                    using (Bitmap rough = new Bitmap(pictureBoxRoughness.ImageLocation))
                    using (Bitmap metal = new Bitmap(pictureBoxMetallic.ImageLocation))
                    {
                        Bitmap orm = GenerateORMMap(occ, rough, metal);
                        string ormPath = Path.Combine(dir, mat_name + "_orm.png");
                        orm.Save(ormPath, System.Drawing.Imaging.ImageFormat.Png);
                        orm.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Fehler beim Erzeugen der ORM-Map: " + ex.Message);
                }
            }

            // PBR-Vorschau nach Autofill aktualisieren
            UpdatePBRPreview();

            this.UseWaitCursor = false;

        }

        /// <summary>
        /// Generates an ORM map from the given Occlusion, Roughness, and Metallic maps.
        /// </summary>
        /// <param name="occ">Occlusion Bitmap</param>
        /// <param name="rough">Roughness Bitmap</param>
        /// <param name="metal">Metallic Bitmap</param>
        /// <returns>Bitmap representing the combined ORM map</returns>
        private Bitmap GenerateORMMap(Bitmap occ, Bitmap rough, Bitmap metal)
        {
            int width = occ.Width;
            int height = occ.Height;
            Bitmap orm = new Bitmap(width, height);

            // ORM-Parameter robust auslesen (Fallback auf Standardwerte)
            float aoRadius = 1.0f, aoContrast = 1.0f, blend = 0.0f;
            try { aoRadius = trackBarORM_AORadius != null ? trackBarORM_AORadius.Value / 100.0f : 1.0f; } catch { aoRadius = 1.0f; }
            try { aoContrast = trackBarORM_AOContrast != null ? trackBarORM_AOContrast.Value / 100.0f : 1.0f; } catch { aoContrast = 1.0f; }
            try { blend = trackBarORM_Blend != null ? trackBarORM_Blend.Value / 100.0f : 0.0f; } catch { blend = 0.0f; }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color cOcc = occ.GetPixel(x, y);
                    Color cRough = rough.GetPixel(x, y);
                    Color cMetal = metal.GetPixel(x, y);

                    // AO-Radius/Strength anwenden (Skalierung)
                    float ao = cOcc.R / 255.0f;
                    ao = (float)Math.Pow(ao, 1.0f / Math.Max(aoRadius, 0.01f));

                    // AO-Kontrast anwenden
                    ao = (float)Math.Pow(ao, aoContrast);

                    // Blending zwischen AO und Roughness
                    float roughVal = cRough.R / 255.0f;
                    float aoBlended = ao * (1.0f - blend) + roughVal * blend;
                    int aoByte = (int)(Math.Max(0, Math.Min(1, aoBlended)) * 255);

                    // ORM: R = AO, G = Roughness, B = Metal
                    Color cORM = Color.FromArgb(aoByte, cRough.R, cMetal.R);
                    orm.SetPixel(x, y, cORM);
                }
            }
            return orm;
        }

        /// <summary>
        /// Applies a color tint to the base color bitmap.
        /// </summary>
        /// <param name="bitmap">The base color bitmap to tint.</param>
        /// <param name="tint">An array of three floats representing the RGB tint factors.</param>
        private void ApplyBaseColorTint(Bitmap bitmap, float[] tint)
        {
            if (tint == null || tint.Length != 3) return;
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bitmap.GetPixel(x, y);
                    int r = Math.Min(255, (int)(c.R * tint[0]));
                    int g = Math.Min(255, (int)(c.G * tint[1]));
                    int b = Math.Min(255, (int)(c.B * tint[2]));
                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
        }

        /// <summary>
        /// Generates a normal map using the Sobel operator from the given albedo bitmap.
        /// </summary>
        /// <param name="albedo">The albedo bitmap to generate the normal map from.</param>
        /// <param name="embossStrength">The strength of the emboss effect.</param>
        /// <returns>A bitmap representing the generated normal map.</returns>
        private Bitmap GenerateNormalMap(Bitmap albedo, float embossStrength = 0.05f)
        {
            int width = albedo.Width;
            int height = albedo.Height;
            Bitmap normalMap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            // Graustufen aus Albedo
            float[,] gray = new float[width, height];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color c = albedo.GetPixel(x, y);
                    gray[x, y] = (c.R + c.G + c.B) / 3f / 255f;
                }

            // Sobel-Operator für X/Y
            int[,] sx = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] sy = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    float dx = 0, dy = 0;
                    for (int ky = -1; ky <= 1; ky++)
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            dx += gray[x + kx, y + ky] * sx[ky + 1, kx + 1];
                            dy += gray[x + kx, y + ky] * sy[ky + 1, kx + 1];
                        }

                    dx *= embossStrength;
                    dy *= embossStrength;

                    // Normalenvektor berechnen
                    float dz = 1.0f;
                    float len = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    float nx = dx / len;
                    float ny = dy / len;
                    float nz = dz / len;

                    // In RGB umwandeln
                    int r = (int)((nx * 0.5f + 0.5f) * 255);
                    int g = (int)((ny * 0.5f + 0.5f) * 255);
                    int b = (int)((nz * 0.5f + 0.5f) * 255);

                    normalMap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            // Rand auffüllen
            for (int x = 0; x < width; x++)
            {
                normalMap.SetPixel(x, 0, Color.FromArgb(128, 128, 255));
                normalMap.SetPixel(x, height - 1, Color.FromArgb(128, 128, 255));
            }
            for (int y = 0; y < height; y++)
            {
                normalMap.SetPixel(0, y, Color.FromArgb(128, 128, 255));
                normalMap.SetPixel(width - 1, y, Color.FromArgb(128, 128, 255));
            }

            return normalMap;
        }

        /// <summary>
        /// Generates a flat normal map (all normals pointing up).
        /// </summary>
        /// <param name="width">The width of the normal map.</param>
        /// <param name="height">The height of the normal map.</param>
        /// <returns>A bitmap representing the generated flat normal map.</returns>
        private Bitmap GenerateFlatNormal(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                // Ein flaches Normal-Map-Bild: RGB(128,128,255) = Vektor (0,0,1)
                gfx.Clear(Color.FromArgb(128, 128, 255));
            }
            return bmp;
        }

        /// <summary>
        /// Generates a metallic map from the given albedo bitmap.
        /// </summary>
        /// <param name="albedo">The source albedo bitmap.</param>
        /// <param name="effectStrength">The strength of the effect to apply.</param>
        /// <param name="invert">Whether to invert the resulting map.</param>
        /// <returns>A bitmap representing the generated metallic map.</returns>
        private Bitmap GenerateRoughnessMap(Bitmap albedo, float effectStrength, bool invert)
        {
            int width = albedo.Width;
            int height = albedo.Height;
            Bitmap rough = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color c = albedo.GetPixel(x, y);
                    int gray = (int)(((c.R + c.G + c.B) / 3) * effectStrength);
                    if (invert) gray = 255 - gray;
                    rough.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return rough;
        }

        /// <summary>
        /// Generates a metallic map from the given albedo bitmap.
        /// </summary>
        /// <param name="albedo">The source albedo bitmap.</param>
        /// <param name="threshold">The threshold for metallic effect.</param>
        /// <param name="intensity">The intensity of the metallic effect.</param>
        /// <returns>A bitmap representing the generated metallic map.</returns>
        private Bitmap GenerateMetallicMap(Bitmap albedo, int threshold, float intensity)
        {
            int width = albedo.Width;
            int height = albedo.Height;
            Bitmap metal = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color c = albedo.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int value = gray > threshold ? (int)(255 * intensity) : 0;
                    metal.SetPixel(x, y, Color.FromArgb(value, value, value));
                }
            return metal;
        }

        /// <summary>
        /// Generates an emission map from the given albedo bitmap.
        /// </summary>
        /// <param name="albedo">The source albedo bitmap.</param>
        /// <param name="strength">The strength of the emission effect.</param>
        /// <param name="emissionColor">The color of the emission as an array of three floats (RGB).</param>
        /// <returns>A bitmap representing the generated emission map.</returns>
        private Bitmap GenerateEmissionMap(Bitmap albedo, float strength, float[] emissionColor)
        {
            int width = albedo.Width;
            int height = albedo.Height;
            Bitmap emission = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color c = albedo.GetPixel(x, y);
                    int r = Math.Min(255, (int)(c.R * emissionColor[0] * strength));
                    int g = Math.Min(255, (int)(c.G * emissionColor[1] * strength));
                    int b = Math.Min(255, (int)(c.B * emissionColor[2] * strength));
                    emission.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            return emission;
        }


        /// <summary>
        /// Generates an alpha map from the given albedo bitmap.
        /// </summary>
        /// <param name="albedo">The source albedo bitmap.</param>
        /// <param name="strength">The strength of the alpha effect.</param>
        /// <returns>A bitmap representing the generated alpha map.</returns>
        private Bitmap GenerateAlphaMap(Bitmap albedo, float strength = 1.0f)
        {
            int width = albedo.Width;
            int height = albedo.Height;
            Bitmap alpha = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color c = albedo.GetPixel(x, y);
                    int a = 255; // oder z.B. (c.R + c.G + c.B) / 3 für Helligkeit
                    alpha.SetPixel(x, y, Color.FromArgb(a, c.R, c.G, c.B));
                }
            return alpha;
        }

        /// <summary>
        /// Generates an occlusion map from the given albedo bitmap.
        /// </summary>
        /// <param name="albedo">The source albedo bitmap.</param>
        /// <param name="strength">The strength of the occlusion effect.</param>
        /// <returns>A bitmap representing the generated occlusion map.</returns>
        private Bitmap GenerateOcclusionMap(Bitmap albedo, float strength = 1.0f)
        {
            int width = albedo.Width;
            int height = albedo.Height;
            Bitmap occ = new Bitmap(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color c = albedo.GetPixel(x, y);
                    int gray = 255 - (int)((c.R + c.G + c.B) / 3);
                    occ.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return occ;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The source image to resize.</param>
        /// <param name="width">The desired width of the resized image.</param>
        /// <param name="height">The desired height of the resized image.</param>
        /// <returns>A new Bitmap object with the specified dimensions.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Copies all relevant material images from the source directory to the target directory in a flat structure
        /// (ignoring subdirectories) and renaming files if necessary to avoid name conflicts.
        /// </summary>
        private void CopyAllImagesFlat(string sourceDir, string targetDir)
        {
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            this.UseWaitCursor = true;

            // Materialdateien wie in Autofill_from_color suchen und kopieren
            var materialEndings = new[] {
                "_albedo", "_base", "_color", "_col", "_diffuse", "_diff", "_basecol", "_basecolor", "_1K-JPG_Color", "_2K-JPG_Color",
                "_normal", "_norm", "_nrml", "_nrm", "_nor", "_n", "_normalmap", "_nm", "_Normals", "_NormalMap", "_nor", "_normals", "_normal_map", "_nmap", "_nml", "_nmlmap", "_nrmTex", "_nrmTexture", "_normal-ogl", "_1K-JPG_NormalGL", "_2K-JPG_NormalGL",
                "_ambient", "_occlusion", "_ao", "_ambientocclusion", "_Occ", "_Occlusion", "_AO", "_aoTex", "_aoTexture", "_ambient_occlusion", "_occlusionmap", "_occlusion_map", "_1K-JPG_AmbientOcclusion", "_2K-JPG_AmbientOcclusion",
                "_metallic", "_metalness", "_mtl", "_metal", "_Metal", "_Metallic", "_metalTex", "_metalTexture", "_metal_map", "_metallicmap", "_metallic_map", "_1K-JPG_Metallic", "_2K-JPG_Metallic",
                "_roughness", "_rough", "_roug", "_rgh", "_Rough", "_Roughness", "_roughTex", "_roughTexture", "_rough_map", "_roughnessmap", "_roughness_map", "_1K-JPG_Roughness", "_2K-JPG_Roughness",
                "_emission", "_emiss", "_emit", "_Emissive", "_Emiss", "_emissiveTex", "_emissiveTexture", "_emissive_map", "_emissionmap", "_emission_map", "_Glow", "_glow", "_illum", "_illumination", "_1K-JPG_Emission", "_2K-JPG_Emission",
                "_alpha", "_transparency", "_Opacity", "_opacity", "_mask", "_Mask", "_transTex", "_transTexture", "_alpha_map", "_alphamap", "_alphaMap", "_1K-JPG_Alpha", "_2K-JPG_Alpha",
                "_height", "_Height", "_disp", "_displacement", "_bump", "_bumpmap" +"_1K-JPG_Opacity", "_2K-JPG_Opacity"
            };

            var files = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories)
                .Where(f => {
                    string name = Path.GetFileNameWithoutExtension(f).ToLower();
                    string ext = Path.GetExtension(f).ToLower();
                    // Nur .png und .jpg
                    if (!(ext == ".png" || ext == ".jpg")) return false;
                    // Nur Materialdateien laut Endungen
                    return materialEndings.Any(ending => name.EndsWith(ending.ToLower()));
                });

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(targetDir, fileName);

                // Falls Datei schon existiert, umbenennen
                int count = 1;
                string nameOnly = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);
                while (File.Exists(destFile))
                {
                    destFile = Path.Combine(targetDir, $"{nameOnly}({count}){ext}");
                    count++;
                }

                File.Copy(file, destFile);
            }

            this.UseWaitCursor = false;
        }

        #endregion Material- und Bildverarbeitung

        #region PBR-Vorschau & Rendering

        /// <summary>
        /// Updates the PBR preview by rendering a sphere with the current material settings.
        /// </summary>
        private void UpdatePBRPreview()
        {
            try
            {
                // Nur aktualisieren wenn Form geladen ist
                if (!this.IsHandleCreated || this.IsDisposed) return;
                
                // 512x512 PBR-Vorschau auf Sphere rendern
                Bitmap preview = RenderPBRSphere(512, 512);
                
                // UI-Thread-sicher aktualisieren
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() => {
                        if (pictureBoxPBRPreview.Image != null)
                        {
                            pictureBoxPBRPreview.Image.Dispose();
                        }
                        pictureBoxPBRPreview.Image = preview;
                    }));
                }
                else
                {
                    if (pictureBoxPBRPreview.Image != null)
                    {
                        pictureBoxPBRPreview.Image.Dispose();
                    }
                    pictureBoxPBRPreview.Image = preview;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Aktualisieren der PBR-Vorschau: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Updates the PBR preview with a square aspect ratio by rendering a sphere with the current material settings.
        /// </summary>
        private void UpdatePBRPreviewSquare()
        {
            try
            {
                // Quadratische 400x400 PBR-Vorschau auf Sphere rendern
                var preview = RenderPBRSphere(400, 400); // Quadratisch statt 520x400
                
                if (preview != null)
                {
                    if (pictureBoxPBRPreview.InvokeRequired)
                    {
                        pictureBoxPBRPreview.Invoke(new Action(() =>
                        {
                            if (pictureBoxPBRPreview.Image != null)
                            {
                                pictureBoxPBRPreview.Image.Dispose();
                            }
                            pictureBoxPBRPreview.Image = preview;
                        }));
                    }
                    else
                    {
                        if (pictureBoxPBRPreview.Image != null)
                        {
                            pictureBoxPBRPreview.Image.Dispose();
                        }
                        pictureBoxPBRPreview.Image = preview;
                    }
                }
            }
            

            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Aktualisieren der quadratischen PBR-Vorschau: {ex.Message}");
            }
        }

        /// <summary>
        /// Renders a PBR sphere with the current material settings.
        /// </summary>
        private Bitmap RenderPBRSphere(int width, int height)
        {
            this.UseWaitCursor = true;

            Bitmap result = new Bitmap(width, height);
            
            using (Graphics g = Graphics.FromImage(result))
            {
                // Schwarzer Hintergrund für bessere PBR-Darstellung
                g.Clear(Color.Black);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Sphere-Parameter
                int centerX = width / 2;
                int centerY = height / 2;
                int radius = Math.Min(width, height) / 2 - 10;
                
                // Basis-Texturen laden
                Bitmap baseColorTexture = LoadTextureFromPictureBox(pictureBoxBaseColor);
                Bitmap normalTexture = LoadTextureFromPictureBox(pictureBoxNormal);
                Bitmap roughnessTexture = LoadTextureFromPictureBox(pictureBoxRoughness);
                Bitmap metallicTexture = LoadTextureFromPictureBox(pictureBoxMetallic);
                Bitmap occlusionTexture = LoadTextureFromPictureBox(pictureBoxOcclusion);
                Bitmap emissionTexture = LoadTextureFromPictureBox(pictureBoxEmission);
                Bitmap ormTexture = LoadTextureFromPictureBox(pictureBoxORM);
                
                // Mehrere Lichtquellen für besseren PBR-Effekt
                Vector3 mainLight = Vector3.Normalize(new Vector3(1.0f, 1.0f, 0.8f));
                Vector3 fillLight = Vector3.Normalize(new Vector3(-0.5f, 0.3f, 0.5f));
                Vector3 rimLight = Vector3.Normalize(new Vector3(0.0f, -0.8f, 0.6f));
                
                // Umgebungslicht
                float ambientStrength = 0.1f;
                
                // Sphere rendern mit sicherer SetPixel-Methode
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Prüfen ob Punkt in der Sphere ist
                        float dx = x - centerX;
                        float dy = y - centerY;
                        float distSq = dx * dx + dy * dy;
                        
                        if (distSq <= radius * radius)
                        {
                            // Berechne Sphere-Normale
                            float z = (float)Math.Sqrt(radius * radius - distSq);
                            Vector3 normal = Vector3.Normalize(new Vector3(dx / radius, dy / radius, z / radius));
                            
                            // UV-Koordinaten für Sphere mapping (verbessert)
                            float u = (float)(0.5 + Math.Atan2(normal.X, normal.Z) / (2 * Math.PI));
                            float v = (float)(0.5 - Math.Asin(normal.Y) / Math.PI);
                            
                            // Sample Texturen
                            Color baseColor = SampleTexture(baseColorTexture, u, v, Color.FromArgb(180, 180, 180));
                            Color roughness = SampleTexture(roughnessTexture, u, v, Color.FromArgb(128, 128, 128));
                            Color metallic = SampleTexture(metallicTexture, u, v, Color.Black);
                            Color emission = SampleTexture(emissionTexture, u, v, Color.Black);
                            Color occlusion = SampleTexture(occlusionTexture, u, v, Color.White);
                            
                            // Verbessertes PBR-Shading
                            Color finalColor = CalculateEnhancedPBRColor(
                                baseColor, normal, mainLight, fillLight, rimLight, 
                                roughness, metallic, emission, occlusion, ambientStrength);
                            
                            result.SetPixel(x, y, finalColor);
                        }
                    }
                }
                
                // Cleanup
                baseColorTexture?.Dispose();
                normalTexture?.Dispose();
                roughnessTexture?.Dispose();
                metallicTexture?.Dispose();
                occlusionTexture?.Dispose();
                emissionTexture?.Dispose();
                ormTexture?.Dispose();
            }

            this.UseWaitCursor = false;
            
            return result;
        }

        /// <summary>
        /// Samples a texture at given UV coordinates, returning a default color if the texture is null
        /// or coordinates are out of bounds.
        /// </summary>
        private Color SampleTexture(Bitmap texture, float u, float v, Color defaultColor)
        {
            if (texture == null) return defaultColor;
            
            int x = Math.Max(0, Math.Min(texture.Width - 1, (int)(u * texture.Width)));
            int y = Math.Max(0, Math.Min(texture.Height - 1, (int)(v * texture.Height)));
            
            return texture.GetPixel(x, y);
        }

        /// <summary>
        /// Calculates the final PBR color using multiple light sources and occlusion.
        /// </summary>
        private Color CalculatePBRColor(Color baseColor, Vector3 normal, Vector3 lightDir, Color roughness, Color metallic, Color emission)
        {
            // Einfaches Lambertsches Diffuse-Shading
            float NdotL = Math.Max(0, Vector3.Dot(normal, lightDir));
            
            // Metallicity
            float metallicValue = metallic.R / 255.0f;
            
            // Roughness
            float roughnessValue = roughness.R / 255.0f;
            
            // Diffuse-Komponente
            float diffuse = NdotL * (1.0f - metallicValue);
            
            // Einfache Specular-Komponente
            Vector3 viewDir = new Vector3(0, 0, 1); // Kamera schaut direkt auf Sphere
            Vector3 halfDir = Vector3.Normalize(lightDir + viewDir);
            float NdotH = Math.Max(0, Vector3.Dot(normal, halfDir));
            float specular = (float)Math.Pow(NdotH, (1.0f - roughnessValue) * 64.0f) * metallicValue;
            
            // Farbe berechnen
            float r = Math.Min(255, baseColor.R * diffuse + specular * 255 + emission.R);
            float g = Math.Min(255, baseColor.G * diffuse + specular * 255 + emission.G);
            float b = Math.Min(255, baseColor.B * diffuse + specular * 255 + emission.B);
            
            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        /// <summary>
        /// Calculates the final PBR color using multiple light sources, rim lighting, occlusion, and emission.
        /// </summary>
        private Color CalculateEnhancedPBRColor(Color baseColor, Vector3 normal, Vector3 mainLight, Vector3 fillLight, Vector3 rimLight, Color roughness, Color metallic, Color emission, Color occlusion, float ambientStrength)
        {
            // Material-Eigenschaften
            float metallicValue = metallic.R / 255.0f;
            float roughnessValue = Math.Max(0.04f, roughness.R / 255.0f); // Mindest-Roughness
            float occlusionValue = occlusion.R / 255.0f;
            
            // Fresnel-Effekt für realistischere Reflexionen
            Vector3 viewDir = new Vector3(0, 0, 1);
            float VdotN = Math.Max(0, Vector3.Dot(viewDir, normal));
            float fresnel = 1.0f - VdotN;
            fresnel = fresnel * fresnel * fresnel; // Kubischer Fresnel
            
            // Hauptlicht-Berechnung
            float mainNdotL = Math.Max(0, Vector3.Dot(normal, mainLight));
            Vector3 mainHalf = Vector3.Normalize(mainLight + viewDir);
            float mainNdotH = Math.Max(0, Vector3.Dot(normal, mainHalf));
            float mainSpecular = (float)Math.Pow(mainNdotH, (1.0f - roughnessValue) * 128.0f) * (1.0f + metallicValue * 2.0f);
            
            // Fülllicht
            float fillNdotL = Math.Max(0, Vector3.Dot(normal, fillLight)) * 0.3f;
            
            // Rim-Light für Kantenhighlights
            float rimNdotL = Math.Max(0, Vector3.Dot(normal, rimLight));
            float rimEffect = (float)Math.Pow(1.0f - VdotN, 3.0f) * rimNdotL * 0.5f;
            
            // Diffuse-Beleuchtung
            float totalDiffuse = (mainNdotL + fillNdotL + ambientStrength) * (1.0f - metallicValue * 0.8f);
            
            // Specular-Highlight mit Fresnel
            float totalSpecular = mainSpecular * (fresnel * 0.5f + 0.5f) + rimEffect;
            
            // Occlusion anwenden
            totalDiffuse *= occlusionValue;
            totalSpecular *= occlusionValue;
            
            // Endfarben-Berechnung
            float r = Math.Min(255, (baseColor.R * totalDiffuse + totalSpecular * 255 * (1.0f + metallicValue)) + emission.R * 2.0f);
            float g = Math.Min(255, (baseColor.G * totalDiffuse + totalSpecular * 255 * (1.0f + metallicValue)) + emission.G * 2.0f);
            float b = Math.Min(255, (baseColor.B * totalDiffuse + totalSpecular * 255 * (1.0f + metallicValue)) + emission.B * 2.0f);
            
            // Gamma-Korrektur für realistischere Darstellung
            r = (float)Math.Pow(r / 255.0f, 1.0f / 2.2f) * 255.0f;
            g = (float)Math.Pow(g / 255.0f, 1.0f / 2.2f) * 255.0f;
            b = (float)Math.Pow(b / 255.0f, 1.0f / 2.2f) * 255.0f;
            
            return Color.FromArgb(Math.Max(0, Math.Min(255, (int)r)), 
                                  Math.Max(0, Math.Min(255, (int)g)), 
                                  Math.Max(0, Math.Min(255, (int)b)));
        }

        /// <summary>
        /// Determines a contrasting color (black or white) based on the brightness of the input color
        /// </summary>
        private Color GetContrastColor(Color color)
        {
            // Berechne Helligkeit und wähle kontrastierende Textfarbe
            double brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
            return brightness > 128 ? Color.Black : Color.White;
        }

        #endregion PBR-Vorschau & Rendering

        #region Parameter- und UI-Updates

        /// <summary>
        /// Updates the UI controls based on the current material configuration.
        /// </summary>
        private void UpdateUIFromMaterialConfig()
        {
            // Normal Strength: 0.0-2.0 -> TrackBar 0-200
            if (materialConfig.NormalStrength != null)
            {
                trackBarNormalStrength.Value = (int)(materialConfig.NormalStrength * 100);
                textBoxNormalStrength.Text = materialConfig.NormalStrength.ToString("0.0");
            }

            // Roughness Strength: 0.0-2.0 -> TrackBar 0-200
            if (materialConfig.RoughnessStrength != null)
            {
                trackBarRoughnessStrength.Value = (int)(materialConfig.RoughnessStrength * 100);
                textBoxRoughnessStrength.Text = materialConfig.RoughnessStrength.ToString("0.0");
            }

            // Occlusion Strength: 0.0-2.0 -> TrackBar 0-200
            if (materialConfig.OcclusionStrength != null)
            {
                trackBarOcclusionStrength.Value = (int)(materialConfig.OcclusionStrength * 100);
                textBoxOcclusionStrength.Text = materialConfig.OcclusionStrength.ToString("0.0");
            }

            // Metallic Intensity (entspricht MetallicStrength)
            if (materialConfig.MetallicIntensity != null)
            {
                trackBarMetallicStrength.Value = (int)(materialConfig.MetallicIntensity * 100);
                textBoxMetallicStrength.Text = materialConfig.MetallicIntensity.ToString("0.0");
            }

            // Metallic Threshold: 0-255 -> TrackBar 0-255
            if (materialConfig.MetallicThreshold != null)
            {
                trackBarMetallicThreshold.Value = (int)materialConfig.MetallicThreshold;
                textBoxMetallicThreshold.Text = materialConfig.MetallicThreshold.ToString();
            }

            // Emission Strength: 0.0-2.0 -> TrackBar 0-200
            if (materialConfig.EmissionStrength != null)
            {
                trackBarEmissionStrength.Value = (int)(materialConfig.EmissionStrength * 100);
                textBoxEmissionStrength.Text = materialConfig.EmissionStrength.ToString("0.0");
            }

            // Alpha Strength: 0.0-2.0 -> TrackBar 0-200
            if (materialConfig.AlphaStrength != null)
            {
                trackBarAlphaStrength.Value = (int)(materialConfig.AlphaStrength * 100);
                textBoxAlphaStrength.Text = materialConfig.AlphaStrength.ToString("0.0");
            }

            // Normal Flip Y (Boolean) - RoughnessInvert ist hier etwas verwirrend benannt
            if (materialConfig.RoughnessInvert != null)
                textBoxNormalFlipY.Text = (bool)materialConfig.RoughnessInvert ? "1" : "0";

            // Color Picker Button-Farben aktualisieren
            if (materialConfig.BaseColorTint != null && materialConfig.BaseColorTint.Count >= 3)
            {
                int r = (int)(materialConfig.BaseColorTint[0] * 255);
                int g = (int)(materialConfig.BaseColorTint[1] * 255);
                int b = (int)(materialConfig.BaseColorTint[2] * 255);
                Color baseColor = Color.FromArgb(r, g, b);
                buttonBaseColorTint.BackColor = baseColor;
                buttonBaseColorTint.ForeColor = GetContrastColor(baseColor);
            }

            if (materialConfig.EmissionColor != null && materialConfig.EmissionColor.Count >= 3)
            {
                int r = (int)(materialConfig.EmissionColor[0] * 255);
                int g = (int)(materialConfig.EmissionColor[1] * 255);
                int b = (int)(materialConfig.EmissionColor[2] * 255);
                Color emissionColor = Color.FromArgb(r, g, b);
                buttonEmissionColor.BackColor = emissionColor;
                buttonEmissionColor.ForeColor = GetContrastColor(emissionColor);
            }

            // Sicherstellen, dass alle Controls sichtbar sind
            foreach (Control control in panelControls.Controls)
            {
                control.Visible = true;
            }
        }

        /// <summary>
        /// Updates a specific property in the material configuration object.
        /// </summary>
        private void UpdateMaterialConfigValue(string propertyName, object value)
        {
            try
            {
                if (materialConfig != null)
                {
                    // Update the property using reflection or dynamic assignment
                    var configType = materialConfig.GetType();
                    var property = configType.GetProperty(propertyName);
                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(materialConfig, value);
                    }
                    else
                    {
                        // For dynamic objects, use indexer
                        ((dynamic)materialConfig)[propertyName] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't stop the UI update
                Debug.WriteLine($"Error updating material config property {propertyName}: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates the preview material in real-time as parameters change.
        /// </summary>
        private void UpdatePreviewInRealTime()
        {
            // Throttle updates to prevent too frequent refreshes
            if (previewUpdateTimer != null)
            {
                previewUpdateTimer.Stop();
            }
            
            previewUpdateTimer = new System.Windows.Forms.Timer();
            previewUpdateTimer.Interval = 100; // 100ms delay
            previewUpdateTimer.Tick += (s, e) =>
            {
                previewUpdateTimer.Stop();
                previewUpdateTimer.Dispose();
                previewUpdateTimer = null;
                
                // Trigger preview update
                try
                {
                    GeneratePreviewMaterial();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error updating preview: {ex.Message}");
                }
            };
            previewUpdateTimer.Start();
        }

        /// <summary>
        /// Generates the preview material for real-time updates.
        /// </summary>
        private void GeneratePreviewMaterial()
        {
            // Use the new square PBR preview method for real-time updates
            UpdatePBRPreviewSquare();
        }

        /// <summary>
        /// Updates the parameter TextBoxes based on the current TrackBar values.
        /// </summary>
        private void UpdateParameterTextBoxes()
        {
            try
            {
                // Base Color Parameter
                if (trackBarBaseColorStrength != null && textBoxBaseColorStrength != null)
                    textBoxBaseColorStrength.Text = (trackBarBaseColorStrength.Value / 100.0f).ToString("F2");
                if (trackBarContrast != null && textBoxContrast != null)
                    textBoxContrast.Text = (trackBarContrast.Value / 100.0f).ToString("F2");
                if (trackBarBrightness != null && textBoxBrightness != null)
                    textBoxBrightness.Text = (trackBarBrightness.Value / 100.0f).ToString("F2");

                // Metallic Parameter
                if (trackBarMetallicStrength != null && textBoxMetallicStrength != null)
                    textBoxMetallicStrength.Text = (trackBarMetallicStrength.Value / 100.0f).ToString("F2");
                if (trackBarMetallicThreshold != null && textBoxMetallicThreshold != null)
                    textBoxMetallicThreshold.Text = trackBarMetallicThreshold.Value.ToString();

                // Roughness Parameter
                if (trackBarRoughnessStrength != null && textBoxRoughnessStrength != null)
                    textBoxRoughnessStrength.Text = (trackBarRoughnessStrength.Value / 100.0f).ToString("F2");

                // Normal Map Parameter
                if (trackBarNormalStrength != null && textBoxNormalStrength != null)
                    textBoxNormalStrength.Text = (trackBarNormalStrength.Value / 100.0f).ToString("F2");
                if (trackBarNormalFlipY != null && textBoxNormalFlipY != null)
                    textBoxNormalFlipY.Text = trackBarNormalFlipY.Value.ToString();

                // Ambient Occlusion Parameter
                if (trackBarOcclusionStrength != null && textBoxOcclusionStrength != null)
                    textBoxOcclusionStrength.Text = (trackBarOcclusionStrength.Value / 100.0f).ToString("F2");

                // Emission Parameter
                if (trackBarEmissionStrength != null && textBoxEmissionStrength != null)
                    textBoxEmissionStrength.Text = (trackBarEmissionStrength.Value / 100.0f).ToString("F2");
                if (trackBarEmissionEdgeEnhance != null && textBoxEmissionEdgeEnhance != null)
                    textBoxEmissionEdgeEnhance.Text = (trackBarEmissionEdgeEnhance.Value / 100.0f).ToString("F2");
                if (trackBarEmissionEdgeStrength != null && textBoxEmissionEdgeStrength != null)
                    textBoxEmissionEdgeStrength.Text = (trackBarEmissionEdgeStrength.Value / 100.0f).ToString("F2");

                // Alpha Parameter
                if (trackBarAlphaStrength != null && textBoxAlphaStrength != null)
                    textBoxAlphaStrength.Text = (trackBarAlphaStrength.Value / 100.0f).ToString("F2");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Aktualisieren der Parameter-TextBoxes: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the float value from a TrackBar, applying necessary scaling.
        /// </summary>
        private float GetTrackBarValue(TrackBar trackBar, float defaultValue)
        {
            try
            {
                if (trackBar != null)
                {
                    // Für die meisten TrackBars: Wert durch 100 teilen (0-200 -> 0.0-2.0)
                    if (trackBar == trackBarMetallicThreshold)
                    {
                        return trackBar.Value; // Threshold ist 0-255, nicht durch 100 teilen
                    }
                    else
                    {
                        return trackBar.Value / 100.0f;
                    }
                }
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Loads material presets from a JSON configuration file and populates the ComboBox.
        /// </summary>
        private void LoadMaterialPresets()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "material.json");
            if (!File.Exists(jsonPath))
                return;

            string json = File.ReadAllText(jsonPath);
            dynamic config = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            comboBoxMaterialSelect.Items.Clear();
            if (config.Materials != null)
            {
                foreach (var mat in config.Materials)
                {
                    comboBoxMaterialSelect.Items.Add((string)mat.MaterialName);
                }
                
                // StandardPBR automatisch auswählen falls vorhanden
                if (comboBoxMaterialSelect.Items.Count > 0)
                {
                    int standardIdx = comboBoxMaterialSelect.Items.IndexOf("StandardPBR");
                    if (standardIdx >= 0)
                    {
                        comboBoxMaterialSelect.SelectedIndex = standardIdx;
                    }
                    else
                    {
                        comboBoxMaterialSelect.SelectedIndex = 0;
                    }
                    
                    // ComboBox-Text explizit setzen
                    comboBoxMaterialSelect.Text = comboBoxMaterialSelect.SelectedItem?.ToString();
                }
            }
        }

        #endregion Parameter- und UI-Updates

        #region Hilfsfunktionen & Utility

        /// <summary>
        /// Returns the tooltip text based on the current UI culture.
        /// </summary>
        private string GetTooltip(string en, string de, string fr, string es)
        {
            var culture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            switch (culture)
            {
                case "de": return de;
                case "fr": return fr;
                case "es": return es;
                default: return en;
            }
        }

        /// <summary>
        /// Simple 3D Vector struct for basic vector operations.
        /// </summary>
        public struct Vector3
        {
            public float X, Y, Z;
            
            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            
            public static Vector3 Normalize(Vector3 v)
            {
                float length = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
                if (length > 0)
                {
                    return new Vector3(v.X / length, v.Y / length, v.Z / length);
                }
                return new Vector3(0, 0, 1);
            }
            
            public static float Dot(Vector3 a, Vector3 b)
            {
                return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
            }
            
            public static Vector3 operator +(Vector3 a, Vector3 b)
            {
                return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
            }
        }


        #endregion Hilfsfunktionen & Utility

        #region Event Handler
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://aiaicapta.in/gltf-packer/");
            Process.Start(sInfo);
        }

        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
            picBox.ImageLocation = null;
            buttonSave.Enabled = pictureBoxBaseColor.ImageLocation != null;
        }

        private void Ensure_width()
        {
            // Neue 3-Spalten Layout Größenbeschränkungen Fenstergrößen
            // Mindestbreite = 1000, Mindesthöhe = 750
            this.MinimumSize = new Size(940, 975); // 
            this.MaximumSize = new Size(940, 975); // Unterstützt große Monitore
        }


        private Bitmap GenerateSolidColor(byte r, byte g, byte b, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.FromArgb(r, g, b));
            }
            return bmp;
        }

        private Bitmap GenerateBlack(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.Black);
            }
            return bmp;
        }

        private Bitmap GenerateNormalMapFromBaseColor(Bitmap baseColor)
        {
            int width = baseColor.Width;
            int height = baseColor.Height;
            Bitmap result = new Bitmap(width, height);
            
            // Einfache Normal Map Generierung basierend auf Luminanz-Gradienten
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Hole benachbarte Pixel für Gradient-Berechnung
                    Color center = baseColor.GetPixel(x, y);
                    Color right = baseColor.GetPixel(Math.Min(x + 1, width - 1), y);
                    Color bottom = baseColor.GetPixel(x, Math.Min(y + 1, height - 1));
                    
                    // Berechne Luminanz
                    float centerLum = (center.R * 0.299f + center.G * 0.587f + center.B * 0.114f) / 255.0f;
                    float rightLum = (right.R * 0.299f + right.G * 0.587f + right.B * 0.114f) / 255.0f;
                    float bottomLum = (bottom.R * 0.299f + bottom.G * 0.587f + bottom.B * 0.114f) / 255.0f;
                    
                    // Berechne Normale aus Gradienten
                    float dx = (rightLum - centerLum) * 2.0f; // Gradient X
                    float dy = (bottomLum - centerLum) * 2.0f; // Gradient Y
                    float dz = 1.0f; // Z-Komponente
                    
                    // Normalisiere
                    float length = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    if (length > 0)
                    {
                        dx /= length;
                        dy /= length;
                        dz /= length;
                    }
                    
                    // Konvertiere zu RGB (Normal Map Format: X, Y, Z -> R, G, B)
                    int r = (int)((dx * 0.5f + 0.5f) * 255);
                    int g = (int)((dy * 0.5f + 0.5f) * 255);
                    int b = (int)((dz * 0.5f + 0.5f) * 255);
                    
                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            
            return result;
        }

        /// <summary>
        /// Überladene Version der Normal Map Generierung mit Strength-Parameter
        /// </summary>
        private Bitmap GenerateNormalMapFromBaseColor(Bitmap baseColor, float strength)
        {
            int width = baseColor.Width;
            int height = baseColor.Height;
            Bitmap result = new Bitmap(width, height);
            
            // Normale Normal Map Generierung basierend auf Luminanz-Gradienten mit Stärke-Faktor
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Hole benachbarte Pixel für Gradient-Berechnung
                    Color center = baseColor.GetPixel(x, y);
                    Color right = baseColor.GetPixel(Math.Min(x + 1, width - 1), y);
                    Color bottom = baseColor.GetPixel(x, Math.Min(y + 1, height - 1));
                    
                    // Berechne Luminanz
                    float centerLum = (center.R * 0.299f + center.G * 0.587f + center.B * 0.114f) / 255.0f;
                    float rightLum = (right.R * 0.299f + right.G * 0.587f + right.B * 0.114f) / 255.0f;
                    float bottomLum = (bottom.R * 0.299f + bottom.G * 0.587f + bottom.B * 0.114f) / 255.0f;
                    
                    // Berechne Gradienten mit Stärke-Faktor
                    float dx = (rightLum - centerLum) * strength;
                    float dy = (bottomLum - centerLum) * strength;
                    float dz = 1.0f; // Standard Z-Komponente
                    
                    // Normalisiere den Vektor
                    float length = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    if (length > 0)
                    {
                        dx /= length;
                        dy /= length;
                        dz /= length;
                    }
                    
                    // Konvertiere zu RGB (Normal Map Format: X, Y, Z -> R, G, B)
                    int r = (int)((dx * 0.5f + 0.5f) * 255);
                    int g = (int)((dy * 0.5f + 0.5f) * 255);
                    int b = (int)((dz * 0.5f + 0.5f) * 255);
                    
                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            
            return result;
        }

        /// <summary>
        /// Wendet einen Farb-Tint auf das gegebene Bitmap an.
        /// </summary>
        /// <param name="bitmap">Das zu bearbeitende Bitmap.</param>
        /// <param name="tint">Ein Array mit drei float-Werten für R, G, B (z.B. {1.0f, 1.0f, 1.0f}).</param>

        private void ComboBoxMaterialSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = comboBoxMaterialSelect.SelectedIndex;
            if (idx >= 0 && materialConfig.Materials != null)
            {
                var selectedMat = materialConfig.Materials[idx];
                materialConfig.NormalStrength = selectedMat.NormalStrength;
                materialConfig.RoughnessStrength = selectedMat.RoughnessStrength;
                materialConfig.OcclusionStrength = selectedMat.OcclusionStrength;
                materialConfig.MetallicThreshold = selectedMat.MetallicThreshold;
                materialConfig.EmissionStrength = selectedMat.EmissionStrength;
                materialConfig.AlphaStrength = selectedMat.AlphaStrength;
                materialConfig.BaseColorTint = selectedMat.BaseColorTint;
                materialConfig.NormalMapType = selectedMat.NormalMapType;
                materialConfig.RoughnessInvert = selectedMat.RoughnessInvert;
                materialConfig.MetallicIntensity = selectedMat.MetallicIntensity;
                materialConfig.EmissionColor = selectedMat.EmissionColor;
                materialConfig.AlphaMode = selectedMat.AlphaMode;
                
                // UI-Elemente entsprechend den Preset-Werten aktualisieren
                UpdateUIFromMaterialConfig();
                
                // PBR-Vorschau sofort aktualisieren
                UpdatePBRPreview();
            }
        }

        private void ButtonGenerateMaps_Click(object sender, EventArgs e)
        {
            GenerateMissingMaps();
        }

        private void GenerateMissingMaps()
        {
            this.UseWaitCursor = true;
            try
            {
                // Prüfe welche Basis-Textur vorhanden ist oder lade Fallback
                Bitmap baseTexture = LoadTextureFromPictureBox(pictureBoxBaseColor);
                
                if (baseTexture == null)
                {
                    // Versuche Fallback-Textur zu laden
                    baseTexture = LoadFallbackTexture();
                    
                    if (baseTexture == null)
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Keine Base Color Textur vorhanden und Fallback-Textur (fallback.jpg) nicht gefunden!\n\n" +
                                        "Bitte laden Sie eine Base Color Textur oder stellen Sie sicher, dass 'fallback.jpg' im Resources-Verzeichnis vorhanden ist.", 
                                        "Keine Basis-Textur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Setze Fallback-Textur als Base Color
                    pictureBoxBaseColor.Image = (Bitmap)baseTexture.Clone();
                    Debug.WriteLine("Fallback-Textur 'fallback.jpg' wurde als Base Color geladen.");
                }

                int texWidth = baseTexture.Width;
                int texHeight = baseTexture.Height;

                // Aktuelle Parameter aus UI-Controls lesen
                float normalStrength = GetTrackBarValue(trackBarNormalStrength, 0.20f);
                float roughnessStrength = GetTrackBarValue(trackBarRoughnessStrength, 0.20f);
                float occlusionStrength = GetTrackBarValue(trackBarOcclusionStrength, 1.0f);
                float metallicThreshold = GetTrackBarValue(trackBarMetallicThreshold, 200f);
                float metallicIntensity = GetTrackBarValue(trackBarMetallicStrength, 1.0f);
                float alphaStrength = GetTrackBarValue(trackBarAlphaStrength, 1.0f);
                bool roughnessInvert = false; // TODO: Aus UI lesen wenn verfügbar

                // Generiere fehlende oder gelöschte Normal Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxNormal))
                {
                    Bitmap normalMap = GenerateNormalMapFromBaseColor(baseTexture, normalStrength);
                    pictureBoxNormal.Image = normalMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Normal Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Roughness Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxRoughness))
                {
                    Bitmap roughnessMap = GenerateRoughnessMap(baseTexture, roughnessStrength, roughnessInvert);
                    pictureBoxRoughness.Image = roughnessMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Roughness Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Metallic Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxMetallic))
                {
                    Bitmap metallicMap = GenerateMetallicMap(baseTexture, (int)metallicThreshold, metallicIntensity);
                    pictureBoxMetallic.Image = metallicMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Metallic Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Occlusion Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxOcclusion))
                {
                    Bitmap occlusionMap = GenerateOcclusionMap(baseTexture, occlusionStrength);
                    pictureBoxOcclusion.Image = occlusionMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Occlusion Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Alpha Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxAlpha))
                {
                    Bitmap alphaMap = GenerateAlphaMap(baseTexture, alphaStrength);
                    pictureBoxAlpha.Image = alphaMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Alpha Map wurde generiert/regeneriert.");
                }

                // Finale Vorschau-Aktualisierung
                UpdatePBRPreviewSquare();

                // Aktualisiere auch die TextBox-Werte für die aktuellen Parameter
                UpdateParameterTextBoxes();

                this.UseWaitCursor = false;
                MessageBox.Show("Fehlende Maps wurden erfolgreich generiert!", "Maps generiert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baseTexture?.Dispose();
            }
            catch (Exception ex)
            {
                this.UseWaitCursor = false;
                MessageBox.Show($"Fehler beim Generieren der Maps: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private void BatchButtonGenerateMaps_Click(object sender, EventArgs e)
        {
            // Verzeichnis und Suffixe für BaseColor-Texturen bestimmen
            if (pictureBoxBaseColor.ImageLocation == null)
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Base Color Required");
                return;
            }

            this.UseWaitCursor = true;

            string baseDir = Path.GetDirectoryName(pictureBoxBaseColor.ImageLocation);
            string[] baseColorSuffixes = new string[] { "_albedo", "_base", "_color", "_col", "_diffuse", "_diff", "_basecol", "_basecolor" };
            string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".tif", ".tiff", ".tga", ".exr", ".hdr" };

            // Alle Bilddateien im Verzeichnis sammeln
            var allFiles = Directory.GetFiles(baseDir)
                .Where(f => imageExtensions.Contains(Path.GetExtension(f).ToLower()))
                .ToArray();

            // Alle BaseColor-Texturen finden
            var baseColorFiles = allFiles.Where(f =>
                baseColorSuffixes.Any(suf => Path.GetFileNameWithoutExtension(f).ToLower().EndsWith(suf))
            ).ToList();
            if (baseColorFiles.Count == 0)
            {
                this.UseWaitCursor = false;
                MessageBox.Show("Keine BaseColor-Texturen im Verzeichnis gefunden.");
                return;
            }

            // Aktuelle Parameter aus UI-Controls lesen
            float normalStrength = GetTrackBarValue(trackBarNormalStrength, 0.20f);
            float roughnessStrength = GetTrackBarValue(trackBarRoughnessStrength, 0.20f);
            float occlusionStrength = GetTrackBarValue(trackBarOcclusionStrength, 1.0f);
            float metallicThreshold = GetTrackBarValue(trackBarMetallicThreshold, 200f);
            float metallicIntensity = GetTrackBarValue(trackBarMetallicStrength, 1.0f);
            float alphaStrength = GetTrackBarValue(trackBarAlphaStrength, 1.0f);
            bool roughnessInvert = false; // TODO: Aus UI lesen wenn verfügbar

            foreach (var baseColorFile in baseColorFiles)
            {
                string matName = Path.GetFileNameWithoutExtension(baseColorFile);
                foreach (var suf in baseColorSuffixes)
                {
                    if (matName.ToLower().EndsWith(suf))
                    {
                        matName = matName.Substring(0, matName.Length - suf.Length);
                        break;
                    }
                }
                string dir = Path.GetDirectoryName(baseColorFile);
                string ext = Path.GetExtension(baseColorFile);

                // Map-Suffixe
                var mapSuffixes = new (string Suffix, Func<Bitmap, Bitmap> Generator, string Description)[] {
                    ("_normal", (bmp) => GenerateNormalMapFromBaseColor(bmp, normalStrength), "Normal Map"),
                    ("_rough", (bmp) => GenerateRoughnessMap(bmp, roughnessStrength, roughnessInvert), "Roughness Map"),
                    ("_metal", (bmp) => GenerateMetallicMap(bmp, (int)metallicThreshold, metallicIntensity), "Metallic Map"),
                    ("_occ", (bmp) => GenerateOcclusionMap(bmp, occlusionStrength), "Occlusion Map"),
                    ("_alpha", (bmp) => GenerateAlphaMap(bmp, alphaStrength), "Alpha Map")
                };

                Bitmap baseTexture = null;
                try { baseTexture = new Bitmap(baseColorFile); } catch { continue; }

                foreach (var (suffix, generator, desc) in mapSuffixes)
                {
                    string mapPath = Path.Combine(dir, matName + suffix + ".png");
                    if (!File.Exists(mapPath))
                    {
                        try
                        {
                            using (var mapBmp = generator(baseTexture))
                            {
                                mapBmp.Save(mapPath, System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Fehler beim Generieren von {desc} für {matName}: {ex.Message}");
                        }
                    }
                }
                // ORM-Map generieren, wenn occ, rough, metal vorhanden
                try
                {
                    string occPath = Path.Combine(dir, matName + "_occ.png");
                    string roughPath = Path.Combine(dir, matName + "_rough.png");
                    string metalPath = Path.Combine(dir, matName + "_metal.png");
                    string ormPath = Path.Combine(dir, matName + "_orm.png");
                    if (File.Exists(occPath) && File.Exists(roughPath) && File.Exists(metalPath) && !File.Exists(ormPath))
                    {
                        using (var occ = new Bitmap(occPath))
                        using (var rough = new Bitmap(roughPath))
                        using (var metal = new Bitmap(metalPath))
                        {
                            Bitmap orm = GenerateORMMap(occ, rough, metal);
                            orm.Save(ormPath, System.Drawing.Imaging.ImageFormat.Png);
                            orm.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Generieren der ORM-Map für {matName}: {ex.Message}");
                }
                baseTexture.Dispose();
                // ORM-Map generieren, wenn occ, rough, metal vorhanden
                try
                {
                    string occPath = Path.Combine(dir, matName + "_occ.png");
                    string roughPath = Path.Combine(dir, matName + "_rough.png");
                    string metalPath = Path.Combine(dir, matName + "_metal.png");
                    string ormPath = Path.Combine(dir, matName + "_orm.png");
                    if (File.Exists(occPath) && File.Exists(roughPath) && File.Exists(metalPath) && !File.Exists(ormPath))
                    {
                        using (var occ = new Bitmap(occPath))
                        using (var rough = new Bitmap(roughPath))
                        using (var metal = new Bitmap(metalPath))
                        {
                            Bitmap orm = GenerateORMMap(occ, rough, metal);
                            orm.Save(ormPath, System.Drawing.Imaging.ImageFormat.Png);
                            orm.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Generieren der ORM-Map für {matName}: {ex.Message}");
                }
            }
            
            this.UseWaitCursor = false;
            MessageBox.Show("Batch-Generierung der fehlenden Maps abgeschlossen!", "Fertig", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BatchGenerateMissingMaps()
        {
            // Dies soll alle Materialien im ausgewähltem Verzeichnis verarbeiten.
            // Wird die erste Base Color Textur gefunden wird das ganze Verzeichnis verarbeitet.
            // Fehlende Maps werden generiert und gespeichert.

            this.UseWaitCursor = true;

            try
            {
                // Prüfe welche Basis-Textur vorhanden ist oder lade Fallback
                Bitmap baseTexture = LoadTextureFromPictureBox(pictureBoxBaseColor);
                
                if (baseTexture == null)
                {
                    // Versuche Fallback-Textur zu laden
                    baseTexture = LoadFallbackTexture();
                    
                    if (baseTexture == null)
                    {
                        this.UseWaitCursor = false;
                        MessageBox.Show("Keine Base Color Textur vorhanden und Fallback-Textur (fallback.jpg) nicht gefunden!\n\n" +
                                        "Bitte laden Sie eine Base Color Textur oder stellen Sie sicher, dass 'fallback.jpg' im Resources-Verzeichnis vorhanden ist.", 
                                        "Keine Basis-Textur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Setze Fallback-Textur als Base Color
                    pictureBoxBaseColor.Image = (Bitmap)baseTexture.Clone();
                    Debug.WriteLine("Fallback-Textur 'fallback.jpg' wurde als Base Color geladen.");
                }

                int texWidth = baseTexture.Width;
                int texHeight = baseTexture.Height;

                // Aktuelle Parameter aus UI-Controls lesen
                float normalStrength = GetTrackBarValue(trackBarNormalStrength, 0.20f);
                float roughnessStrength = GetTrackBarValue(trackBarRoughnessStrength, 0.20f);
                float occlusionStrength = GetTrackBarValue(trackBarOcclusionStrength, 1.0f);
                float metallicThreshold = GetTrackBarValue(trackBarMetallicThreshold, 200f);
                float metallicIntensity = GetTrackBarValue(trackBarMetallicStrength, 1.0f);
                float alphaStrength = GetTrackBarValue(trackBarAlphaStrength, 1.0f);
                bool roughnessInvert = false; // TODO: Aus UI lesen wenn verfügbar

                // Generiere fehlende oder gelöschte Normal Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxNormal))
                {
                    Bitmap normalMap = GenerateNormalMapFromBaseColor(baseTexture, normalStrength);
                    pictureBoxNormal.Image = normalMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Normal Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Roughness Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxRoughness))
                {
                    Bitmap roughnessMap = GenerateRoughnessMap(baseTexture, roughnessStrength, roughnessInvert);
                    pictureBoxRoughness.Image = roughnessMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Roughness Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Metallic Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxMetallic))
                {
                    Bitmap metallicMap = GenerateMetallicMap(baseTexture, (int)metallicThreshold, metallicIntensity);
                    pictureBoxMetallic.Image = metallicMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Metallic Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Occlusion Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxOcclusion))
                {
                    Bitmap occlusionMap = GenerateOcclusionMap(baseTexture, occlusionStrength);
                    pictureBoxOcclusion.Image = occlusionMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Occlusion Map wurde generiert/regeneriert.");
                }

                // Generiere fehlende oder gelöschte Alpha Map mit aktuellen Parametern
                if (IsTextureEmpty(pictureBoxAlpha))
                {
                    Bitmap alphaMap = GenerateAlphaMap(baseTexture, alphaStrength);
                    pictureBoxAlpha.Image = alphaMap;
                    UpdatePBRPreviewSquare();
                    Debug.WriteLine("Alpha Map wurde generiert/regeneriert.");
                }

                // Finale Vorschau-Aktualisierung
                UpdatePBRPreviewSquare();

                // Aktualisiere auch die TextBox-Werte für die aktuellen Parameter
                UpdateParameterTextBoxes();

                this.UseWaitCursor = false;
                MessageBox.Show("Fehlende Maps wurden erfolgreich generiert!", "Maps generiert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baseTexture?.Dispose();
            }
            catch (Exception ex)
            {
                this.UseWaitCursor = false;
                MessageBox.Show($"Fehler beim Generieren der Maps: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.UseWaitCursor = false;
        }

        private Bitmap LoadFallbackTexture()
        {
            try
            {
                // Versuche verschiedene mögliche Pfade für fallback.jpg
                string[] possiblePaths = {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "fallback.jpg"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fallback.jpg"),
                    Path.Combine(Environment.CurrentDirectory, "Resources", "fallback.jpg"),
                    Path.Combine(Environment.CurrentDirectory, "fallback.jpg"),
                    "Resources/fallback.jpg",
                    "fallback.jpg"
                };

                foreach (string path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        Debug.WriteLine($"Fallback-Textur gefunden: {path}");
                        return new Bitmap(path);
                    }
                }

                Debug.WriteLine("Fallback-Textur (fallback.jpg) nicht gefunden in keinem der möglichen Pfade.");
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Laden der Fallback-Textur: {ex.Message}");
                return null;
            }
        }

        private bool IsTextureEmpty(PictureBox pictureBox)
        {
            return pictureBox.Image == null && string.IsNullOrEmpty(pictureBox.ImageLocation);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Keine manuelle Positionierung mehr!
            // Das Layout wird durch das Panel und die Designer-Einstellungen gesteuert.
        }
 
        private void PictureBoxPBRPreview_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PBR Vorschau: Manuelle Aktualisierung ausgelöst");
                
                // Sofortige manuelle Aktualisierung der PBR-Vorschau
                UpdatePBRPreviewSquare();
                
                // Feedback für den Benutzer (optional)
                pictureBoxPBRPreview.BorderStyle = BorderStyle.Fixed3D;
                
                // Timer für visuelles Feedback (Rahmen kurz anzeigen)
                var feedbackTimer = new System.Windows.Forms.Timer();
                feedbackTimer.Interval = 200; // 200ms
                feedbackTimer.Tick += (s, ev) =>
                {
                    pictureBoxPBRPreview.BorderStyle = BorderStyle.None;
                    feedbackTimer.Stop();
                    feedbackTimer.Dispose();
                };
                feedbackTimer.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler bei manueller PBR-Vorschau-Aktualisierung: {ex.Message}");
            }
        }

        private Bitmap LoadTextureFromPictureBox(PictureBox pictureBox)
        {
            try
            {
                if (pictureBox?.Image != null)
                {
                    return new Bitmap(pictureBox.Image);
                }
                // Wenn aus ImageLocation laden
                else if (!string.IsNullOrEmpty(pictureBox?.ImageLocation) && File.Exists(pictureBox.ImageLocation))
                {
                    return new Bitmap(pictureBox.ImageLocation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Laden der Textur aus PictureBox: {ex.Message}");
            }
            return null;
        }

        private void ButtonBaseColorTint_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Aktuelle Farbe aus materialConfig setzen
                if (materialConfig.BaseColorTint != null && materialConfig.BaseColorTint.Count >= 3)
                {
                    int r = (int)(materialConfig.BaseColorTint[0] * 255);
                    int g = (int)(materialConfig.BaseColorTint[1] * 255);
                    int b = (int)(materialConfig.BaseColorTint[2] * 255);
                    colorDialog.Color = Color.FromArgb(r, g, b);
                }
                else
                {
                    colorDialog.Color = Color.White;
                }

                colorDialog.FullOpen = true;
                
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Farbe in materialConfig speichern (0.0-1.0 Bereich)
                    materialConfig.BaseColorTint = new JArray(
    colorDialog.Color.R / 255.0f,
    colorDialog.Color.G / 255.0f,
    colorDialog.Color.B / 255.0f
);
                    
                    // Button-Farbe anpassen
                    buttonBaseColorTint.BackColor = colorDialog.Color;
                    buttonBaseColorTint.ForeColor = GetContrastColor(colorDialog.Color);
                    
                    // PBR-Vorschau aktualisieren
                    UpdatePBRPreview();
                }
            }
        }

        private void ButtonEmissionColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Aktuelle Farbe aus materialConfig setzen
                if (materialConfig.EmissionColor != null && materialConfig.EmissionColor.Count >= 3)
                {
                    int r = (int)(materialConfig.EmissionColor[0] * 255);
                    int g = (int)(materialConfig.EmissionColor[1] * 255);
                    int b = (int)(materialConfig.EmissionColor[2] * 255);
                    colorDialog.Color = Color.FromArgb(r, g, b);
                }
                else
                {
                    colorDialog.Color = Color.White;
                }

                colorDialog.FullOpen = true;
                
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Farbe in materialConfig speichern (0.0-1.0 Bereich)
                    materialConfig.EmissionColor = new JArray(
        colorDialog.Color.R / 255.0f,
        colorDialog.Color.G / 255.0f,
        colorDialog.Color.B / 255.0f
    );
                    
                    // Button-Farbe anpassen
                    buttonEmissionColor.BackColor = colorDialog.Color;
                    buttonEmissionColor.ForeColor = GetContrastColor(colorDialog.Color);
                    
                    // PBR-Vorschau aktualisieren
                    UpdatePBRPreview();
                }
            }
        }
        #endregion  Event Handler

        #region Real-time Parameter Update Event Handlers

        private void InitializeTrackBarEventHandlers()
        {
            try
            {
                // Base Color Parameters
                if (trackBarBaseColorStrength != null)
                    trackBarBaseColorStrength.Scroll += TrackBarBaseColorStrength_Scroll;
                if (trackBarContrast != null)
                    trackBarContrast.Scroll += TrackBarContrast_Scroll;
                if (trackBarBrightness != null)
                    trackBarBrightness.Scroll += TrackBarBrightness_Scroll;
                
                // Metallic Parameters
                if (trackBarMetallicStrength != null)
                    trackBarMetallicStrength.Scroll += TrackBarMetallicStrength_Scroll;
                if (trackBarMetallicThreshold != null)
                    trackBarMetallicThreshold.Scroll += TrackBarMetallicThreshold_Scroll;
                
                // Roughness Parameters
                if (trackBarRoughnessStrength != null)
                    trackBarRoughnessStrength.Scroll += TrackBarRoughnessStrength_Scroll;
                
                // Normal Map Parameters
                if (trackBarNormalStrength != null)
                    trackBarNormalStrength.Scroll += TrackBarNormalStrength_Scroll;
                if (trackBarNormalFlipY != null)
                    trackBarNormalFlipY.Scroll += TrackBarNormalFlipY_Scroll;
                
                // Ambient Occlusion Parameters
                if (trackBarOcclusionStrength != null)
                    trackBarOcclusionStrength.Scroll += TrackBarOcclusionStrength_Scroll;
                
                // Emission Parameters
                if (trackBarEmissionStrength != null)
                    trackBarEmissionStrength.Scroll += TrackBarEmissionStrength_Scroll;
                if (trackBarEmissionEdgeEnhance != null)
                    trackBarEmissionEdgeEnhance.Scroll += TrackBarEmissionEdgeEnhance_Scroll;
                if (trackBarEmissionEdgeStrength != null)
                    trackBarEmissionEdgeStrength.Scroll += TrackBarEmissionEdgeStrength_Scroll;
                
                // Alpha Parameters
                if (trackBarAlphaStrength != null)
                    trackBarAlphaStrength.Scroll += TrackBarAlphaStrength_Scroll;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing TrackBar event handlers: {ex.Message}");
            }
        }

        private void TrackBarBaseColorStrength_Scroll(object sender, EventArgs e)
        {
            try
            {
                var value = trackBarBaseColorStrength.Value / 100.0f;
                if (textBoxBaseColorStrength != null)
                    textBoxBaseColorStrength.Text = value.ToString("F2");
                UpdateMaterialConfigValue("BaseColorStrength", value);
                UpdatePreviewInRealTime();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in TrackBarBaseColorStrength_Scroll: {ex.Message}");
            }
        }

        private void TrackBarContrast_Scroll(object sender, EventArgs e)
        {
            try
            {
                var value = trackBarContrast.Value / 100.0f;
                if (textBoxContrast != null)
                    textBoxContrast.Text = value.ToString("F2");
                UpdateMaterialConfigValue("Contrast", value);
                UpdatePreviewInRealTime();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in TrackBarContrast_Scroll: {ex.Message}");
            }
        }

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            try
            {
                var value = trackBarBrightness.Value / 100.0f;
                if (textBoxBrightness != null)
                    textBoxBrightness.Text = value.ToString("F2");
                UpdateMaterialConfigValue("Brightness", value);
                UpdatePreviewInRealTime();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in TrackBarBrightness_Scroll: {ex.Message}");
            }
        }

        private void TrackBarMetallicStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarMetallicStrength.Value / 100.0f;
            textBoxMetallicStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("MetallicIntensity", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarMetallicThreshold_Scroll(object sender, EventArgs e)
        {
            var value = trackBarMetallicThreshold.Value;
            textBoxMetallicThreshold.Text = value.ToString();
            UpdateMaterialConfigValue("MetallicThreshold", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarRoughnessStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarRoughnessStrength.Value / 100.0f;
            textBoxRoughnessStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("RoughnessStrength", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarNormalStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarNormalStrength.Value / 100.0f;
            textBoxNormalStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("NormalStrength", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarNormalFlipY_Scroll(object sender, EventArgs e)
        {
            var value = trackBarNormalFlipY.Value;
            textBoxNormalFlipY.Text = value.ToString();
            UpdateMaterialConfigValue("NormalFlipY", value > 0);
            UpdatePreviewInRealTime();
        }

        private void TrackBarOcclusionStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarOcclusionStrength.Value / 100.0f;
            textBoxOcclusionStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("OcclusionStrength", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarEmissionStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarEmissionStrength.Value / 100.0f;
            textBoxEmissionStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("EmissionStrength", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarEmissionEdgeEnhance_Scroll(object sender, EventArgs e)
        {
            var value = trackBarEmissionEdgeEnhance.Value / 100.0f;
            textBoxEmissionEdgeEnhance.Text = value.ToString("F2");
            UpdateMaterialConfigValue("EmissionEdgeEnhance", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarEmissionEdgeStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarEmissionEdgeStrength.Value / 100.0f;
            textBoxEmissionEdgeStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("EmissionEdgeStrength", value);
            UpdatePreviewInRealTime();
        }

        private void TrackBarAlphaStrength_Scroll(object sender, EventArgs e)
        {
            var value = trackBarAlphaStrength.Value / 100.0f;
            textBoxAlphaStrength.Text = value.ToString("F2");
            UpdateMaterialConfigValue("AlphaStrength", value);
            UpdatePreviewInRealTime();
        }

        private System.Windows.Forms.Timer previewUpdateTimer;


        #endregion  Real-time Parameter Update Event Handlers
    }
}