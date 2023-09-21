namespace Log_Lookup_Tool {
    partial class frmDisplayUserAD {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplayUserAD));
            this.txtADInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtADInfo
            // 
            this.txtADInfo.Location = new System.Drawing.Point(12, 12);
            this.txtADInfo.Multiline = true;
            this.txtADInfo.Name = "txtADInfo";
            this.txtADInfo.ReadOnly = true;
            this.txtADInfo.Size = new System.Drawing.Size(1003, 314);
            this.txtADInfo.TabIndex = 0;
            // 
            // frmDisplayUserAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 338);
            this.Controls.Add(this.txtADInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDisplayUserAD";
            this.Text = "Obejct AD data";
            this.Shown += new System.EventHandler(this.frmDisplayUserAD_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtADInfo;
    }
}