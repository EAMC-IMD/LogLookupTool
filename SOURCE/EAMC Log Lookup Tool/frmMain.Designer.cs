namespace EAMC_Log_Lookup_Tool
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnUser = new System.Windows.Forms.Button();
            this.btnEUD = new System.Windows.Forms.Button();
            this.btnExportHardwareList = new System.Windows.Forms.Button();
            this.btnExportInventoryData = new System.Windows.Forms.Button();
            this.eUDLoggingDataSet = new EAMC_Log_Lookup_Tool.EUDLoggingDataSet();
            this.vwHardwareListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vwHardwareListTableAdapter = new EAMC_Log_Lookup_Tool.EUDLoggingDataSetTableAdapters.vwHardwareListTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwHardwareListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(33, 37);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(140, 56);
            this.btnUser.TabIndex = 0;
            this.btnUser.Text = "User Lookup";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnEUD
            // 
            this.btnEUD.Location = new System.Drawing.Point(196, 37);
            this.btnEUD.Name = "btnEUD";
            this.btnEUD.Size = new System.Drawing.Size(140, 56);
            this.btnEUD.TabIndex = 1;
            this.btnEUD.Text = "EUD Lookup";
            this.btnEUD.UseVisualStyleBackColor = true;
            this.btnEUD.Click += new System.EventHandler(this.btnEUD_Click);
            // 
            // btnExportHardwareList
            // 
            this.btnExportHardwareList.Location = new System.Drawing.Point(196, 137);
            this.btnExportHardwareList.Name = "btnExportHardwareList";
            this.btnExportHardwareList.Size = new System.Drawing.Size(140, 56);
            this.btnExportHardwareList.TabIndex = 2;
            this.btnExportHardwareList.Text = "Export Full Hardware List";
            this.btnExportHardwareList.UseVisualStyleBackColor = true;
            this.btnExportHardwareList.Click += new System.EventHandler(this.btnExportHardwareList_Click);
            // 
            // btnExportInventoryData
            // 
            this.btnExportInventoryData.Location = new System.Drawing.Point(33, 137);
            this.btnExportInventoryData.Name = "btnExportInventoryData";
            this.btnExportInventoryData.Size = new System.Drawing.Size(140, 56);
            this.btnExportInventoryData.TabIndex = 3;
            this.btnExportInventoryData.Text = "Export Inventory Data";
            this.btnExportInventoryData.UseVisualStyleBackColor = true;
            this.btnExportInventoryData.Click += new System.EventHandler(this.btnExportInventoryData_Click);
            // 
            // eUDLoggingDataSet
            // 
            this.eUDLoggingDataSet.DataSetName = "EUDLoggingDataSet";
            this.eUDLoggingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vwHardwareListBindingSource
            // 
            this.vwHardwareListBindingSource.DataMember = "vwHardwareList";
            this.vwHardwareListBindingSource.DataSource = this.eUDLoggingDataSet;
            // 
            // vwHardwareListTableAdapter
            // 
            this.vwHardwareListTableAdapter.ClearBeforeFill = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 240);
            this.Controls.Add(this.btnExportInventoryData);
            this.Controls.Add(this.btnExportHardwareList);
            this.Controls.Add(this.btnEUD);
            this.Controls.Add(this.btnUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "EAMC Log Lookup Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwHardwareListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnEUD;
        private System.Windows.Forms.Button btnExportHardwareList;
        private EUDLoggingDataSet eUDLoggingDataSet;
        private System.Windows.Forms.BindingSource vwHardwareListBindingSource;
        private EUDLoggingDataSetTableAdapters.vwHardwareListTableAdapter vwHardwareListTableAdapter;
        private System.Windows.Forms.Button btnExportInventoryData;
    }
}

