namespace EAMC_Log_Lookup_Tool {
    partial class frmShowLogs {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowLogs));
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.vwLoggingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EUDLoggingDataSet = new EAMC_Log_Lookup_Tool.EUDLoggingDataSet();
            this.vwLoggingTableAdapter = new EAMC_Log_Lookup_Tool.EUDLoggingDataSetTableAdapters.vwLoggingTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwLoggingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EUDLoggingDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLogs
            // 
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AllowUserToDeleteRows = false;
            this.dgvLogs.AllowUserToResizeRows = false;
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLogs.Location = new System.Drawing.Point(7, 6);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.ShowCellErrors = false;
            this.dgvLogs.ShowCellToolTips = false;
            this.dgvLogs.ShowEditingIcon = false;
            this.dgvLogs.ShowRowErrors = false;
            this.dgvLogs.Size = new System.Drawing.Size(853, 477);
            this.dgvLogs.TabIndex = 0;
            this.dgvLogs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogs_CellDoubleClick);
            // 
            // vwLoggingBindingSource
            // 
            this.vwLoggingBindingSource.DataSource = this.EUDLoggingDataSet;
            this.vwLoggingBindingSource.Position = 0;
            // 
            // EUDLoggingDataSet
            // 
            this.EUDLoggingDataSet.DataSetName = "EUDLoggingDataSet";
            this.EUDLoggingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vwLoggingTableAdapter
            // 
            this.vwLoggingTableAdapter.ClearBeforeFill = true;
            // 
            // frmShowLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 487);
            this.Controls.Add(this.dgvLogs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmShowLogs";
            this.Text = "Login Event Display";
            this.Load += new System.EventHandler(this.frmShowLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwLoggingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EUDLoggingDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.BindingSource vwLoggingBindingSource;
        private EUDLoggingDataSetTableAdapters.vwLoggingTableAdapter vwLoggingTableAdapter;
        private EUDLoggingDataSet EUDLoggingDataSet;
    }
}