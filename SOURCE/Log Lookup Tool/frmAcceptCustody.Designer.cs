
namespace Log_Lookup_Tool {
    partial class frmAcceptCustody {
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
            this.txtCACScan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLaptopScan = new System.Windows.Forms.TextBox();
            this.lblLaptopInfo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCACScan
            // 
            this.txtCACScan.Location = new System.Drawing.Point(15, 25);
            this.txtCACScan.Name = "txtCACScan";
            this.txtCACScan.Size = new System.Drawing.Size(113, 20);
            this.txtCACScan.TabIndex = 0;
            this.txtCACScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCACScan_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CAC Scan (or DoDID if CAC not available)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "User:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(64, 57);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 20);
            this.lblUserName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "EUD Scan";
            // 
            // txtLaptopScan
            // 
            this.txtLaptopScan.Location = new System.Drawing.Point(18, 125);
            this.txtLaptopScan.Name = "txtLaptopScan";
            this.txtLaptopScan.Size = new System.Drawing.Size(100, 20);
            this.txtLaptopScan.TabIndex = 5;
            this.txtLaptopScan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLaptopScan_KeyDown);
            // 
            // lblLaptopInfo
            // 
            this.lblLaptopInfo.AutoSize = true;
            this.lblLaptopInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLaptopInfo.Location = new System.Drawing.Point(71, 159);
            this.lblLaptopInfo.Name = "lblLaptopInfo";
            this.lblLaptopInfo.Size = new System.Drawing.Size(29, 20);
            this.lblLaptopInfo.TabIndex = 7;
            this.lblLaptopInfo.Text = "----";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "EUD:";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(100, 210);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 37);
            this.btnAccept.TabIndex = 8;
            this.btnAccept.Text = "Accept EUD";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // frmAcceptCustody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 262);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblLaptopInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLaptopScan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCACScan);
            this.Name = "frmAcceptCustody";
            this.Text = "EUD Acceptance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCACScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLaptopScan;
        private System.Windows.Forms.Label lblLaptopInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAccept;
    }
}