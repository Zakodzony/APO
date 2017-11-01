using System;
using System.Windows.Forms;
using AlgorytmyPrzetwarzaniaObrazow.Controls;

namespace AlgorytmyPrzetwarzaniaObrazow.Dialogs
{
    partial class ComparisonDialog
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
            this.slider = new System.Windows.Forms.TrackBar();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelOrg = new BufferedPanel();
            this.panelNew = new BufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // slider
            // 
            this.slider.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.slider.LargeChange = 1;
            this.slider.Location = new System.Drawing.Point(141, 403);
            this.slider.Maximum = 0;
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(360, 45);
            this.slider.TabIndex = 2;
            this.slider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.slider.ValueChanged += new System.EventHandler(this.slider_ValueChanged);
            this.slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.slider_MouseUp);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(266, 430);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(347, 430);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox.Location = new System.Drawing.Point(497, 403);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(38, 20);
            this.textBox.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelOrg);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelNew);
            this.splitContainer1.Size = new System.Drawing.Size(711, 366);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 7;
            // 
            // panelOrg
            // 
            this.panelOrg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrg.AutoScroll = true;
            this.panelOrg.AutoSize = true;
            this.panelOrg.BackColor = System.Drawing.Color.Black;
            this.panelOrg.Location = new System.Drawing.Point(14, 15);
            this.panelOrg.Name = "panelOrg";
            this.panelOrg.Size = new System.Drawing.Size(320, 334);
            this.panelOrg.TabIndex = 0;
            this.panelOrg.Paint += new System.Windows.Forms.PaintEventHandler(this.panelOrg_Paint);
            // 
            // panelNew
            // 
            this.panelNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNew.AutoScroll = true;
            this.panelNew.AutoSize = true;
            this.panelNew.BackColor = System.Drawing.Color.Black;
            this.panelNew.Location = new System.Drawing.Point(15, 15);
            this.panelNew.Name = "panelNew";
            this.panelNew.Size = new System.Drawing.Size(321, 334);
            this.panelNew.TabIndex = 1;
            this.panelNew.Paint += new System.Windows.Forms.PaintEventHandler(this.panelNew_Paint);
            // 
            // BeforeAferDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 457);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.slider);
            this.Name = "BeforeAferDialog";
            this.Text = "BeforeAfterDialog";
            ((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar slider;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private BufferedPanel panelOrg;
        private BufferedPanel panelNew;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}