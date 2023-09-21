
namespace Log_Lookup_Tool {
    partial class frmInCustody {
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.custodyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eUDCustody = new Log_Lookup_Tool.EUDCustody();
            this.custodyTableAdapter = new Log_Lookup_Tool.EUDCustodyTableAdapters.CustodyTableAdapter();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoDID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Received = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullScanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custodyIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.custodyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDCustody)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNumber,
            this.DoDID,
            this.CustomerName,
            this.Received,
            this.ReceivedBy,
            this.fullScanDataGridViewTextBoxColumn,
            this.custodyIDDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.custodyBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(646, 341);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
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
            // custodyTableAdapter
            // 
            this.custodyTableAdapter.ClearBeforeFill = true;
            // 
            // SerialNumber
            // 
            this.SerialNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "SerialNumber";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.Width = 95;
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
            this.Received.HeaderText = "Received";
            this.Received.Name = "Received";
            this.Received.ReadOnly = true;
            this.Received.Width = 78;
            // 
            // ReceivedBy
            // 
            this.ReceivedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ReceivedBy.DataPropertyName = "ReceivedBy";
            this.ReceivedBy.HeaderText = "ReceivedBy";
            this.ReceivedBy.Name = "ReceivedBy";
            this.ReceivedBy.ReadOnly = true;
            this.ReceivedBy.Width = 90;
            // 
            // fullScanDataGridViewTextBoxColumn
            // 
            this.fullScanDataGridViewTextBoxColumn.DataPropertyName = "FullScan";
            this.fullScanDataGridViewTextBoxColumn.HeaderText = "FullScan";
            this.fullScanDataGridViewTextBoxColumn.Name = "fullScanDataGridViewTextBoxColumn";
            this.fullScanDataGridViewTextBoxColumn.Visible = false;
            // 
            // custodyIDDataGridViewTextBoxColumn
            // 
            this.custodyIDDataGridViewTextBoxColumn.DataPropertyName = "CustodyID";
            this.custodyIDDataGridViewTextBoxColumn.HeaderText = "CustodyID";
            this.custodyIDDataGridViewTextBoxColumn.Name = "custodyIDDataGridViewTextBoxColumn";
            this.custodyIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.custodyIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // frmInCustody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 359);
            this.Controls.Add(this.dataGridView);
            this.Name = "frmInCustody";
            this.Text = "EUDs In CSC Custody";
            this.Load += new System.EventHandler(this.frmInCustody_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.custodyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eUDCustody)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private EUDCustody eUDCustody;
        private System.Windows.Forms.BindingSource custodyBindingSource;
        private EUDCustodyTableAdapters.CustodyTableAdapter custodyTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoDID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Received;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullScanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn custodyIDDataGridViewTextBoxColumn;
    }
}