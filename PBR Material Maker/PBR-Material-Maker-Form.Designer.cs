using System.Windows.Forms;
using System.Drawing;
using System; // <--- Diese Zeile hinzugefügt

namespace PBR_Material_Maker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form not Designer generated user code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Controls erstellen
            toolTip1 = new ToolTip(components);
            mainTableLayoutPanel = new TableLayoutPanel();
            
            // Spalte 1: Drag & Drop Bereich
            panelDragDrop = new Panel();
            labelBaseColor = new Label();
            pictureBoxBaseColor = new PictureBox();
            labelAlpha = new Label();
            pictureBoxAlpha = new PictureBox();
            labelOcclusion = new Label();
            pictureBoxOcclusion = new PictureBox();
            labelRoughness = new Label();
            pictureBoxRoughness = new PictureBox();
            labelMetallic = new Label();
            pictureBoxMetallic = new PictureBox();
            labelNormal = new Label();
            pictureBoxNormal = new PictureBox();
            labelEmission = new Label();
            pictureBoxEmission = new PictureBox();
            
            // Spalte 2: Parameter & Buttons
            panelControls = new Panel();
            labelMaterialName = new Label();
            textBoxMaterialName = new TextBox();
            labelMaterialSelect = new Label();
            comboBoxMaterialSelect = new ComboBox();
            labelResolution = new Label();
            comboBoxResolution = new ComboBox();
            buttonGenerateMaps = new Button();
            buttonSave = new Button();
            buttonClear = new Button();
            
            // Base Color Parameter Controls
            labelBaseColorParams = new Label();
            labelBaseColorStrength = new Label();
            trackBarBaseColorStrength = new TrackBar();
            textBoxBaseColorStrength = new TextBox();
            labelContrast = new Label();
            trackBarContrast = new TrackBar();
            textBoxContrast = new TextBox();
            labelBrightness = new Label();
            trackBarBrightness = new TrackBar();
            textBoxBrightness = new TextBox();
            
            // Metallic Parameter Controls
            labelMetallicParams = new Label();
            labelMetallicStrength = new Label();
            trackBarMetallicStrength = new TrackBar();
            textBoxMetallicStrength = new TextBox();
            labelMetallicThreshold = new Label();
            trackBarMetallicThreshold = new TrackBar();
            textBoxMetallicThreshold = new TextBox();
            
            // Roughness Parameter Controls
            labelRoughnessParams = new Label();
            labelRoughnessStrength = new Label();
            trackBarRoughnessStrength = new TrackBar();
            textBoxRoughnessStrength = new TextBox();
            
            // Normal Map Parameter Controls
            labelNormalParams = new Label();
            labelNormalStrength = new Label();
            trackBarNormalStrength = new TrackBar();
            textBoxNormalStrength = new TextBox();
            labelNormalFlipY = new Label();
            trackBarNormalFlipY = new TrackBar();
            textBoxNormalFlipY = new TextBox();
            
            // Ambient Occlusion Parameter Controls
            labelOcclusionParams = new Label();
            labelOcclusionStrength = new Label();
            trackBarOcclusionStrength = new TrackBar();
            textBoxOcclusionStrength = new TextBox();
            
            // Emission Parameter Controls
            labelEmissionParams = new Label();
            labelEmissionStrength = new Label();
            trackBarEmissionStrength = new TrackBar();
            textBoxEmissionStrength = new TextBox();
            labelEmissionEdgeEnhance = new Label();
            trackBarEmissionEdgeEnhance = new TrackBar();
            textBoxEmissionEdgeEnhance = new TextBox();
            labelEmissionEdgeStrength = new Label();
            trackBarEmissionEdgeStrength = new TrackBar();
            textBoxEmissionEdgeStrength = new TextBox();
            
            // Alpha Parameter Controls
            labelAlphaParams = new Label();
            labelAlphaStrength = new Label();
            trackBarAlphaStrength = new TrackBar();
            textBoxAlphaStrength = new TextBox(); // statt comboBoxAlphaStrength
            
            // Color Picker Buttons
            buttonBaseColorTint = new Button();
            buttonEmissionColor = new Button();
            
            // Spalte 3: Vorschau
            panelPreview = new Panel();
            pictureBoxPBRPreview = new PictureBox();
            
            // Footer
            panelFooter = new Panel();
            labelVersion = new Label();
            checkBoxKeepOntop = new CheckBox();

            // Initialisierung beginnen
            mainTableLayoutPanel.SuspendLayout();
            panelDragDrop.SuspendLayout();
            panelControls.SuspendLayout();
            panelPreview.SuspendLayout();
            panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBaseColor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAlpha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOcclusion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRoughness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMetallic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNormal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEmission).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPBRPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBaseColorStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMetallicStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMetallicThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRoughnessStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarNormalStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarNormalFlipY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOcclusionStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEmissionStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEmissionEdgeEnhance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEmissionEdgeStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarAlphaStrength).BeginInit();
            SuspendLayout();

            // 
            // MainTableLayoutPanel - 3-Spalten Layout
            // 
            mainTableLayoutPanel.ColumnCount = 3;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(panelDragDrop, 0, 0);
            mainTableLayoutPanel.Controls.Add(panelControls, 1, 0);
            mainTableLayoutPanel.Controls.Add(panelPreview, 2, 0);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Margin = new Padding(0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 1;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Size = new Size(1200, 900);
            mainTableLayoutPanel.TabIndex = 0;

            // 
            // PanelDragDrop - Spalte 1: Drag & Drop Texturen
            // 
            panelDragDrop.BackColor = Color.FromArgb(50, 50, 50);
            panelDragDrop.Controls.Add(labelBaseColor);
            panelDragDrop.Controls.Add(pictureBoxBaseColor);
            panelDragDrop.Controls.Add(labelAlpha);
            panelDragDrop.Controls.Add(pictureBoxAlpha);
            panelDragDrop.Controls.Add(labelOcclusion);
            panelDragDrop.Controls.Add(pictureBoxOcclusion);
            panelDragDrop.Controls.Add(labelRoughness);
            panelDragDrop.Controls.Add(pictureBoxRoughness);
            panelDragDrop.Controls.Add(labelMetallic);
            panelDragDrop.Controls.Add(pictureBoxMetallic);
            panelDragDrop.Controls.Add(labelNormal);
            panelDragDrop.Controls.Add(pictureBoxNormal);
            panelDragDrop.Controls.Add(labelEmission);
            panelDragDrop.Controls.Add(pictureBoxEmission);
            
            panelDragDrop.Dock = DockStyle.Fill;
            panelDragDrop.Padding = new Padding(10);
            panelDragDrop.Name = "panelDragDrop";
            panelDragDrop.TabIndex = 0;

            // Base Color
            labelBaseColor.AutoSize = true;
            labelBaseColor.ForeColor = Color.White;
            labelBaseColor.Location = new Point(10, 10);
            labelBaseColor.Name = "labelBaseColor";
            labelBaseColor.Size = new Size(68, 15);
            labelBaseColor.TabIndex = 0;
            labelBaseColor.Text = "&Base Color*";

            pictureBoxBaseColor.AllowDrop = true;
            pictureBoxBaseColor.BackColor = SystemColors.ControlLight;
            pictureBoxBaseColor.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxBaseColor.Location = new Point(10, 28);
            pictureBoxBaseColor.Name = "pictureBoxBaseColor";
            pictureBoxBaseColor.Size = new Size(80, 80);
            pictureBoxBaseColor.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxBaseColor.TabIndex = 1;
            pictureBoxBaseColor.TabStop = false;
            toolTip1.SetToolTip(pictureBoxBaseColor, "Base Color Textur - RGB Daten");
            pictureBoxBaseColor.DragDrop += PictureBoxDragDrop;
            pictureBoxBaseColor.DragEnter += PictureBoxDragEnter;
            pictureBoxBaseColor.MouseDown += PictureBoxMouseDown;

            // Alpha
            labelAlpha.AutoSize = true;
            labelAlpha.ForeColor = Color.White;
            labelAlpha.Location = new Point(10, 120);
            labelAlpha.Name = "labelAlpha";
            labelAlpha.Size = new Size(38, 15);
            labelAlpha.TabIndex = 2;
            labelAlpha.Text = "&Alpha";

            pictureBoxAlpha.AllowDrop = true;
            pictureBoxAlpha.BackColor = SystemColors.ControlLight;
            pictureBoxAlpha.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxAlpha.Location = new Point(10, 138);
            pictureBoxAlpha.Name = "pictureBoxAlpha";
            pictureBoxAlpha.Size = new Size(80, 80);
            pictureBoxAlpha.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAlpha.TabIndex = 3;
            pictureBoxAlpha.TabStop = false;
            toolTip1.SetToolTip(pictureBoxAlpha, "Alpha Kanal - Graustufenbild");
            pictureBoxAlpha.DragDrop += PictureBoxDragDrop;
            pictureBoxAlpha.DragEnter += PictureBoxDragEnter;
            pictureBoxAlpha.MouseDown += PictureBoxMouseDown;

            // Occlusion
            labelOcclusion.AutoSize = true;
            labelOcclusion.ForeColor = Color.White;
            labelOcclusion.Location = new Point(10, 230);
            labelOcclusion.Name = "labelOcclusion";
            labelOcclusion.Size = new Size(60, 15);
            labelOcclusion.TabIndex = 4;
            labelOcclusion.Text = "&Occlusion";

            pictureBoxOcclusion.AllowDrop = true;
            pictureBoxOcclusion.BackColor = SystemColors.ControlLight;
            pictureBoxOcclusion.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxOcclusion.Location = new Point(10, 248);
            pictureBoxOcclusion.Name = "pictureBoxOcclusion";
            pictureBoxOcclusion.Size = new Size(80, 80);
            pictureBoxOcclusion.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOcclusion.TabIndex = 5;
            pictureBoxOcclusion.TabStop = false;
            toolTip1.SetToolTip(pictureBoxOcclusion, "Occlusion Map für ORM");
            pictureBoxOcclusion.DragDrop += PictureBoxDragDrop;
            pictureBoxOcclusion.DragEnter += PictureBoxDragEnter;
            pictureBoxOcclusion.MouseDown += PictureBoxMouseDown;

            // Roughness
            labelRoughness.AutoSize = true;
            labelRoughness.ForeColor = Color.White;
            labelRoughness.Location = new Point(10, 340);
            labelRoughness.Name = "labelRoughness";
            labelRoughness.Size = new Size(65, 15);
            labelRoughness.TabIndex = 6;
            labelRoughness.Text = "&Roughness";

            pictureBoxRoughness.AllowDrop = true;
            pictureBoxRoughness.BackColor = SystemColors.ControlLight;
            pictureBoxRoughness.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxRoughness.Location = new Point(10, 358);
            pictureBoxRoughness.Name = "pictureBoxRoughness";
            pictureBoxRoughness.Size = new Size(80, 80);
            pictureBoxRoughness.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxRoughness.TabIndex = 7;
            pictureBoxRoughness.TabStop = false;
            toolTip1.SetToolTip(pictureBoxRoughness, "Roughness Map für ORM");
            pictureBoxRoughness.DragDrop += PictureBoxDragDrop;
            pictureBoxRoughness.DragEnter += PictureBoxDragEnter;
            pictureBoxRoughness.MouseDown += PictureBoxMouseDown;

            // Metallic
            labelMetallic.AutoSize = true;
            labelMetallic.ForeColor = Color.White;
            labelMetallic.Location = new Point(10, 450);
            labelMetallic.Name = "labelMetallic";
            labelMetallic.Size = new Size(49, 15);
            labelMetallic.TabIndex = 8;
            labelMetallic.Text = "&Metallic";

            pictureBoxMetallic.AllowDrop = true;
            pictureBoxMetallic.BackColor = SystemColors.ControlLight;
            pictureBoxMetallic.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxMetallic.Location = new Point(10, 468);
            pictureBoxMetallic.Name = "pictureBoxMetallic";
            pictureBoxMetallic.Size = new Size(80, 80);
            pictureBoxMetallic.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxMetallic.TabIndex = 9;
            pictureBoxMetallic.TabStop = false;
            toolTip1.SetToolTip(pictureBoxMetallic, "Metallic Map für ORM");
            pictureBoxMetallic.DragDrop += PictureBoxDragDrop;
            pictureBoxMetallic.DragEnter += PictureBoxDragEnter;
            pictureBoxMetallic.MouseDown += PictureBoxMouseDown;

            // Normal
            labelNormal.AutoSize = true;
            labelNormal.ForeColor = Color.White;
            labelNormal.Location = new Point(10, 560);
            labelNormal.Name = "labelNormal";
            labelNormal.Size = new Size(47, 15);
            labelNormal.TabIndex = 10;
            labelNormal.Text = "&Normal";

            pictureBoxNormal.AllowDrop = true;
            pictureBoxNormal.BackColor = SystemColors.ControlLight;
            pictureBoxNormal.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxNormal.Location = new Point(10, 578);
            pictureBoxNormal.Name = "pictureBoxNormal";
            pictureBoxNormal.Size = new Size(80, 80);
            pictureBoxNormal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxNormal.TabIndex = 11;
            pictureBoxNormal.TabStop = false;
            toolTip1.SetToolTip(pictureBoxNormal, "Normal Map");
            pictureBoxNormal.DragDrop += PictureBoxDragDrop;
            pictureBoxNormal.DragEnter += PictureBoxDragEnter;
            pictureBoxNormal.MouseDown += PictureBoxMouseDown;

            // Emission
            labelEmission.AutoSize = true;
            labelEmission.ForeColor = Color.White;
            labelEmission.Location = new Point(10, 670);
            labelEmission.Name = "labelEmission";
            labelEmission.Size = new Size(54, 15);
            labelEmission.TabIndex = 12;
            labelEmission.Text = "&Emission";

            pictureBoxEmission.AllowDrop = true;
            pictureBoxEmission.BackColor = SystemColors.ControlLight;
            pictureBoxEmission.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxEmission.Location = new Point(10, 688);
            pictureBoxEmission.Name = "pictureBoxEmission";
            pictureBoxEmission.Size = new Size(80, 80);
            pictureBoxEmission.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEmission.TabIndex = 13;
            pictureBoxEmission.TabStop = false;
            toolTip1.SetToolTip(pictureBoxEmission, "Emission Map");
            pictureBoxEmission.DragDrop += PictureBoxDragDrop;
            pictureBoxEmission.DragEnter += PictureBoxDragEnter;
            pictureBoxEmission.MouseDown += PictureBoxMouseDown;

            // 
            // PanelControls - Spalte 2: Parameter & Buttons
            // 
            panelControls.BackColor = Color.FromArgb(80, 80, 80);
            panelControls.Controls.Add(labelMaterialSelect);
            panelControls.Controls.Add(comboBoxMaterialSelect);
            panelControls.Controls.Add(labelResolution);
            panelControls.Controls.Add(comboBoxResolution);
            
            // Base Color Parameter
            panelControls.Controls.Add(labelBaseColorParams);
            panelControls.Controls.Add(labelBaseColorStrength);
            panelControls.Controls.Add(trackBarBaseColorStrength);
            panelControls.Controls.Add(textBoxBaseColorStrength); // statt comboBoxBaseColorStrength
            panelControls.Controls.Add(buttonBaseColorTint);
            panelControls.Controls.Add(labelContrast);
            panelControls.Controls.Add(trackBarContrast);
            panelControls.Controls.Add(textBoxContrast); // statt comboBoxContrast
            panelControls.Controls.Add(labelBrightness);
            panelControls.Controls.Add(trackBarBrightness);
            panelControls.Controls.Add(textBoxBrightness); // statt comboBoxBrightness
            
            // Metallic Parameter
            panelControls.Controls.Add(labelMetallicParams);
            panelControls.Controls.Add(labelMetallicStrength);
            panelControls.Controls.Add(trackBarMetallicStrength);
            panelControls.Controls.Add(textBoxMetallicStrength); // statt comboBoxMetallicStrength
            panelControls.Controls.Add(labelMetallicThreshold);
            panelControls.Controls.Add(trackBarMetallicThreshold);
            panelControls.Controls.Add(textBoxMetallicThreshold); // statt comboBoxMetallicThreshold
            
            // Roughness Parameter
            panelControls.Controls.Add(labelRoughnessParams);
            panelControls.Controls.Add(labelRoughnessStrength);
            panelControls.Controls.Add(trackBarRoughnessStrength);
            panelControls.Controls.Add(textBoxRoughnessStrength); // statt comboBoxRoughnessStrength
            
            // Normal Map Parameter
            panelControls.Controls.Add(labelNormalParams);
            panelControls.Controls.Add(labelNormalStrength);
            panelControls.Controls.Add(trackBarNormalStrength);
            panelControls.Controls.Add(textBoxNormalStrength); // statt comboBoxNormalStrength
            panelControls.Controls.Add(labelNormalFlipY);
            panelControls.Controls.Add(trackBarNormalFlipY);
            panelControls.Controls.Add(textBoxNormalFlipY); // statt comboBoxNormalFlipY
            
            // Ambient Occlusion Parameter
            panelControls.Controls.Add(labelOcclusionParams);
            panelControls.Controls.Add(labelOcclusionStrength);
            panelControls.Controls.Add(trackBarOcclusionStrength);
            panelControls.Controls.Add(textBoxOcclusionStrength); // statt comboBoxOcclusionStrength
            
            // Emission Parameter
            panelControls.Controls.Add(labelEmissionParams);
            panelControls.Controls.Add(labelEmissionStrength);
            panelControls.Controls.Add(trackBarEmissionStrength);
            panelControls.Controls.Add(textBoxEmissionStrength); // statt comboBoxEmissionStrength
            panelControls.Controls.Add(buttonEmissionColor);
            panelControls.Controls.Add(labelEmissionEdgeEnhance);
            panelControls.Controls.Add(trackBarEmissionEdgeEnhance);
            panelControls.Controls.Add(textBoxEmissionEdgeEnhance); // statt comboBoxEmissionEdgeEnhance
            panelControls.Controls.Add(labelEmissionEdgeStrength);
            panelControls.Controls.Add(trackBarEmissionEdgeStrength);
            panelControls.Controls.Add(textBoxEmissionEdgeStrength); // statt comboBoxEmissionEdgeStrength
            
            // Alpha Parameter
            panelControls.Controls.Add(labelAlphaParams);
            panelControls.Controls.Add(labelAlphaStrength);
            panelControls.Controls.Add(trackBarAlphaStrength);
            panelControls.Controls.Add(textBoxAlphaStrength);
            
            panelControls.Dock = DockStyle.Fill;
            panelControls.Padding = new Padding(10);
            panelControls.Name = "panelControls";
            panelControls.TabIndex = 1;
            panelControls.AutoScroll = true;

            // Material Name (in Preview Panel)
            labelMaterialName.AutoSize = true;
            labelMaterialName.ForeColor = Color.White;
            labelMaterialName.Location = new Point(20, 440);
            labelMaterialName.Name = "labelMaterialName";
            labelMaterialName.Size = new Size(84, 15);
            labelMaterialName.TabIndex = 0;
            labelMaterialName.Text = "Material Name:";

            textBoxMaterialName.BackColor = Color.FromArgb(60, 60, 60);
            textBoxMaterialName.ForeColor = Color.White;
            textBoxMaterialName.Location = new Point(20, 460);
            textBoxMaterialName.Name = "textBoxMaterialName";
            textBoxMaterialName.Size = new Size(320, 23);
            textBoxMaterialName.TabIndex = 1;
            toolTip1.SetToolTip(textBoxMaterialName, "Name des Materials");

            // Material Select
            labelMaterialSelect.AutoSize = true;
            labelMaterialSelect.ForeColor = Color.White;
            labelMaterialSelect.Location = new Point(15, 15);
            labelMaterialSelect.Name = "labelMaterialSelect";
            labelMaterialSelect.Size = new Size(90, 15);
            labelMaterialSelect.TabIndex = 2;
            labelMaterialSelect.Text = "Material Preset:";

            comboBoxMaterialSelect.BackColor = Color.FromArgb(60, 60, 60);
            comboBoxMaterialSelect.ForeColor = Color.White;
            comboBoxMaterialSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMaterialSelect.FormattingEnabled = true;
            comboBoxMaterialSelect.Location = new Point(15, 35);
            comboBoxMaterialSelect.Name = "comboBoxMaterialSelect";
            comboBoxMaterialSelect.Size = new Size(220, 23);
            comboBoxMaterialSelect.TabIndex = 3;

            // Resolution
            labelResolution.AutoSize = true;
            labelResolution.ForeColor = Color.White;
            labelResolution.Location = new Point(15, 75);
            labelResolution.Name = "labelResolution";
            labelResolution.Size = new Size(67, 15);
            labelResolution.TabIndex = 4;
            labelResolution.Text = "Auflösung:";

            comboBoxResolution.BackColor = Color.FromArgb(60, 60, 60);
            comboBoxResolution.ForeColor = Color.White;
            comboBoxResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxResolution.FormattingEnabled = true;
            comboBoxResolution.Items.AddRange(new object[] { "Don't Resize", "2048 * 2048", "1024 * 1024", "512 * 512", "256 * 256", "128 * 128", "64 * 64", "32 * 32", "16 * 16", "8 * 8", "Custom..." });
            comboBoxResolution.Location = new Point(15, 95);
            comboBoxResolution.Name = "comboBoxResolution";
            comboBoxResolution.Size = new Size(220, 23);
            comboBoxResolution.TabIndex = 5;
            comboBoxResolution.SelectedIndexChanged += ComboBoxResolution_SelectedIndexChanged;

            // Base Color Parameter Section
            labelBaseColorParams.AutoSize = true;
            labelBaseColorParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelBaseColorParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelBaseColorParams.Location = new Point(15, 140);
            labelBaseColorParams.Name = "labelBaseColorParams";
            labelBaseColorParams.Size = new Size(75, 15);
            labelBaseColorParams.TabIndex = 6;
            labelBaseColorParams.Text = "Base Color:";

            // Base Color Strength
            labelBaseColorStrength.AutoSize = true;
            labelBaseColorStrength.ForeColor = Color.White;
            labelBaseColorStrength.Location = new Point(15, 165);
            labelBaseColorStrength.Name = "labelBaseColorStrength";
            labelBaseColorStrength.Size = new Size(100, 15);
            labelBaseColorStrength.TabIndex = 7;
            labelBaseColorStrength.Text = "Intensität";

            trackBarBaseColorStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarBaseColorStrength.Location = new Point(115, 160);
            trackBarBaseColorStrength.Maximum = 200;
            trackBarBaseColorStrength.Minimum = 0;
            trackBarBaseColorStrength.Name = "trackBarBaseColorStrength";
            trackBarBaseColorStrength.Size = new Size(100, 25);
            trackBarBaseColorStrength.TabIndex = 8;
            trackBarBaseColorStrength.TickStyle = TickStyle.TopLeft;
            trackBarBaseColorStrength.TickFrequency = 25;
            trackBarBaseColorStrength.TickFrequency = 25;
            trackBarBaseColorStrength.Value = 100;
            toolTip1.SetToolTip(trackBarBaseColorStrength, "Base Color Intensität:\nVerstärkt oder reduziert die Intensität der Grundfarbe\n• 0.0 = Keine Farbe (grau)\n• 1.0 = Original-Farbintensität\n• 2.0 = Verstärkte Farben");

            textBoxBaseColorStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxBaseColorStrength.ForeColor = Color.White;
            textBoxBaseColorStrength.Location = new Point(215, 162);
            textBoxBaseColorStrength.Name = "textBoxBaseColorStrength";
            textBoxBaseColorStrength.Size = new Size(70, 23);
            textBoxBaseColorStrength.TabIndex = 9;
            textBoxBaseColorStrength.Text = "1.0";

            // Contrast
            labelContrast.AutoSize = true;
            labelContrast.ForeColor = Color.White;
            labelContrast.Location = new Point(15, 195);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(100, 15);
            labelContrast.TabIndex = 10;
            labelContrast.Text = "Kontrast";

            trackBarContrast.BackColor = Color.FromArgb(80, 80, 80);
            trackBarContrast.Location = new Point(115, 190);
            trackBarContrast.Maximum = 200;
            trackBarContrast.Minimum = 50;
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(100, 25);
            trackBarContrast.TabIndex = 11;
            trackBarContrast.TickStyle = TickStyle.TopLeft;
            trackBarContrast.TickFrequency = 25;
            trackBarContrast.TickFrequency = 25;
            trackBarContrast.Value = 100;
            toolTip1.SetToolTip(trackBarContrast, "Base Color Kontrast:\nVerstärkt den Farbkontrast der Textur\n• 0.5 = Wenig Kontrast (flach)\n• 1.0 = Original-Kontrast\n• 2.0 = Hoher Kontrast (knackig)");

            textBoxContrast.BackColor = Color.FromArgb(60, 60, 60);
            textBoxContrast.ForeColor = Color.White;
            textBoxContrast.Location = new Point(215, 192);
            textBoxContrast.Name = "textBoxContrast";
            textBoxContrast.Size = new Size(70, 23);
            textBoxContrast.TabIndex = 12;
            textBoxContrast.Text = "1.0";

            // Brightness
            labelBrightness.AutoSize = true;
            labelBrightness.ForeColor = Color.White;
            labelBrightness.Location = new Point(15, 225);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(100, 15);
            labelBrightness.TabIndex = 13;
            labelBrightness.Text = "Helligkeit";

            trackBarBrightness.BackColor = Color.FromArgb(80, 80, 80);
            trackBarBrightness.Location = new Point(115, 220);
            trackBarBrightness.Maximum = 200;
            trackBarBrightness.Minimum = 0;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(100, 25);
            trackBarBrightness.TabIndex = 14;
            trackBarBrightness.TickStyle = TickStyle.TopLeft;
            trackBarBrightness.TickFrequency = 25;
            trackBarBrightness.TickFrequency = 25;
            trackBarBrightness.Value = 100;
            toolTip1.SetToolTip(trackBarBrightness, "Base Color Helligkeit:\nErhöht oder reduziert die Gesamthelligkeit\n• 0.0 = Sehr dunkel\n• 1.0 = Original-Helligkeit\n• 2.0 = Sehr hell");

            textBoxBrightness.BackColor = Color.FromArgb(60, 60, 60);
            textBoxBrightness.ForeColor = Color.White;
            textBoxBrightness.Location = new Point(215, 222);
            textBoxBrightness.Name = "textBoxBrightness";
            textBoxBrightness.Size = new Size(70, 23);
            textBoxBrightness.TabIndex = 15;
            textBoxBrightness.Text = "1.0";

            // Metallic Parameter Section
            labelMetallicParams.AutoSize = true;
            labelMetallicParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelMetallicParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelMetallicParams.Location = new Point(15, 260);
            labelMetallicParams.Name = "labelMetallicParams";
            labelMetallicParams.Size = new Size(55, 15);
            labelMetallicParams.TabIndex = 16;
            labelMetallicParams.Text = "Metallic:";

            // Metallic Strength
            labelMetallicStrength.AutoSize = true;
            labelMetallicStrength.ForeColor = Color.White;
            labelMetallicStrength.Location = new Point(15, 285);
            labelMetallicStrength.Name = "labelMetallicStrength";
            labelMetallicStrength.Size = new Size(100, 15);
            labelMetallicStrength.TabIndex = 17;
            labelMetallicStrength.Text = "Stärke";

            trackBarMetallicStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarMetallicStrength.Location = new Point(115, 280);
            trackBarMetallicStrength.Maximum = 200;
            trackBarMetallicStrength.Minimum = 0;
            trackBarMetallicStrength.Name = "trackBarMetallicStrength";
            trackBarMetallicStrength.Size = new Size(100, 25);
            trackBarMetallicStrength.TabIndex = 18;
            trackBarMetallicStrength.TickStyle = TickStyle.TopLeft;
            trackBarMetallicStrength.TickFrequency = 25;
            trackBarMetallicStrength.TickFrequency = 25;
            trackBarMetallicStrength.Value = 100;
            toolTip1.SetToolTip(trackBarMetallicStrength, "Metallic Verstärkung:\nVerstärkt metallische Eigenschaften für realistischere Reflexionen\n• 0.0 = Kein Metall (isolierend)\n• 1.0 = Original-Metallwerte\n• 2.0 = Verstärkt metallisch");

            textBoxMetallicStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxMetallicStrength.ForeColor = Color.White;
            textBoxMetallicStrength.Location = new Point(215, 282);
            textBoxMetallicStrength.Name = "textBoxMetallicStrength";
            textBoxMetallicStrength.Size = new Size(70, 23);
            textBoxMetallicStrength.TabIndex = 19;
            textBoxMetallicStrength.Text = "1.0";

            // Metallic Threshold
            labelMetallicThreshold.AutoSize = true;
            labelMetallicThreshold.ForeColor = Color.White;
            labelMetallicThreshold.Location = new Point(15, 315);
            labelMetallicThreshold.Name = "labelMetallicThreshold";
            labelMetallicThreshold.Size = new Size(100, 15);
            labelMetallicThreshold.TabIndex = 20;
            labelMetallicThreshold.Text = "Schwelle";

            trackBarMetallicThreshold.BackColor = Color.FromArgb(80, 80, 80);
            trackBarMetallicThreshold.Location = new Point(115, 310);
            trackBarMetallicThreshold.Maximum = 255;
            trackBarMetallicThreshold.Minimum = 0;
            trackBarMetallicThreshold.Name = "trackBarMetallicThreshold";
            trackBarMetallicThreshold.Size = new Size(100, 25);
            trackBarMetallicThreshold.TabIndex = 21;
            trackBarMetallicThreshold.TickStyle = TickStyle.TopLeft;
            trackBarMetallicThreshold.TickFrequency = 25;
            trackBarMetallicThreshold.TickFrequency = 32;
            trackBarMetallicThreshold.Value = 127;
            toolTip1.SetToolTip(trackBarMetallicThreshold, "Metallic Schwellenwert:\nBestimmt ab welchem Grauwert Pixel als metallisch erkannt werden\n• 0 = Alles wird metallisch\n• 127 = Mittlere Helligkeit als Grenze\n• 255 = Nur weiße Bereiche werden metallisch");

            textBoxMetallicThreshold.BackColor = Color.FromArgb(60, 60, 60);
            textBoxMetallicThreshold.ForeColor = Color.White;
            textBoxMetallicThreshold.Location = new Point(215, 312);
            textBoxMetallicThreshold.Name = "textBoxMetallicThreshold";
            textBoxMetallicThreshold.Size = new Size(70, 23);
            textBoxMetallicThreshold.TabIndex = 22;
            textBoxMetallicThreshold.Text = "127";

            // Roughness Parameter Section
            labelRoughnessParams.AutoSize = true;
            labelRoughnessParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelRoughnessParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelRoughnessParams.Location = new Point(15, 350);
            labelRoughnessParams.Name = "labelRoughnessParams";
            labelRoughnessParams.Size = new Size(71, 15);
            labelRoughnessParams.TabIndex = 23;
            labelRoughnessParams.Text = "Roughness:";

            // Roughness Strength
            labelRoughnessStrength.AutoSize = true;
            labelRoughnessStrength.ForeColor = Color.White;
            labelRoughnessStrength.Location = new Point(15, 375);
            labelRoughnessStrength.Name = "labelRoughnessStrength";
            labelRoughnessStrength.Size = new Size(100, 15);
            labelRoughnessStrength.TabIndex = 24;
            labelRoughnessStrength.Text = "Stärke";

            trackBarRoughnessStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarRoughnessStrength.Location = new Point(115, 370);
            trackBarRoughnessStrength.Maximum = 200;
            trackBarRoughnessStrength.Minimum = 0;
            trackBarRoughnessStrength.Name = "trackBarRoughnessStrength";
            trackBarRoughnessStrength.Size = new Size(100, 25);
            trackBarRoughnessStrength.TabIndex = 25;
            trackBarRoughnessStrength.TickStyle = TickStyle.TopLeft;
            trackBarRoughnessStrength.TickFrequency = 25;
            trackBarRoughnessStrength.Value = 20;
            toolTip1.SetToolTip(trackBarRoughnessStrength, "Roughness Verstärkung:\nVerstärkt Oberflächenrauheit für realistischere Lichtstreuung\n• 0.0 = Spiegelglatt\n• 0.2 = Leicht aufgeraut (Standard)\n• 2.0 = Sehr rau (matt)");

            textBoxRoughnessStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxRoughnessStrength.ForeColor = Color.White;
            textBoxRoughnessStrength.Location = new Point(215, 372);
            textBoxRoughnessStrength.Name = "textBoxRoughnessStrength";
            textBoxRoughnessStrength.Size = new Size(70, 23);
            textBoxRoughnessStrength.TabIndex = 26;
            textBoxRoughnessStrength.Text = "0.2";

            // Normal Map Parameter Section
            labelNormalParams.AutoSize = true;
            labelNormalParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelNormalParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelNormalParams.Location = new Point(15, 410);
            labelNormalParams.Name = "labelNormalParams";
            labelNormalParams.Size = new Size(78, 15);
            labelNormalParams.TabIndex = 27;
            labelNormalParams.Text = "Normal Map:";

            // Normal Strength
            labelNormalStrength.AutoSize = true;
            labelNormalStrength.ForeColor = Color.White;
            labelNormalStrength.Location = new Point(15, 435);
            labelNormalStrength.Name = "labelNormalStrength";
            labelNormalStrength.Size = new Size(100, 15);
            labelNormalStrength.TabIndex = 28;
            labelNormalStrength.Text = "Stärke";

            trackBarNormalStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarNormalStrength.Location = new Point(115, 430);
            trackBarNormalStrength.Maximum = 200;
            trackBarNormalStrength.Minimum = 0;
            trackBarNormalStrength.Name = "trackBarNormalStrength";
            trackBarNormalStrength.Size = new Size(100, 25);
            trackBarNormalStrength.TabIndex = 29;
            trackBarNormalStrength.TickStyle = TickStyle.TopLeft;
            trackBarNormalStrength.TickFrequency = 25;
            trackBarNormalStrength.Value = 20;
            toolTip1.SetToolTip(trackBarNormalStrength, "Normal Map Stärke:\nVerstärkt die Tiefenwirkung der Normal Map für realistischere Oberflächen\n• 0.0 = Flache Oberfläche\n• 0.2 = Subtile Struktur (Standard)\n• 2.0 = Stark geprägte Oberfläche");

            textBoxNormalStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxNormalStrength.ForeColor = Color.White;
            textBoxNormalStrength.Location = new Point(215, 432);
            textBoxNormalStrength.Name = "textBoxNormalStrength";
            textBoxNormalStrength.Size = new Size(70, 23);
            textBoxNormalStrength.TabIndex = 30;
            textBoxNormalStrength.Text = "0.2";

            // Normal Flip Y
            labelNormalFlipY.AutoSize = true;
            labelNormalFlipY.ForeColor = Color.White;
            labelNormalFlipY.Location = new Point(15, 465);
            labelNormalFlipY.Name = "labelNormalFlipY";
            labelNormalFlipY.Size = new Size(100, 15);
            labelNormalFlipY.TabIndex = 31;
            labelNormalFlipY.Text = "Y-Achse";

            trackBarNormalFlipY.BackColor = Color.FromArgb(80, 80, 80);
            trackBarNormalFlipY.Location = new Point(115, 460);
            trackBarNormalFlipY.Maximum = 1;
            trackBarNormalFlipY.Minimum = 0;
            trackBarNormalFlipY.Name = "trackBarNormalFlipY";
            trackBarNormalFlipY.Size = new Size(100, 25);
            trackBarNormalFlipY.TabIndex = 32;
            trackBarNormalFlipY.TickStyle = TickStyle.TopLeft;
            trackBarNormalFlipY.TickFrequency = 25;
            trackBarNormalFlipY.Value = 0;
            toolTip1.SetToolTip(trackBarNormalFlipY, "Normal Map Y-Orientierung:\nKehrt Y-Achse der Normal Map um für verschiedene Engines\n• 0 = OpenGL Standard (Y nach oben)\n• 1 = DirectX Standard (Y nach unten)");

            textBoxNormalFlipY.BackColor = Color.FromArgb(60, 60, 60);
            textBoxNormalFlipY.ForeColor = Color.White;
            textBoxNormalFlipY.Location = new Point(215, 462);
            textBoxNormalFlipY.Name = "textBoxNormalFlipY";
            textBoxNormalFlipY.Size = new Size(70, 23);
            textBoxNormalFlipY.TabIndex = 33;
            textBoxNormalFlipY.Text = "0";

            // Ambient Occlusion Parameter Section
            labelOcclusionParams.AutoSize = true;
            labelOcclusionParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelOcclusionParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelOcclusionParams.Location = new Point(15, 500);
            labelOcclusionParams.Name = "labelOcclusionParams";
            labelOcclusionParams.Size = new Size(123, 15);
            labelOcclusionParams.TabIndex = 34;
            labelOcclusionParams.Text = "Ambient Occlusion:";

            // Occlusion Strength
            labelOcclusionStrength.AutoSize = true;
            labelOcclusionStrength.ForeColor = Color.White;
            labelOcclusionStrength.Location = new Point(15, 525);
            labelOcclusionStrength.Name = "labelOcclusionStrength";
            labelOcclusionStrength.Size = new Size(100, 15);
            labelOcclusionStrength.TabIndex = 35;
            labelOcclusionStrength.Text = "Stärke";

            trackBarOcclusionStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarOcclusionStrength.Location = new Point(115, 520);
            trackBarOcclusionStrength.Maximum = 200;
            trackBarOcclusionStrength.Minimum = 0;
            trackBarOcclusionStrength.Name = "trackBarOcclusionStrength";
            trackBarOcclusionStrength.Size = new Size(100, 25);
            trackBarOcclusionStrength.TabIndex = 36;
            trackBarOcclusionStrength.TickStyle = TickStyle.TopLeft;
            trackBarOcclusionStrength.TickFrequency = 25;
            trackBarOcclusionStrength.Value = 100;
            toolTip1.SetToolTip(trackBarOcclusionStrength, "Ambient Occlusion Verstärkung:\nVerstärkt Selbstschattierung für realistischere Tiefenwirkung\n• 0.0 = Keine Schatten\n• 1.0 = Original-Schattierung\n• 2.0 = Verstärkte Schatten");

            textBoxOcclusionStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxOcclusionStrength.ForeColor = Color.White;
            textBoxOcclusionStrength.Location = new Point(215, 522);
            textBoxOcclusionStrength.Name = "textBoxOcclusionStrength";
            textBoxOcclusionStrength.Size = new Size(70, 23);
            textBoxOcclusionStrength.TabIndex = 37;
            textBoxOcclusionStrength.Text = "1.0";

            // Emission Parameter Section
            labelEmissionParams.AutoSize = true;
            labelEmissionParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelEmissionParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelEmissionParams.Location = new Point(15, 560);
            labelEmissionParams.Name = "labelEmissionParams";
            labelEmissionParams.Size = new Size(60, 15);
            labelEmissionParams.TabIndex = 38;
            labelEmissionParams.Text = "Emission:";

            // Emission Strength
            labelEmissionStrength.AutoSize = true;
            labelEmissionStrength.ForeColor = Color.White;
            labelEmissionStrength.Location = new Point(15, 585);
            labelEmissionStrength.Name = "labelEmissionStrength";
            labelEmissionStrength.Size = new Size(100, 15);
            labelEmissionStrength.TabIndex = 39;
            labelEmissionStrength.Text = "Leuchtstärke";

            trackBarEmissionStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarEmissionStrength.Location = new Point(115, 580);
            trackBarEmissionStrength.Maximum = 200;
            trackBarEmissionStrength.Minimum = 0;
            trackBarEmissionStrength.Name = "trackBarEmissionStrength";
            trackBarEmissionStrength.Size = new Size(100, 25);
            trackBarEmissionStrength.TabIndex = 40;
            trackBarEmissionStrength.TickStyle = TickStyle.TopLeft;
            trackBarEmissionStrength.TickFrequency = 25;
            trackBarEmissionStrength.Value = 0;
            toolTip1.SetToolTip(trackBarEmissionStrength, "Emission Leuchtintensität:\nMacht Texturbereiche selbstleuchtend\n• 0.0 = Kein Leuchten\n• 1.0 = Helle Leuchtbereiche\n• 2.0 = Intensive Leuchtbereiche");

            textBoxEmissionStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxEmissionStrength.ForeColor = Color.White;
            textBoxEmissionStrength.Location = new Point(215, 582);
            textBoxEmissionStrength.Name = "textBoxEmissionStrength";
            textBoxEmissionStrength.Size = new Size(70, 23);
            textBoxEmissionStrength.TabIndex = 41;
            textBoxEmissionStrength.Text = "0.0";

            // Emission Edge Enhance
            labelEmissionEdgeEnhance.AutoSize = true;
            labelEmissionEdgeEnhance.ForeColor = Color.White;
            labelEmissionEdgeEnhance.Location = new Point(15, 615);
            labelEmissionEdgeEnhance.Name = "labelEmissionEdgeEnhance";
            labelEmissionEdgeEnhance.Size = new Size(100, 15);
            labelEmissionEdgeEnhance.TabIndex = 42;
            labelEmissionEdgeEnhance.Text = "Kontur";

            trackBarEmissionEdgeEnhance.BackColor = Color.FromArgb(80, 80, 80);
            trackBarEmissionEdgeEnhance.Location = new Point(115, 610);
            trackBarEmissionEdgeEnhance.Maximum = 100;
            trackBarEmissionEdgeEnhance.Minimum = 0;
            trackBarEmissionEdgeEnhance.Name = "trackBarEmissionEdgeEnhance";
            trackBarEmissionEdgeEnhance.Size = new Size(100, 25);
            trackBarEmissionEdgeEnhance.TabIndex = 43;
            trackBarEmissionEdgeEnhance.TickStyle = TickStyle.TopLeft;
            trackBarEmissionEdgeEnhance.TickFrequency = 25;
            trackBarEmissionEdgeEnhance.Value = 0;
            toolTip1.SetToolTip(trackBarEmissionEdgeEnhance, "Emission Kantenbeleuchtung:\nLässt Objektkanten zusätzlich leuchten\n• 0.0 = Deaktiviert\n• 1.0 = Aktiviert");

            textBoxEmissionEdgeEnhance.BackColor = Color.FromArgb(60, 60, 60);
            textBoxEmissionEdgeEnhance.ForeColor = Color.White;
            textBoxEmissionEdgeEnhance.Location = new Point(215, 612);
            textBoxEmissionEdgeEnhance.Name = "textBoxEmissionEdgeEnhance";
            textBoxEmissionEdgeEnhance.Size = new Size(70, 23);
            textBoxEmissionEdgeEnhance.TabIndex = 44;
            textBoxEmissionEdgeEnhance.Text = "0.0";

            // Emission Edge Strength
            labelEmissionEdgeStrength.AutoSize = true;
            labelEmissionEdgeStrength.ForeColor = Color.White;
            labelEmissionEdgeStrength.Location = new Point(15, 645);
            labelEmissionEdgeStrength.Name = "labelEmissionEdgeStrength";
            labelEmissionEdgeStrength.Size = new Size(100, 15);
            labelEmissionEdgeStrength.TabIndex = 45;
            labelEmissionEdgeStrength.Text = "Stärke";

            trackBarEmissionEdgeStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarEmissionEdgeStrength.Location = new Point(115, 640);
            trackBarEmissionEdgeStrength.Maximum = 300;
            trackBarEmissionEdgeStrength.Minimum = 10;
            trackBarEmissionEdgeStrength.Name = "trackBarEmissionEdgeStrength";
            trackBarEmissionEdgeStrength.Size = new Size(100, 25);
            trackBarEmissionEdgeStrength.TabIndex = 46;
            trackBarEmissionEdgeStrength.TickStyle = TickStyle.TopLeft;
            trackBarEmissionEdgeStrength.TickFrequency = 25;
            trackBarEmissionEdgeStrength.Value = 100;
            toolTip1.SetToolTip(trackBarEmissionEdgeStrength, "Emission Kantenstärke:\nIntensität des Kontur-Leuchtens\n• 0.1 = Schwaches Leuchten\n• 1.0 = Normales Leuchten\n• 3.0 = Intensives Leuchten");

            textBoxEmissionEdgeStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxEmissionEdgeStrength.ForeColor = Color.White;
            textBoxEmissionEdgeStrength.Location = new Point(215, 642);
            textBoxEmissionEdgeStrength.Name = "textBoxEmissionEdgeStrength";
            textBoxEmissionEdgeStrength.Size = new Size(70, 23);
            textBoxEmissionEdgeStrength.TabIndex = 47;
            textBoxEmissionEdgeStrength.Text = "1.0";

            // Alpha Parameter Section
            labelAlphaParams.AutoSize = true;
            labelAlphaParams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelAlphaParams.ForeColor = Color.FromArgb(100, 150, 255);
            labelAlphaParams.Location = new Point(15, 680);
            labelAlphaParams.Name = "labelAlphaParams";
            labelAlphaParams.Size = new Size(82, 15);
            labelAlphaParams.TabIndex = 48;
            labelAlphaParams.Text = "Transparenz:";

            // Alpha Strength
            labelAlphaStrength.AutoSize = true;
            labelAlphaStrength.ForeColor = Color.White;
            labelAlphaStrength.Location = new Point(15, 705);
            labelAlphaStrength.Name = "labelAlphaStrength";
            labelAlphaStrength.Size = new Size(100, 15);
            labelAlphaStrength.TabIndex = 49;
            labelAlphaStrength.Text = "Stärke";

            trackBarAlphaStrength.BackColor = Color.FromArgb(80, 80, 80);
            trackBarAlphaStrength.Location = new Point(115, 700);
            trackBarAlphaStrength.Maximum = 200;
            trackBarAlphaStrength.Minimum = 0;
            trackBarAlphaStrength.Name = "trackBarAlphaStrength";
            trackBarAlphaStrength.Size = new Size(100, 25);
            trackBarAlphaStrength.TabIndex = 50;
            trackBarAlphaStrength.TickStyle = TickStyle.TopLeft;
            trackBarAlphaStrength.TickFrequency = 25;
            trackBarAlphaStrength.Value = 100;
            toolTip1.SetToolTip(trackBarAlphaStrength, "Alpha/Transparenz Verstärkung:\nVerstärkt Transparenz-/Undurchsichtigkeitseffekte\n• 0.0 = Vollständig transparent\n• 1.0 = Original-Transparenz\n• 2.0 = Verstärkt undurchsichtig");

            textBoxAlphaStrength.BackColor = Color.FromArgb(60, 60, 60);
            textBoxAlphaStrength.ForeColor = Color.White;
            textBoxAlphaStrength.Location = new Point(215, 702);
            textBoxAlphaStrength.Name = "textBoxAlphaStrength";
            textBoxAlphaStrength.Size = new Size(70, 23);
            textBoxAlphaStrength.TabIndex = 51;
            textBoxAlphaStrength.Text = "1.0";

            // Color Picker Buttons (in Drag&Drop Panel)
            buttonBaseColorTint.BackColor = Color.FromArgb(0, 120, 215);
            buttonBaseColorTint.FlatStyle = FlatStyle.Flat;
            buttonBaseColorTint.ForeColor = Color.White;
            buttonBaseColorTint.Location = new Point(285, 162);
            buttonBaseColorTint.Name = "buttonBaseColorTint";
            buttonBaseColorTint.Size = new Size(50, 25);
            buttonBaseColorTint.TabIndex = 52;
            buttonBaseColorTint.Text = "Tint";
            buttonBaseColorTint.UseVisualStyleBackColor = false;
            buttonBaseColorTint.Click += ButtonBaseColorTint_Click;
            toolTip1.SetToolTip(buttonBaseColorTint, "Base Color Tint-Farbe auswählen");

            buttonEmissionColor.BackColor = Color.FromArgb(0, 120, 215);
            buttonEmissionColor.FlatStyle = FlatStyle.Flat;
            buttonEmissionColor.ForeColor = Color.White;
            buttonEmissionColor.Location = new Point(285, 582);
            buttonEmissionColor.Name = "buttonEmissionColor";
            buttonEmissionColor.Size = new Size(50, 25);
            buttonEmissionColor.TabIndex = 53;
            buttonEmissionColor.Text = "Color";
            buttonEmissionColor.UseVisualStyleBackColor = false;
            buttonEmissionColor.Click += ButtonEmissionColor_Click;
            toolTip1.SetToolTip(buttonEmissionColor, "Emission-Farbe auswählen");

            // Generate Maps Button (in Preview Panel)
            buttonGenerateMaps.BackColor = Color.FromArgb(0, 150, 90);
            buttonGenerateMaps.FlatStyle = FlatStyle.Flat;
            buttonGenerateMaps.ForeColor = Color.White;
            buttonGenerateMaps.Location = new Point(20, 500);
            buttonGenerateMaps.Name = "buttonGenerateMaps";
            buttonGenerateMaps.Size = new Size(320, 35);
            buttonGenerateMaps.TabIndex = 6;
            buttonGenerateMaps.Text = "&Generiere Fehlende Maps";
            buttonGenerateMaps.UseVisualStyleBackColor = false;
            toolTip1.SetToolTip(buttonGenerateMaps, "Automatisch fehlende Texturen basierend auf vorhandenen Maps generieren");

            // Buttons (in Preview Panel)
            buttonSave.BackColor = Color.FromArgb(0, 120, 215);
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(20, 550);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(155, 35);
            buttonSave.TabIndex = 7;
            buttonSave.Text = "&Speichern...";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += ButtonSave_Click;
            toolTip1.SetToolTip(buttonSave, "Material speichern und GLTF erstellen");

            buttonClear.BackColor = Color.FromArgb(196, 43, 28);
            buttonClear.FlatStyle = FlatStyle.Flat;
            buttonClear.ForeColor = Color.White;
            buttonClear.Location = new Point(185, 550);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(155, 35);
            buttonClear.TabIndex = 8;
            buttonClear.Text = "&Löschen";
            buttonClear.UseVisualStyleBackColor = false;
            buttonClear.Click += ButtonClear_Click;
            toolTip1.SetToolTip(buttonClear, "Alle Texturen löschen");

            // 
            // PanelPreview - Spalte 3: PBR Vorschau
            // 
            panelPreview.BackColor = Color.FromArgb(40, 40, 40);
            panelPreview.Controls.Add(pictureBoxPBRPreview);
            panelPreview.Controls.Add(labelMaterialName);
            panelPreview.Controls.Add(textBoxMaterialName);
            panelPreview.Controls.Add(buttonGenerateMaps);
            panelPreview.Controls.Add(buttonSave);
            panelPreview.Controls.Add(buttonClear);
            panelPreview.Dock = DockStyle.Fill;
            panelPreview.Padding = new Padding(20);
            panelPreview.Name = "panelPreview";
            panelPreview.TabIndex = 2;

            pictureBoxPBRPreview.BackColor = Color.FromArgb(30, 30, 30);
            pictureBoxPBRPreview.BorderStyle = BorderStyle.None;
            pictureBoxPBRPreview.Dock = DockStyle.None;
            pictureBoxPBRPreview.Location = new Point(20, 20);
            pictureBoxPBRPreview.Name = "pictureBoxPBRPreview";
            pictureBoxPBRPreview.Size = new Size(520, 400);
            pictureBoxPBRPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPBRPreview.TabIndex = 0;
            pictureBoxPBRPreview.TabStop = false;
            toolTip1.SetToolTip(pictureBoxPBRPreview, "PBR Material Vorschau in Echtzeit");

            // 
            // PanelFooter
            // 
            panelFooter.BackColor = Color.Black;
            panelFooter.Controls.Add(labelVersion);
            panelFooter.Controls.Add(checkBoxKeepOntop);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.ForeColor = SystemColors.ControlDark;
            panelFooter.Location = new Point(0, 900);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(1200, 40);
            panelFooter.TabIndex = 1;

            labelVersion.AutoSize = true;
            labelVersion.ForeColor = Color.Gray;
            labelVersion.Location = new Point(10, 12);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(53, 15);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "{Version}";

            checkBoxKeepOntop.Appearance = Appearance.Button;
            checkBoxKeepOntop.Checked = true;
            checkBoxKeepOntop.CheckState = CheckState.Checked;
            checkBoxKeepOntop.FlatStyle = FlatStyle.Flat;
            checkBoxKeepOntop.ForeColor = Color.White;
            checkBoxKeepOntop.Location = new Point(1120, 8);
            checkBoxKeepOntop.Name = "checkBoxKeepOntop";
            checkBoxKeepOntop.Size = new Size(70, 25);
            checkBoxKeepOntop.TabIndex = 1;
            checkBoxKeepOntop.Text = "Pin";
            checkBoxKeepOntop.UseVisualStyleBackColor = true;
            checkBoxKeepOntop.CheckedChanged += CheckBoxKeepOntop_CheckedChanged;

            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(1200, 940);
            Controls.Add(mainTableLayoutPanel);
            Controls.Add(panelFooter);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ControlLightLight;
            MinimumSize = new Size(1000, 700);
            Name = "MainForm";
            ShowIcon = false;
            Text = "PBR Material Maker";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += Form1_Shown;
            ResizeEnd += Form1_ResizeEnd;

            // Layout beenden
            mainTableLayoutPanel.ResumeLayout(false);
            panelDragDrop.ResumeLayout(false);
            panelDragDrop.PerformLayout();
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            panelPreview.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBaseColor).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAlpha).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOcclusion).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRoughness).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMetallic).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNormal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEmission).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPBRPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBaseColorStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMetallicStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMetallicThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarRoughnessStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarNormalStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarNormalFlipY).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOcclusionStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEmissionStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEmissionEdgeEnhance).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarEmissionEdgeStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarAlphaStrength).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Neue 3-Spalten Layout Controls
        private TableLayoutPanel mainTableLayoutPanel;
        private Panel panelDragDrop;
        private Panel panelControls;
        private Panel panelPreview;
        private Label labelMaterialName;
        private Label labelMaterialSelect;
        private Label labelResolution;
        
        // Base Color Parameter Controls
        private Label labelBaseColorParams;
        private Label labelBaseColorStrength;
        private TrackBar trackBarBaseColorStrength;
        private TextBox textBoxBaseColorStrength;
        private Label labelContrast;
        private TrackBar trackBarContrast;
        private TextBox textBoxContrast;
        private Label labelBrightness;
        private TrackBar trackBarBrightness;
        private TextBox textBoxBrightness;
        
        // Metallic Parameter Controls
        private Label labelMetallicParams;
        private Label labelMetallicStrength;
        private TrackBar trackBarMetallicStrength;
        private TextBox textBoxMetallicStrength;
        private Label labelMetallicThreshold;
        private TrackBar trackBarMetallicThreshold;
        private TextBox textBoxMetallicThreshold;
        
        // Roughness Parameter Controls
        private Label labelRoughnessParams;
        private Label labelRoughnessStrength;
        private TrackBar trackBarRoughnessStrength;
        private TextBox textBoxRoughnessStrength;
        
        // Normal Map Parameter Controls
        private Label labelNormalParams;
        private Label labelNormalStrength;
        private TrackBar trackBarNormalStrength;
        private TextBox textBoxNormalStrength;
        private Label labelNormalFlipY;
        private TrackBar trackBarNormalFlipY;
        private TextBox textBoxNormalFlipY;
        
        // Ambient Occlusion Parameter Controls
        private Label labelOcclusionParams;
        private Label labelOcclusionStrength;
        private TrackBar trackBarOcclusionStrength;
        private TextBox textBoxOcclusionStrength;
        
        // Emission Parameter Controls
        private Label labelEmissionParams;
        private Label labelEmissionStrength;
        private TrackBar trackBarEmissionStrength;
        private TextBox textBoxEmissionStrength;
        private Label labelEmissionEdgeEnhance;
        private TrackBar trackBarEmissionEdgeEnhance;
        private TextBox textBoxEmissionEdgeEnhance;
        private Label labelEmissionEdgeStrength;
        private TrackBar trackBarEmissionEdgeStrength;
        private TextBox textBoxEmissionEdgeStrength;
        
        // Alpha Parameter Controls
        private Label labelAlphaParams;
        private Label labelAlphaStrength;
        private TrackBar trackBarAlphaStrength;
        private TextBox textBoxAlphaStrength;
        
        // Color Picker Buttons
        private Button buttonBaseColorTint;
        private Button buttonEmissionColor;
        
        // Vorhandene Controls
        private System.Windows.Forms.PictureBox pictureBoxBaseColor;
        private System.Windows.Forms.Label labelBaseColor;
        private System.Windows.Forms.Label labelOcclusion;
        private System.Windows.Forms.PictureBox pictureBoxOcclusion;
        private System.Windows.Forms.Label labelRoughness;
        private System.Windows.Forms.PictureBox pictureBoxRoughness;
        private System.Windows.Forms.Label labelMetallic;
        private System.Windows.Forms.PictureBox pictureBoxMetallic;
        private System.Windows.Forms.Label labelNormal;
        private System.Windows.Forms.PictureBox pictureBoxNormal;
        private System.Windows.Forms.Label labelEmission;
        private System.Windows.Forms.PictureBox pictureBoxEmission;
        private System.Windows.Forms.Button buttonGenerateMaps;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.CheckBox checkBoxKeepOntop;
        private System.Windows.Forms.Label labelAlpha;
        private System.Windows.Forms.PictureBox pictureBoxAlpha;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBoxMaterialSelect;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.PictureBox pictureBoxPBRPreview;

        private int GetNearestSmallerSquareSize(int width, int height)
        {
            int minSide = Math.Min(width, height);
            int[] allowedSizes = { 2048, 1024, 512, 256, 128, 64, 32, 16, 8 };
            foreach (int size in allowedSizes)
            {
                if (minSide >= size)
                    return size;
            }
            return 8; // Fallback
        }

        private Bitmap ResizeToSquare(Bitmap bmp)
        {
            int targetSize = GetNearestSmallerSquareSize(bmp.Width, bmp.Height);
            Bitmap result = new Bitmap(targetSize, targetSize);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, 0, 0, targetSize, targetSize);
            }
            return result;
        }
    }
}
