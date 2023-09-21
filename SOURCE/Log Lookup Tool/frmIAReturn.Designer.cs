
namespace Log_Lookup_Tool {
    partial class frmIAReturn {
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
            this.txtSNSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSNSearch = new System.Windows.Forms.Button();
            this.btnUserSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserSearch = new System.Windows.Forms.TextBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.issuance_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.badge_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.badge_serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issued_to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issue_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.return_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iaccess_status_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iaccesslogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eUDCustody = new Log_Lookup_Tool.EUDCustody();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnLost = new System.Windows.Forms.Button();
            this.btnFound = new System.Windows.Forms.Button();
            this.custodyTableAdapter = new Log_Lookup_Tool.EUDCustodyTableAdapters.CustodyTableAdapter();
            this.custodyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnReturnedToDHA = new System.Windows.Forms.Button();
            this.iaccess_logTableAdapter = new Log_Lookup_Tool.EUDCustodyTableAdapters.iaccess_logTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iaccesslogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDCustody)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.custodyBindingSource)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search by Badge";
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
            this.issuance_id,
            this.badge_id,
            this.badge_serial,
            this.CustomerName,
            this.issued_to,
            this.issue_date,
            this.return_date,
            this.iaccess_status_desc});
            this.dgvResults.DataSource = this.iaccesslogBindingSource;
            this.dgvResults.Location = new System.Drawing.Point(226, 13);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(958, 236);
            this.dgvResults.TabIndex = 6;
            this.dgvResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResults_CellFormatting);
            // 
            // issuance_id
            // 
            this.issuance_id.DataPropertyName = "issuance_id";
            this.issuance_id.HeaderText = "CustodyID";
            this.issuance_id.Name = "issuance_id";
            this.issuance_id.ReadOnly = true;
            this.issuance_id.Visible = false;
            // 
            // badge_id
            // 
            this.badge_id.DataPropertyName = "badge_id";
            this.badge_id.HeaderText = "badge_id";
            this.badge_id.Name = "badge_id";
            this.badge_id.Visible = false;
            // 
            // badge_serial
            // 
            this.badge_serial.DataPropertyName = "badge_serial";
            this.badge_serial.HeaderText = "Badge Serial";
            this.badge_serial.Name = "badge_serial";
            // 
            // CustomerName
            // 
            this.CustomerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CustomerName.HeaderText = "Issued To (Name)";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Visible = false;
            // 
            // issued_to
            // 
            this.issued_to.DataPropertyName = "issued_to";
            this.issued_to.HeaderText = "Issued To (DoDID)";
            this.issued_to.Name = "issued_to";
            // 
            // issue_date
            // 
            this.issue_date.DataPropertyName = "issue_date";
            this.issue_date.HeaderText = "Issue Date";
            this.issue_date.Name = "issue_date";
            // 
            // return_date
            // 
            this.return_date.DataPropertyName = "return_date";
            this.return_date.HeaderText = "Return Date";
            this.return_date.Name = "return_date";
            // 
            // iaccess_status_desc
            // 
            this.iaccess_status_desc.DataPropertyName = "iaccess_status_desc";
            this.iaccess_status_desc.HeaderText = "Badge Status";
            this.iaccess_status_desc.Name = "iaccess_status_desc";
            // 
            // iaccesslogBindingSource
            // 
            this.iaccesslogBindingSource.DataMember = "iaccess_log";
            this.iaccesslogBindingSource.DataSource = this.eUDCustody;
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
            this.btnReturn.Text = "Badge Returned to IMD";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnLost
            // 
            this.btnLost.Location = new System.Drawing.Point(406, 264);
            this.btnLost.Name = "btnLost";
            this.btnLost.Size = new System.Drawing.Size(108, 40);
            this.btnLost.TabIndex = 8;
            this.btnLost.Text = "Mark Badge Lost";
            this.btnLost.UseVisualStyleBackColor = true;
            this.btnLost.Click += new System.EventHandler(this.btnLost_Click);
            // 
            // btnFound
            // 
            this.btnFound.Location = new System.Drawing.Point(586, 264);
            this.btnFound.Name = "btnFound";
            this.btnFound.Size = new System.Drawing.Size(108, 40);
            this.btnFound.TabIndex = 9;
            this.btnFound.Text = "Mark Badge Found";
            this.btnFound.UseVisualStyleBackColor = true;
            this.btnFound.Click += new System.EventHandler(this.btnFound_Click);
            // 
            // custodyTableAdapter
            // 
            this.custodyTableAdapter.ClearBeforeFill = true;
            // 
            // custodyBindingSource
            // 
            this.custodyBindingSource.DataMember = "Custody";
            this.custodyBindingSource.DataSource = this.eUDCustody;
            // 
            // btnReturnedToDHA
            // 
            this.btnReturnedToDHA.Location = new System.Drawing.Point(766, 264);
            this.btnReturnedToDHA.Name = "btnReturnedToDHA";
            this.btnReturnedToDHA.Size = new System.Drawing.Size(108, 40);
            this.btnReturnedToDHA.TabIndex = 10;
            this.btnReturnedToDHA.Text = "Mark Badge Returned to DHA";
            this.btnReturnedToDHA.UseVisualStyleBackColor = true;
            this.btnReturnedToDHA.Click += new System.EventHandler(this.btnReturnedToDHA_Click);
            // 
            // iaccess_logTableAdapter
            // 
            this.iaccess_logTableAdapter.ClearBeforeFill = true;
            // 
            // frmIAReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 325);
            this.Controls.Add(this.btnReturnedToDHA);
            this.Controls.Add(this.btnFound);
            this.Controls.Add(this.btnLost);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btnUserSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserSearch);
            this.Controls.Add(this.btnSNSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSNSearch);
            this.Name = "frmIAReturn";
            this.Text = "Badge Turn-In";
            this.Load += new System.EventHandler(this.frmIAReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iaccesslogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDCustody)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.custodyBindingSource)).EndInit();
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
        private EUDCustodyTableAdapters.CustodyTableAdapter custodyTableAdapter;
        private EUDCustody eUDCustody;
        private System.Windows.Forms.BindingSource custodyBindingSource;
        private System.Windows.Forms.Button btnLost;
        private System.Windows.Forms.Button btnFound;
        private System.Windows.Forms.DataGridViewTextBoxColumn issuance_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn badge_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn badge_serial;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn issued_to;
        private System.Windows.Forms.DataGridViewTextBoxColumn issue_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn return_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn iaccess_status_desc;
        private System.Windows.Forms.Button btnReturnedToDHA;
        private System.Windows.Forms.BindingSource iaccesslogBindingSource;
        private EUDCustodyTableAdapters.iaccess_logTableAdapter iaccess_logTableAdapter;
    }
}