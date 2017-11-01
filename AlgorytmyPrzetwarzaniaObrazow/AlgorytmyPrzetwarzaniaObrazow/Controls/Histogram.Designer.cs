namespace AlgorytmyPrzetwarzaniaObrazow.Controls
{
    partial class Histogram
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.labelPos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLevels = new System.Windows.Forms.Label();
            this.bufferedPanel = new BufferedPanel();
            this.SuspendLayout();
            // 
            // labelPos
            // 
            this.labelPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.labelPos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPos.Location = new System.Drawing.Point(284, 39);
            this.labelPos.Name = "labelPos";
            this.labelPos.Size = new System.Drawing.Size(60, 23);
            this.labelPos.TabIndex = 2;
            this.labelPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(284, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "kolor :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(284, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ilość pix. :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelValue
            // 
            this.labelValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.labelValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelValue.Location = new System.Drawing.Point(284, 104);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(60, 23);
            this.labelValue.TabIndex = 4;
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(284, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Lmax :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLevels
            // 
            this.labelLevels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.labelLevels.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelLevels.Location = new System.Drawing.Point(284, 226);
            this.labelLevels.Name = "labelLevels";
            this.labelLevels.Size = new System.Drawing.Size(60, 23);
            this.labelLevels.TabIndex = 7;
            this.labelLevels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bufferedPanel
            // 
            this.bufferedPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bufferedPanel.Location = new System.Drawing.Point(0, 0);
            this.bufferedPanel.Name = "bufferedPanel";
            this.bufferedPanel.Size = new System.Drawing.Size(278, 255);
            this.bufferedPanel.TabIndex = 6;
            this.bufferedPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.bufferedPanel_Paint);
            this.bufferedPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel_MouseDown);
            this.bufferedPanel.MouseLeave += new System.EventHandler(this.bufferedPanel_Leave);
            this.bufferedPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel_MouseMove);
            this.bufferedPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel_MouseUp);
            // 
            // Histogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelLevels);
            this.Controls.Add(this.bufferedPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPos);
            this.DoubleBuffered = true;
            this.Name = "Histogram";
            this.Size = new System.Drawing.Size(356, 355);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label labelPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelValue;
        private BufferedPanel bufferedPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLevels;
    }
}
