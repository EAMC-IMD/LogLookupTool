using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log_Lookup_Tool {
    public partial class TextRequest : Form {
        private string _title = "";
        private string _message = "";
        private string _default = "";
        private string _input = "";
        public string BoxTitle => _title;
        public string BoxMessage => _message;
        public string DefaultInput => _default;
        public string ReturnValue { get => _input; }
        public TextRequest() {
            InitializeComponent();
        }
        public TextRequest(string Message) {
            InitializeComponent();
        }
        public TextRequest(string Title, string Message) {
            InitializeComponent();
        }
        public TextRequest(string Title, string Message, string DefaultInput) {
            InitializeComponent();
        }

        private void TextRequest_Shown(object sender, EventArgs e) {
            Text = _title;
            lblMessage.Text = _message;
            txtInput.Text = _default;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                btnOK_Click(txtInput, new EventArgs());
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            _input = txtInput.Text;
            Close();
        }
    }
}
