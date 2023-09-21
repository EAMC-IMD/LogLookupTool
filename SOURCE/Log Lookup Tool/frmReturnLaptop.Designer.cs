
namespace Log_Lookup_Tool {
    partial class frmReturnLaptop {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSNSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSNSearch = new System.Windows.Forms.Button();
            this.btnUserSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserSearch = new System.Windows.Forms.TextBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.custodyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eUDCustody = new Log_Lookup_Tool.EUDCustody();
            this.btnReturn = new System.Windows.Forms.Button();
            this.custodyTableAdapter = new Log_Lookup_Tool.EUDCustodyTableAdapters.CustodyTableAdapter();
            this.btnReturnAlt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReturnAlt = new System.Windows.Forms.TextBox();
            this.CustodyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoDID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Received = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Returned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickupDoDID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullScan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickupFullScan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.custodyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDCustody)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSNSearch
            // 
            this.txtSNSearch.Location = new System.Drawing.Point(16, 29);
            this.txtSNSearch.Name = "txtSNSearch";
            this.txtSNSearch.Size = new System.Drawing.Size(111, 20);
            this.txtSNSearch.TabIndex = 0;
            this.txtSNSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSNSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search by SN";
            // 
            // btnSNSearch
            // 
            this.btnSNSearch.Location = new System.Drawing.Point(133, 29);
            this.btnSNSearch.Name = "btnSNSearch";
            this.btnSNSearch.Size = new System.Drawing.Size(22, 20);
            this.btnSNSearch.TabIndex = 2;
            this.btnSNSearch.UseVisualStyleBackColor = true;
            this.btnSNSearch.Click += new System.EventHandler(this.btnSNSearch_Click);
            // 
            // btnUserSearch
            // 
            this.btnUserSearch.Location = new System.Drawing.Point(133, 100);
            this.btnUserSearch.Name = "btnUserSearch";
            this.btnUserSearch.Size = new System.Drawing.Size(22, 20);
            this.btnUserSearch.TabIndex = 5;
            this.btnUserSearch.UseVisualStyleBackColor = true;
            this.btnUserSearch.Click += new System.EventHandler(this.btnUserSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search by DoDID/CAC Scan";
            // 
            // txtUserSearch
            // 
            this.txtUserSearch.Location = new System.Drawing.Point(16, 100);
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.Size = new System.Drawing.Size(111, 20);
            this.txtUserSearch.TabIndex = 3;
            this.txtUserSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserSearch_KeyDown);
            // 
            // dgvResults
            // 
            this.dgvResults.AutoGenerateColumns = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustodyID,
            this.SerialNumber,
            this.DoDID,
            this.CustomerName,
            this.Received,
            this.ReceivedBy,
            this.Returned,
            this.ReturnedBy,
            this.PickupDoDID,
            this.PickupName,
            this.FullScan,
            this.PickupFullScan});
            this.dgvResults.DataSource = this.custodyBindingSource;
            this.dgvResults.Location = new System.Drawing.Point(226, 13);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(958, 236);
            this.dgvResults.TabIndex = 6;
            this.dgvResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResults_CellFormatting);
            // 
            // custodyBindingSource
            // 
            this.custodyBindingSource.DataMember = "Custody";
            this.custodyBindingSource.DataSource = this.eUDCustody;
            // 
            // eUDCustody
            // 
            this.eUDCustody.DataSetName = "EUDCustody";
            this.eUDCustody.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(226, 264);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(108, 40);
            this.btnReturn.TabIndex = 7;
            this.btnReturn.Text = "Return EUD to Original Customer";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // custodyTableAdapter
            // 
            this.custodyTableAdapter.ClearBeforeFill = true;
            // 
            // btnReturnAlt
            // 
            this.btnReturnAlt.Location = new System.Drawing.Point(723, 264);
            this.btnReturnAlt.Name = "btnReturnAlt";
            this.btnReturnAlt.Size = new System.Drawing.Size(108, 40);
            this.btnReturnAlt.TabIndex = 8;
            this.btnReturnAlt.Text = "Return EUD to Different Customer";
            this.btnReturnAlt.UseVisualStyleBackColor = true;
            this.btnReturnAlt.Click += new System.EventHandler(this.btnReturnAlt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(530, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Pickup Customer DoDID or CAC Scan";
            // 
            // txtReturnAlt
            // 
            this.txtReturnAlt.Location = new System.Drawing.Point(533, 284);
            this.txtReturnAlt.Name = "txtReturnAlt";
            this.txtReturnAlt.Size = new System.Drawing.Size(111, 20);
            this.txtReturnAlt.TabIndex = 9;
            this.txtReturnAlt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnAlt_KeyDown);
            // 
            // CustodyID
            // 
            this.CustodyID.DataPropertyName = "CustodyID";
            this.CustodyID.HeaderText = "CustodyID";
            this.CustodyID.Name = "CustodyID";
            this.CustodyID.ReadOnly = true;
            this.CustodyID.Visible = false;
            // 
            // SerialNumber
            // 
            this.SerialNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "Serial Number";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.Width = 98;
            // 
            // DoDID
            // 
            this.DoDID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DoDID.DataPropertyName = "DoDID";
            this.DoDID.HeaderText = "DoDID";
            this.DoDID.Name = "DoDID";
            this.DoDID.ReadOnly = true;
            this.DoDID.Width = 65;
            // 
            // CustomerName
            // 
            this.CustomerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 98;
            // 
            // Received
            // 
            this.Received.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Received.DataPropertyName = "Received";
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.Received.DefaultCellStyle = dataGridViewCellStyle1;
            this.Received.HeaderText = "Received";
            this.Received.Name = "Received";
            this.Received.ReadOnly = true;
            this.Received.Width = 78;
            // 
            // ReceivedBy
            // 
            this.ReceivedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ReceivedBy.DataPropertyName = "ReceivedBy";
            this.ReceivedBy.HeaderText = "Received By";
            this.ReceivedBy.Name = "ReceivedBy";
            this.ReceivedBy.ReadOnly = true;
            this.ReceivedBy.Width = 86;
            // 
            // Returned
            // 
            this.Returned.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Returned.DataPropertyName = "Returned";
            dataGridViewCellStyle2.Format = "g";
            this.Returned.DefaultCellStyle = dataGridViewCellStyle2;
            this.Returned.HeaderText = "Returned";
            this.Returned.Name = "Returned";
            this.Returned.ReadOnly = true;
            this.Returned.Width = 76;
            // 
            // ReturnedBy
            // 
            this.ReturnedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ReturnedBy.DataPropertyName = "ReturnedBy";
            this.ReturnedBy.HeaderText = "Returned By";
            this.ReturnedBy.Name = "ReturnedBy";
            this.ReturnedBy.ReadOnly = true;
            this.ReturnedBy.Width = 84;
            // 
            // PickupDoDID
            // 
            this.PickupDoDID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PickupDoDID.DataPropertyName = "PickupDoDID";
            this.PickupDoDID.HeaderText = "Returned To (DoDID)";
            this.PickupDoDID.Name = "PickupDoDID";
            this.PickupDoDID.ReadOnly = true;
            this.PickupDoDID.Width = 87;
            // 
            // PickupName
            // 
            this.PickupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PickupName.HeaderText = "Returned To (Name)";
            this.PickupName.Name = "PickupName";
            this.PickupName.ReadOnly = true;
            this.PickupName.Width = 87;
            // 
            // FullScan
            // 
            this.FullScan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FullScan.DataPropertyName = "FullScan";
            this.FullScan.HeaderText = "FullScan";
            this.FullScan.Name = "FullScan";
            this.FullScan.Visible = false;
            this.FullScan.Width = 73;
            // 
            // PickupFullScan
            // 
            this.PickupFullScan.DataPropertyName = "PickupFullScan";
            this.PickupFullScan.HeaderText = "PickupFullScan";
            this.PickupFullScan.Name = "PickupFullScan";
            this.PickupFullScan.ReadOnly = true;
            this.PickupFullScan.Visible = false;
            // 
            // frmReturnLaptop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 325);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReturnAlt);
            this.Controls.Add(this.btnReturnAlt);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btnUserSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserSearch);
            this.Controls.Add(this.btnSNSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSNSearch);
            this.Name = "frmReturnLaptop";
            this.Text = "Return EUD to User";
            this.Load += new System.EventHandler(this.frmReturnLaptop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.custodyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDCustody)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSNSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSNSearch;
        private System.Windows.Forms.Button btnUserSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserSearch;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Button btnReturn;
        private EUDCustody eUDCustody;
        private System.Windows.Forms.BindingSource custodyBindingSource;
        private EUDCustodyTableAdapters.CustodyTableAdapter custodyTableAdapter;
        private System.Windows.Forms.Button btnReturnAlt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReturnAlt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustodyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoDID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Received;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Returned;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickupDoDID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullScan;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickupFullScan;
    }
}