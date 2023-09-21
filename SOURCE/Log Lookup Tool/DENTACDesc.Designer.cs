
namespace Log_Lookup_Tool {
    partial class DENTACDesc {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBldgDesc = new System.Windows.Forms.Label();
            this.txtBldgDesc = new System.Windows.Forms.TextBox();
            this.lblBldg = new System.Windows.Forms.Label();
            this.txtBldg = new System.Windows.Forms.TextBox();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbTDC = new System.Windows.Forms.RadioButton();
            this.rbSDC = new System.Windows.Forms.RadioButton();
            this.rbHDC = new System.Windows.Forms.RadioButton();
            this.rbHQ = new System.Windows.Forms.RadioButton();
            this.lblRoom = new System.Windows.Forms.Label();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.txtRoomDesc = new System.Windows.Forms.TextBox();
            this.lblRoomDesc = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBldgDesc);
            this.groupBox1.Controls.Add(this.txtBldgDesc);
            this.groupBox1.Controls.Add(this.lblBldg);
            this.groupBox1.Controls.Add(this.txtBldg);
            this.groupBox1.Controls.Add(this.rbOther);
            this.groupBox1.Controls.Add(this.rbTDC);
            this.groupBox1.Controls.Add(this.rbSDC);
            this.groupBox1.Controls.Add(this.rbHDC);
            this.groupBox1.Controls.Add(this.rbHQ);
            this.groupBox1.Location = new System.Drawing.Point(37, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clinic";
            // 
            // lblBldgDesc
            // 
            this.lblBldgDesc.AutoSize = true;
            this.lblBldgDesc.Enabled = false;
            this.lblBldgDesc.Location = new System.Drawing.Point(7, 166);
            this.lblBldgDesc.Name = "lblBldgDesc";
            this.lblBldgDesc.Size = new System.Drawing.Size(56, 13);
            this.lblBldgDesc.TabIndex = 7;
            this.lblBldgDesc.Text = "Bldg Desc";
            // 
            // txtBldgDesc
            // 
            this.txtBldgDesc.Enabled = false;
            this.txtBldgDesc.Location = new System.Drawing.Point(76, 163);
            this.txtBldgDesc.Name = "txtBldgDesc";
            this.txtBldgDesc.Size = new System.Drawing.Size(118, 20);
            this.txtBldgDesc.TabIndex = 6;
            // 
            // lblBldg
            // 
            this.lblBldg.AutoSize = true;
            this.lblBldg.Enabled = false;
            this.lblBldg.Location = new System.Drawing.Point(7, 140);
            this.lblBldg.Name = "lblBldg";
            this.lblBldg.Size = new System.Drawing.Size(38, 13);
            this.lblBldg.TabIndex = 5;
            this.lblBldg.Text = "Bldg #";
            // 
            // txtBldg
            // 
            this.txtBldg.Enabled = false;
            this.txtBldg.Location = new System.Drawing.Point(76, 137);
            this.txtBldg.Name = "txtBldg";
            this.txtBldg.Size = new System.Drawing.Size(59, 20);
            this.txtBldg.TabIndex = 1;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(7, 116);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(51, 17);
            this.rbOther.TabIndex = 4;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbClinic_CheckedChanged);
            // 
            // rbTDC
            // 
            this.rbTDC.AutoSize = true;
            this.rbTDC.Location = new System.Drawing.Point(7, 92);
            this.rbTDC.Name = "rbTDC";
            this.rbTDC.Size = new System.Drawing.Size(119, 17);
            this.rbTDC.TabIndex = 3;
            this.rbTDC.TabStop = true;
            this.rbTDC.Text = "Tingay Dental Clinic";
            this.rbTDC.UseVisualStyleBackColor = true;
            this.rbTDC.CheckedChanged += new System.EventHandler(this.rbClinic_CheckedChanged);
            // 
            // rbSDC
            // 
            this.rbSDC.AutoSize = true;
            this.rbSDC.Location = new System.Drawing.Point(7, 68);
            this.rbSDC.Name = "rbSDC";
            this.rbSDC.Size = new System.Drawing.Size(120, 17);
            this.rbSDC.TabIndex = 2;
            this.rbSDC.TabStop = true;
            this.rbSDC.Text = "Snyder Dental Clinic";
            this.rbSDC.UseVisualStyleBackColor = true;
            this.rbSDC.CheckedChanged += new System.EventHandler(this.rbClinic_CheckedChanged);
            // 
            // rbHDC
            // 
            this.rbHDC.AutoSize = true;
            this.rbHDC.Location = new System.Drawing.Point(7, 44);
            this.rbHDC.Name = "rbHDC";
            this.rbHDC.Size = new System.Drawing.Size(125, 17);
            this.rbHDC.TabIndex = 1;
            this.rbHDC.TabStop = true;
            this.rbHDC.Text = "Hospital Dental Clinic";
            this.rbHDC.UseVisualStyleBackColor = true;
            this.rbHDC.CheckedChanged += new System.EventHandler(this.rbClinic_CheckedChanged);
            // 
            // rbHQ
            // 
            this.rbHQ.AutoSize = true;
            this.rbHQ.Location = new System.Drawing.Point(7, 20);
            this.rbHQ.Name = "rbHQ";
            this.rbHQ.Size = new System.Drawing.Size(154, 17);
            this.rbHQ.TabIndex = 0;
            this.rbHQ.TabStop = true;
            this.rbHQ.Text = "HQs, Dental Health Activity";
            this.rbHQ.UseVisualStyleBackColor = true;
            this.rbHQ.CheckedChanged += new System.EventHandler(this.rbClinic_CheckedChanged);
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(44, 226);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(45, 13);
            this.lblRoom.TabIndex = 1;
            this.lblRoom.Text = "Room #";
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(113, 223);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(83, 20);
            this.txtRoom.TabIndex = 2;
            // 
            // txtRoomDesc
            // 
            this.txtRoomDesc.Location = new System.Drawing.Point(113, 253);
            this.txtRoomDesc.Name = "txtRoomDesc";
            this.txtRoomDesc.Size = new System.Drawing.Size(118, 20);
            this.txtRoomDesc.TabIndex = 4;
            // 
            // lblRoomDesc
            // 
            this.lblRoomDesc.AutoSize = true;
            this.lblRoomDesc.Location = new System.Drawing.Point(44, 256);
            this.lblRoomDesc.Name = "lblRoomDesc";
            this.lblRoomDesc.Size = new System.Drawing.Size(63, 13);
            this.lblRoomDesc.TabIndex = 3;
            this.lblRoomDesc.Text = "Room Desc";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(12, 292);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(104, 53);
            this.btnSet.TabIndex = 5;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(158, 292);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 53);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // DENTACDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 357);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.txtRoomDesc);
            this.Controls.Add(this.lblRoomDesc);
            this.Controls.Add(this.txtRoom);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.groupBox1);
            this.Name = "DENTACDesc";
            this.Text = "DENTAC Post-Image Data";
            this.Shown += new System.EventHandler(this.DENTACDesc_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBldg;
        private System.Windows.Forms.TextBox txtBldg;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbTDC;
        private System.Windows.Forms.RadioButton rbSDC;
        private System.Windows.Forms.RadioButton rbHDC;
        private System.Windows.Forms.RadioButton rbHQ;
        private System.Windows.Forms.Label lblBldgDesc;
        private System.Windows.Forms.TextBox txtBldgDesc;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.TextBox txtRoomDesc;
        private System.Windows.Forms.Label lblRoomDesc;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnClear;
    }
}