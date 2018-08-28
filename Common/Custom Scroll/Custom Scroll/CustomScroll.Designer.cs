namespace PhidgetsControlLibrary
{
    partial class CustomScroll
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.slider = new System.Windows.Forms.TrackBar();
            this.maxLbl = new System.Windows.Forms.Label();
            this.trkName = new System.Windows.Forms.Label();
            this.minLbl = new System.Windows.Forms.Label();
            this.trkTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.slider)).BeginInit();
            this.SuspendLayout();
            // 
            // slider
            // 
            this.slider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slider.Location = new System.Drawing.Point(83, -1);
            this.slider.Name = "slider";
            this.slider.Size = new System.Drawing.Size(138, 45);
            this.slider.TabIndex = 0;
            this.slider.Scroll += new System.EventHandler(this.slider_Scroll);
            // 
            // maxLbl
            // 
            this.maxLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.maxLbl.Location = new System.Drawing.Point(169, 26);
            this.maxLbl.Name = "maxLbl";
            this.maxLbl.Size = new System.Drawing.Size(98, 9);
            this.maxLbl.TabIndex = 34;
            this.maxLbl.Text = "max";
            this.maxLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // trkName
            // 
            this.trkName.Location = new System.Drawing.Point(-2, 9);
            this.trkName.Name = "trkName";
            this.trkName.Size = new System.Drawing.Size(88, 13);
            this.trkName.TabIndex = 31;
            this.trkName.Text = "Name";
            // 
            // minLbl
            // 
            this.minLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.minLbl.Location = new System.Drawing.Point(42, 26);
            this.minLbl.Name = "minLbl";
            this.minLbl.Size = new System.Drawing.Size(98, 9);
            this.minLbl.TabIndex = 33;
            this.minLbl.Text = "min";
            this.minLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // trkTxt
            // 
            this.trkTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trkTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trkTxt.Location = new System.Drawing.Point(227, 8);
            this.trkTxt.Name = "trkTxt";
            this.trkTxt.ReadOnly = true;
            this.trkTxt.Size = new System.Drawing.Size(69, 13);
            this.trkTxt.TabIndex = 32;
            this.trkTxt.TabStop = false;
            // 
            // CustomScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.trkName);
            this.Controls.Add(this.maxLbl);
            this.Controls.Add(this.minLbl);
            this.Controls.Add(this.trkTxt);
            this.Controls.Add(this.slider);
            this.Name = "CustomScroll";
            this.Size = new System.Drawing.Size(304, 37);
            ((System.ComponentModel.ISupportInitialize)(this.slider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TrackBar slider;
        private System.Windows.Forms.Label maxLbl;
        private System.Windows.Forms.Label trkName;
        private System.Windows.Forms.Label minLbl;
        private System.Windows.Forms.TextBox trkTxt;
    }
}
