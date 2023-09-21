namespace Log_Lookup_Tool {
    partial class frmEUDStatInfo {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEUDStatInfo));
            this.tbxStatData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxStatData
            // 
            this.tbxStatData.Location = new System.Drawing.Point(13, 13);
            this.tbxStatData.Multiline = true;
            this.tbxStatData.Name = "tbxStatData";
            this.tbxStatData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxStatData.Size = new System.Drawing.Size(775, 425);
            this.tbxStatData.TabIndex = 0;
            // 
            // frmEUDStatInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbxStatData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEUDStatInfo";
            this.Text = "EUD Hardware Information";
            this.Shown += new System.EventHandler(this.frmEUDStatInfo_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxStatData;
    }
}