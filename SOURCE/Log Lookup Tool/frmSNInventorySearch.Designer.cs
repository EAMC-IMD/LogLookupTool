namespace Log_Lookup_Tool {
    partial class frmSNInventorySearch {
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
            this.txtEcnList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateCSV = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ecnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggedOnUserSAMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vLANDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vLANDESCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastLogonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventoryDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eUDLoggingDataSet = new Log_Lookup_Tool.EUDLoggingDataSet();
            this.btnGenerateByECN = new System.Windows.Forms.Button();
            this.inventoryDataTableAdapter = new Log_Lookup_Tool.EUDLoggingDataSetTableAdapters.InventoryDataTableAdapter();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ecnDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNumberDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computernameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggedOnUserSAMDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vLANDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vLANDESCDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastLogonDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inventoryDataByECNBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inventoryDataByECNTableAdapter = new Log_Lookup_Tool.EUDLoggingDataSetTableAdapters.InventoryDataByECNTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataByECNBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEcnList
            // 
            this.txtEcnList.Location = new System.Drawing.Point(12, 29);
            this.txtEcnList.MaxLength = 4096;
            this.txtEcnList.Multiline = true;
            this.txtEcnList.Name = "txtEcnList";
            this.txtEcnList.Size = new System.Drawing.Size(410, 283);
            this.txtEcnList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter either ECNs or SNs separated by commas or new lines";
            // 
            // btnGenerateCSV
            // 
            this.btnGenerateCSV.Location = new System.Drawing.Point(428, 76);
            this.btnGenerateCSV.Name = "btnGenerateCSV";
            this.btnGenerateCSV.Size = new System.Drawing.Size(103, 55);
            this.btnGenerateCSV.TabIndex = 2;
            this.btnGenerateCSV.Text = "Generate Inventory Excel by SN";
            this.btnGenerateCSV.UseVisualStyleBackColor = true;
            this.btnGenerateCSV.Click += new System.EventHandler(this.btnGenerateCSV_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ecnDataGridViewTextBoxColumn,
            this.serialNumberDataGridViewTextBoxColumn,
            this.computernameDataGridViewTextBoxColumn,
            this.loggedOnUserSAMDataGridViewTextBoxColumn,
            this.vLANDataGridViewTextBoxColumn,
            this.vLANDESCDataGridViewTextBoxColumn,
            this.lastLogonDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.inventoryDataBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(479, 273);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(52, 26);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.Visible = false;
            // 
            // ecnDataGridViewTextBoxColumn
            // 
            this.ecnDataGridViewTextBoxColumn.DataPropertyName = "Ecn";
            this.ecnDataGridViewTextBoxColumn.HeaderText = "Ecn";
            this.ecnDataGridViewTextBoxColumn.Name = "ecnDataGridViewTextBoxColumn";
            this.ecnDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // serialNumberDataGridViewTextBoxColumn
            // 
            this.serialNumberDataGridViewTextBoxColumn.DataPropertyName = "SerialNumber";
            this.serialNumberDataGridViewTextBoxColumn.HeaderText = "SerialNumber";
            this.serialNumberDataGridViewTextBoxColumn.Name = "serialNumberDataGridViewTextBoxColumn";
            this.serialNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // computernameDataGridViewTextBoxColumn
            // 
            this.computernameDataGridViewTextBoxColumn.DataPropertyName = "computername";
            this.computernameDataGridViewTextBoxColumn.HeaderText = "computername";
            this.computernameDataGridViewTextBoxColumn.Name = "computernameDataGridViewTextBoxColumn";
            this.computernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loggedOnUserSAMDataGridViewTextBoxColumn
            // 
            this.loggedOnUserSAMDataGridViewTextBoxColumn.DataPropertyName = "loggedOnUserSAM";
            this.loggedOnUserSAMDataGridViewTextBoxColumn.HeaderText = "loggedOnUserSAM";
            this.loggedOnUserSAMDataGridViewTextBoxColumn.Name = "loggedOnUserSAMDataGridViewTextBoxColumn";
            this.loggedOnUserSAMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vLANDataGridViewTextBoxColumn
            // 
            this.vLANDataGridViewTextBoxColumn.DataPropertyName = "VLAN";
            this.vLANDataGridViewTextBoxColumn.HeaderText = "VLAN";
            this.vLANDataGridViewTextBoxColumn.Name = "vLANDataGridViewTextBoxColumn";
            this.vLANDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vLANDESCDataGridViewTextBoxColumn
            // 
            this.vLANDESCDataGridViewTextBoxColumn.DataPropertyName = "VLAN_DESC";
            this.vLANDESCDataGridViewTextBoxColumn.HeaderText = "VLAN_DESC";
            this.vLANDESCDataGridViewTextBoxColumn.Name = "vLANDESCDataGridViewTextBoxColumn";
            this.vLANDESCDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastLogonDataGridViewTextBoxColumn
            // 
            this.lastLogonDataGridViewTextBoxColumn.DataPropertyName = "lastLogon";
            this.lastLogonDataGridViewTextBoxColumn.HeaderText = "lastLogon";
            this.lastLogonDataGridViewTextBoxColumn.Name = "lastLogonDataGridViewTextBoxColumn";
            this.lastLogonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // inventoryDataBindingSource
            // 
            this.inventoryDataBindingSource.DataMember = "InventoryData";
            this.inventoryDataBindingSource.DataSource = this.eUDLoggingDataSet;
            // 
            // eUDLoggingDataSet
            // 
            this.eUDLoggingDataSet.DataSetName = "EUDLoggingDataSet";
            this.eUDLoggingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnGenerateByECN
            // 
            this.btnGenerateByECN.Location = new System.Drawing.Point(428, 167);
            this.btnGenerateByECN.Name = "btnGenerateByECN";
            this.btnGenerateByECN.Size = new System.Drawing.Size(103, 55);
            this.btnGenerateByECN.TabIndex = 4;
            this.btnGenerateByECN.Text = "Generate Inventory Excel by ECN";
            this.btnGenerateByECN.UseVisualStyleBackColor = true;
            this.btnGenerateByECN.Click += new System.EventHandler(this.btnGenerateByECN_Click);
            // 
            // inventoryDataTableAdapter
            // 
            this.inventoryDataTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ecnDataGridViewTextBoxColumn1,
            this.serialNumberDataGridViewTextBoxColumn1,
            this.computernameDataGridViewTextBoxColumn1,
            this.loggedOnUserSAMDataGridViewTextBoxColumn1,
            this.vLANDataGridViewTextBoxColumn1,
            this.vLANDESCDataGridViewTextBoxColumn1,
            this.lastLogonDataGridViewTextBoxColumn1});
            this.dataGridView2.DataSource = this.inventoryDataByECNBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(454, 246);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(30, 21);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.Visible = false;
            // 
            // ecnDataGridViewTextBoxColumn1
            // 
            this.ecnDataGridViewTextBoxColumn1.DataPropertyName = "Ecn";
            this.ecnDataGridViewTextBoxColumn1.HeaderText = "Ecn";
            this.ecnDataGridViewTextBoxColumn1.Name = "ecnDataGridViewTextBoxColumn1";
            this.ecnDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // serialNumberDataGridViewTextBoxColumn1
            // 
            this.serialNumberDataGridViewTextBoxColumn1.DataPropertyName = "SerialNumber";
            this.serialNumberDataGridViewTextBoxColumn1.HeaderText = "SerialNumber";
            this.serialNumberDataGridViewTextBoxColumn1.Name = "serialNumberDataGridViewTextBoxColumn1";
            this.serialNumberDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // computernameDataGridViewTextBoxColumn1
            // 
            this.computernameDataGridViewTextBoxColumn1.DataPropertyName = "computername";
            this.computernameDataGridViewTextBoxColumn1.HeaderText = "computername";
            this.computernameDataGridViewTextBoxColumn1.Name = "computernameDataGridViewTextBoxColumn1";
            this.computernameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // loggedOnUserSAMDataGridViewTextBoxColumn1
            // 
            this.loggedOnUserSAMDataGridViewTextBoxColumn1.DataPropertyName = "loggedOnUserSAM";
            this.loggedOnUserSAMDataGridViewTextBoxColumn1.HeaderText = "loggedOnUserSAM";
            this.loggedOnUserSAMDataGridViewTextBoxColumn1.Name = "loggedOnUserSAMDataGridViewTextBoxColumn1";
            this.loggedOnUserSAMDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // vLANDataGridViewTextBoxColumn1
            // 
            this.vLANDataGridViewTextBoxColumn1.DataPropertyName = "VLAN";
            this.vLANDataGridViewTextBoxColumn1.HeaderText = "VLAN";
            this.vLANDataGridViewTextBoxColumn1.Name = "vLANDataGridViewTextBoxColumn1";
            this.vLANDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // vLANDESCDataGridViewTextBoxColumn1
            // 
            this.vLANDESCDataGridViewTextBoxColumn1.DataPropertyName = "VLAN_DESC";
            this.vLANDESCDataGridViewTextBoxColumn1.HeaderText = "VLAN_DESC";
            this.vLANDESCDataGridViewTextBoxColumn1.Name = "vLANDESCDataGridViewTextBoxColumn1";
            this.vLANDESCDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // lastLogonDataGridViewTextBoxColumn1
            // 
            this.lastLogonDataGridViewTextBoxColumn1.DataPropertyName = "lastLogon";
            this.lastLogonDataGridViewTextBoxColumn1.HeaderText = "lastLogon";
            this.lastLogonDataGridViewTextBoxColumn1.Name = "lastLogonDataGridViewTextBoxColumn1";
            this.lastLogonDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // inventoryDataByECNBindingSource
            // 
            this.inventoryDataByECNBindingSource.DataMember = "InventoryDataByECN";
            this.inventoryDataByECNBindingSource.DataSource = this.eUDLoggingDataSet;
            // 
            // inventoryDataByECNTableAdapter
            // 
            this.inventoryDataByECNTableAdapter.ClearBeforeFill = true;
            // 
            // frmSNInventorySearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 321);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnGenerateByECN);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGenerateCSV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEcnList);
            this.Name = "frmSNInventorySearch";
            this.Text = "Inventory Export";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inventoryDataByECNBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEcnList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateCSV;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ecnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn computernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loggedOnUserSAMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vLANDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vLANDESCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastLogonDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource inventoryDataBindingSource;
        private EUDLoggingDataSet eUDLoggingDataSet;
        private EUDLoggingDataSetTableAdapters.InventoryDataTableAdapter inventoryDataTableAdapter;
        private System.Windows.Forms.Button btnGenerateByECN;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ecnDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNumberDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn computernameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn loggedOnUserSAMDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn vLANDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn vLANDESCDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastLogonDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource inventoryDataByECNBindingSource;
        private EUDLoggingDataSetTableAdapters.InventoryDataByECNTableAdapter inventoryDataByECNTableAdapter;
    }
}