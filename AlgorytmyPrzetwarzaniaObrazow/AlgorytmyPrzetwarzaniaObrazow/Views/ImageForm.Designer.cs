using AlgorytmyPrzetwarzaniaObrazow.Controls;

namespace AlgorytmyPrzetwarzaniaObrazow
{
    partial class ImageForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageObraz = new System.Windows.Forms.TabPage();
            this.graphicsPanel = new BufferedPanel();
            this.tabPageTablica = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageHistogram = new System.Windows.Forms.TabPage();
            this.tabLiniaProfilu = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.liniaProfiluMainPanel = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label_max = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label_min = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.labelInstrukacja = new System.Windows.Forms.Label();
            this.label_4 = new System.Windows.Forms.Label();
            this.label_2 = new System.Windows.Forms.Label();
            this.label_0 = new System.Windows.Forms.Label();
            this.wykresProfiluPanel = new System.Windows.Forms.Panel();
            this.wykresProfiluPictureBox = new System.Windows.Forms.PictureBox();
            this.labelDlugosc = new System.Windows.Forms.Label();
            this.toolTipXY = new System.Windows.Forms.ToolTip(this.components);
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.lblZoomLvl = new System.Windows.Forms.Label();
            this.btnZoomReset = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.txtZoom = new System.Windows.Forms.TextBox();
            this.chkCut = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageObraz.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLiniaProfilu.SuspendLayout();
            this.liniaProfiluMainPanel.SuspendLayout();
            this.wykresProfiluPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wykresProfiluPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1065, 374);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabPageObraz);
            this.tabControl.Controls.Add(this.tabPageTablica);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(10, 3);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(373, 372);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageObraz
            // 
            this.tabPageObraz.AutoScroll = true;
            this.tabPageObraz.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageObraz.Controls.Add(this.graphicsPanel);
            this.tabPageObraz.Location = new System.Drawing.Point(4, 25);
            this.tabPageObraz.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageObraz.Name = "tabPageObraz";
            this.tabPageObraz.Size = new System.Drawing.Size(365, 343);
            this.tabPageObraz.TabIndex = 0;
            this.tabPageObraz.Text = "Obraz";
            this.tabPageObraz.UseVisualStyleBackColor = true;
            // 
            // graphicsPanel
            // 
            this.graphicsPanel.AutoScroll = true;
            this.graphicsPanel.BackColor = System.Drawing.Color.Black;
            this.graphicsPanel.Location = new System.Drawing.Point(0, 0);
            this.graphicsPanel.Name = "graphicsPanel";
            this.graphicsPanel.Size = new System.Drawing.Size(201, 206);
            this.graphicsPanel.TabIndex = 0;
            this.graphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphicsPanel_Paint);
            this.graphicsPanel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.graphicsPanel_KeyUp);
            this.graphicsPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.graphicsPanel_MouseClick);
            this.graphicsPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.graphicsPanel_MouseDoubleClick);
            this.graphicsPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphicsPanel_MouseDown);
            this.graphicsPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphicsPanel_MouseMove);
            this.graphicsPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphicsPanel_MouseUp);
            // 
            // tabPageTablica
            // 
            this.tabPageTablica.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageTablica.Location = new System.Drawing.Point(4, 25);
            this.tabPageTablica.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageTablica.Name = "tabPageTablica";
            this.tabPageTablica.Size = new System.Drawing.Size(365, 343);
            this.tabPageTablica.TabIndex = 1;
            this.tabPageTablica.Text = "Tablica";
            this.tabPageTablica.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPageHistogram);
            this.tabControl1.Controls.Add(this.tabLiniaProfilu);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 372);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageHistogram
            // 
            this.tabPageHistogram.AutoScroll = true;
            this.tabPageHistogram.Location = new System.Drawing.Point(4, 25);
            this.tabPageHistogram.Name = "tabPageHistogram";
            this.tabPageHistogram.Size = new System.Drawing.Size(676, 343);
            this.tabPageHistogram.TabIndex = 2;
            this.tabPageHistogram.Text = "Histogram";
            this.tabPageHistogram.UseVisualStyleBackColor = true;
            // 
            // tabLiniaProfilu
            // 
            this.tabLiniaProfilu.Controls.Add(this.label3);
            this.tabLiniaProfilu.Controls.Add(this.label2);
            this.tabLiniaProfilu.Controls.Add(this.label1);
            this.tabLiniaProfilu.Controls.Add(this.liniaProfiluMainPanel);
            this.tabLiniaProfilu.Location = new System.Drawing.Point(4, 25);
            this.tabLiniaProfilu.Name = "tabLiniaProfilu";
            this.tabLiniaProfilu.Padding = new System.Windows.Forms.Padding(3);
            this.tabLiniaProfilu.Size = new System.Drawing.Size(676, 343);
            this.tabLiniaProfilu.TabIndex = 3;
            this.tabLiniaProfilu.Text = "Linia profilu";
            this.tabLiniaProfilu.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Average";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Min";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Max";
            // 
            // liniaProfiluMainPanel
            // 
            this.liniaProfiluMainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.liniaProfiluMainPanel.Controls.Add(this.radioButton3);
            this.liniaProfiluMainPanel.Controls.Add(this.label_max);
            this.liniaProfiluMainPanel.Controls.Add(this.radioButton2);
            this.liniaProfiluMainPanel.Controls.Add(this.label_min);
            this.liniaProfiluMainPanel.Controls.Add(this.label8);
            this.liniaProfiluMainPanel.Controls.Add(this.radioButton1);
            this.liniaProfiluMainPanel.Controls.Add(this.labelInstrukacja);
            this.liniaProfiluMainPanel.Controls.Add(this.label_4);
            this.liniaProfiluMainPanel.Controls.Add(this.label_2);
            this.liniaProfiluMainPanel.Controls.Add(this.label_0);
            this.liniaProfiluMainPanel.Controls.Add(this.wykresProfiluPanel);
            this.liniaProfiluMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liniaProfiluMainPanel.Location = new System.Drawing.Point(3, 3);
            this.liniaProfiluMainPanel.Name = "liniaProfiluMainPanel";
            this.liniaProfiluMainPanel.Size = new System.Drawing.Size(670, 337);
            this.liniaProfiluMainPanel.TabIndex = 5;
            // 
            // radioButton3
            // 
            this.radioButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(581, 79);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(66, 17);
            this.radioButton3.TabIndex = 17;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Freeman";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label_max
            // 
            this.label_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_max.AutoSize = true;
            this.label_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_max.ForeColor = System.Drawing.Color.Blue;
            this.label_max.Location = new System.Drawing.Point(545, 8);
            this.label_max.Name = "label_max";
            this.label_max.Size = new System.Drawing.Size(14, 13);
            this.label_max.TabIndex = 15;
            this.label_max.Text = "_";
            this.label_max.LocationChanged += new System.EventHandler(this.label_max_LocationChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(581, 56);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 17);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "punktowy";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label_min
            // 
            this.label_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_min.AutoSize = true;
            this.label_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_min.ForeColor = System.Drawing.Color.Red;
            this.label_min.Location = new System.Drawing.Point(545, 238);
            this.label_min.Name = "label_min";
            this.label_min.Size = new System.Drawing.Size(14, 13);
            this.label_min.TabIndex = 16;
            this.label_min.Text = "_";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(581, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Rodzaj Wykresu";
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(581, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(56, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "liniowy";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // labelInstrukacja
            // 
            this.labelInstrukacja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInstrukacja.AutoSize = true;
            this.labelInstrukacja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInstrukacja.Location = new System.Drawing.Point(267, 322);
            this.labelInstrukacja.Name = "labelInstrukacja";
            this.labelInstrukacja.Size = new System.Drawing.Size(399, 13);
            this.labelInstrukacja.TabIndex = 6;
            this.labelInstrukacja.Text = "Naciśnij prawy przycisk myszy na obrazie i rusz myszką aby poprowadzić linię prof" +
    "ilu";
            // 
            // label_4
            // 
            this.label_4.AutoSize = true;
            this.label_4.Location = new System.Drawing.Point(9, 3);
            this.label_4.Name = "label_4";
            this.label_4.Size = new System.Drawing.Size(34, 13);
            this.label_4.TabIndex = 13;
            this.label_4.Text = "255 _";
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Location = new System.Drawing.Point(9, 131);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(34, 13);
            this.label_2.TabIndex = 12;
            this.label_2.Text = "127 _";
            // 
            // label_0
            // 
            this.label_0.AutoSize = true;
            this.label_0.Location = new System.Drawing.Point(21, 238);
            this.label_0.Name = "label_0";
            this.label_0.Size = new System.Drawing.Size(22, 13);
            this.label_0.TabIndex = 8;
            this.label_0.Text = "0 _";
            // 
            // wykresProfiluPanel
            // 
            this.wykresProfiluPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wykresProfiluPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.wykresProfiluPanel.BackColor = System.Drawing.Color.Transparent;
            this.wykresProfiluPanel.Controls.Add(this.wykresProfiluPictureBox);
            this.wykresProfiluPanel.Location = new System.Drawing.Point(46, 8);
            this.wykresProfiluPanel.Name = "wykresProfiluPanel";
            this.wykresProfiluPanel.Size = new System.Drawing.Size(493, 243);
            this.wykresProfiluPanel.TabIndex = 7;
            // 
            // wykresProfiluPictureBox
            // 
            this.wykresProfiluPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.wykresProfiluPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wykresProfiluPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wykresProfiluPictureBox.Location = new System.Drawing.Point(0, 0);
            this.wykresProfiluPictureBox.Name = "wykresProfiluPictureBox";
            this.wykresProfiluPictureBox.Size = new System.Drawing.Size(493, 243);
            this.wykresProfiluPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.wykresProfiluPictureBox.TabIndex = 0;
            this.wykresProfiluPictureBox.TabStop = false;
            this.wykresProfiluPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.wykresProfiluPictureBox_MouseMove);
            // 
            // labelDlugosc
            // 
            this.labelDlugosc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDlugosc.AutoSize = true;
            this.labelDlugosc.Location = new System.Drawing.Point(920, 377);
            this.labelDlugosc.Name = "labelDlugosc";
            this.labelDlugosc.Size = new System.Drawing.Size(102, 13);
            this.labelDlugosc.TabIndex = 4;
            this.labelDlugosc.Text = " Długość linii profilu:";
            // 
            // toolTipXY
            // 
            this.toolTipXY.AutoPopDelay = 600000;
            this.toolTipXY.InitialDelay = 500;
            this.toolTipXY.ReshowDelay = 100;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomOut.Location = new System.Drawing.Point(238, 372);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 23);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.Text = "-";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomIn.Location = new System.Drawing.Point(267, 372);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 23);
            this.btnZoomIn.TabIndex = 3;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // lblZoomLvl
            // 
            this.lblZoomLvl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblZoomLvl.AutoSize = true;
            this.lblZoomLvl.Location = new System.Drawing.Point(12, 377);
            this.lblZoomLvl.Name = "lblZoomLvl";
            this.lblZoomLvl.Size = new System.Drawing.Size(89, 13);
            this.lblZoomLvl.TabIndex = 4;
            this.lblZoomLvl.Text = "Powiększenie: 1x";
            // 
            // btnZoomReset
            // 
            this.btnZoomReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomReset.Location = new System.Drawing.Point(296, 372);
            this.btnZoomReset.Name = "btnZoomReset";
            this.btnZoomReset.Size = new System.Drawing.Size(43, 23);
            this.btnZoomReset.TabIndex = 5;
            this.btnZoomReset.Text = "reset";
            this.btnZoomReset.UseVisualStyleBackColor = true;
            this.btnZoomReset.Click += new System.EventHandler(this.btnZoomReset_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoom.Location = new System.Drawing.Point(164, 372);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(43, 23);
            this.btnZoom.TabIndex = 6;
            this.btnZoom.Text = "ustaw";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // txtZoom
            // 
            this.txtZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtZoom.Location = new System.Drawing.Point(128, 374);
            this.txtZoom.MaxLength = 2;
            this.txtZoom.Name = "txtZoom";
            this.txtZoom.Size = new System.Drawing.Size(30, 20);
            this.txtZoom.TabIndex = 7;
            this.txtZoom.Text = "5";
            this.txtZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkCut
            // 
            this.chkCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCut.AutoSize = true;
            this.chkCut.Checked = true;
            this.chkCut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCut.Location = new System.Drawing.Point(369, 376);
            this.chkCut.Name = "chkCut";
            this.chkCut.Size = new System.Drawing.Size(114, 17);
            this.chkCut.TabIndex = 1;
            this.chkCut.Text = "wytnij zaznaczenie";
            this.chkCut.UseVisualStyleBackColor = true;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1065, 396);
            this.Controls.Add(this.chkCut);
            this.Controls.Add(this.txtZoom);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnZoomReset);
            this.Controls.Add(this.labelDlugosc);
            this.Controls.Add(this.lblZoomLvl);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(650, 250);
            this.Name = "ImageForm";
            this.Text = "Image";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageForm_FormClosed);
            this.Resize += new System.EventHandler(this.ImageForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageObraz.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabLiniaProfilu.ResumeLayout(false);
            this.tabLiniaProfilu.PerformLayout();
            this.liniaProfiluMainPanel.ResumeLayout(false);
            this.liniaProfiluMainPanel.PerformLayout();
            this.wykresProfiluPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wykresProfiluPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageObraz;
        private System.Windows.Forms.TabPage tabPageTablica;
        private BufferedPanel graphicsPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageHistogram;
        private System.Windows.Forms.TabPage tabLiniaProfilu;
        private System.Windows.Forms.Label labelInstrukacja;
        private System.Windows.Forms.Panel liniaProfiluMainPanel;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label_4;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.Label label_0;
        private System.Windows.Forms.Panel wykresProfiluPanel;
        private System.Windows.Forms.PictureBox wykresProfiluPictureBox;
        private System.Windows.Forms.Label labelDlugosc;
        private System.Windows.Forms.Label label_max;
        private System.Windows.Forms.Label label_min;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTipXY;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Label lblZoomLvl;
        private System.Windows.Forms.Button btnZoomReset;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.TextBox txtZoom;
        private System.Windows.Forms.CheckBox chkCut;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}