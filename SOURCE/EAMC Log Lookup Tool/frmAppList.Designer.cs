namespace EAMC_Log_Lookup_Tool {
    partial class frmAppList {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAppList));
            this.dgvAppList = new System.Windows.Forms.DataGridView();
            this.applicationnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timestampDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applicationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eUDLoggingDataSet = new EAMC_Log_Lookup_Tool.EUDLoggingDataSet();
            this.btnExport = new System.Windows.Forms.Button();
            this.applicationsTableAdapter = new EAMC_Log_Lookup_Tool.EUDLoggingDataSetTableAdapters.ApplicationsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAppList
            // 
            this.dgvAppList.AutoGenerateColumns = false;
            this.dgvAppList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAppList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.applicationnameDataGridViewTextBoxColumn,
            this.timestampDataGridViewTextBoxColumn});
            this.dgvAppList.DataSource = this.applicationsBindingSource;
            this.dgvAppList.Location = new System.Drawing.Point(12, 12);
            this.dgvAppList.Name = "dgvAppList";
            this.dgvAppList.Size = new System.Drawing.Size(794, 621);
            this.dgvAppList.TabIndex = 0;
            // 
            // applicationnameDataGridViewTextBoxColumn
            // 
            this.applicationnameDataGridViewTextBoxColumn.DataPropertyName = "applicationname";
            this.applicationnameDataGridViewTextBoxColumn.HeaderText = "applicationname";
            this.applicationnameDataGridViewTextBoxColumn.Name = "applicationnameDataGridViewTextBoxColumn";
            this.applicationnameDataGridViewTextBoxColumn.Width = 109;
            // 
            // timestampDataGridViewTextBoxColumn
            // 
            this.timestampDataGridViewTextBoxColumn.DataPropertyName = "timestamp";
            this.timestampDataGridViewTextBoxColumn.HeaderText = "timestamp";
            this.timestampDataGridViewTextBoxColumn.Name = "timestampDataGridViewTextBoxColumn";
            this.timestampDataGridViewTextBoxColumn.ReadOnly = true;
            this.timestampDataGridViewTextBoxColumn.Width = 79;
            // 
            // applicationsBindingSource
            // 
            this.applicationsBindingSource.DataMember = "Applications";
            this.applicationsBindingSource.DataSource = this.eUDLoggingDataSet;
            // 
            // eUDLoggingDataSet
            // 
            this.eUDLoggingDataSet.DataSetName = "EUDLoggingDataSet";
            this.eUDLoggingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(812, 280);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(85, 44);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export to CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // applicationsTableAdapter
            // 
            this.applicationsTableAdapter.ClearBeforeFill = true;
            // 
            // frmAppList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 645);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvAppList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAppList";
            this.Text = "Installed Applications on ";
            this.Load += new System.EventHandler(this.frmAppList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAppList;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn applicationnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timestampDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource applicationsBindingSource;
        private EUDLoggingDataSet eUDLoggingDataSet;
        private EUDLoggingDataSetTableAdapters.ApplicationsTableAdapter applicationsTableAdapter;
    }
}