using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.UI.Winform
{
    public partial class FrmUnStateProgressBar : Form
    {
        public string DisplayInfo = "正在执行，请耐心等待。。。。";
        public bool ShowCancel = true;

        public FrmUnStateProgressBar()
        {
            InitializeComponent();
        }

        private void FrmUnStateProgressBar_Load(object sender, EventArgs e)
        {
            label1.Text = DisplayInfo;

            btnCancel.Visible = ShowCancel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
