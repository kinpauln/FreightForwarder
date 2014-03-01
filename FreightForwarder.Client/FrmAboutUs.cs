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
    public partial class FrmAboutUs : Form
    {
        public FrmAboutUs()
        {
            InitializeComponent();
        }

        private void FrmAboutUs_Load(object sender, EventArgs e)
        {
            Properties.Settings settings = Properties.Settings.Default;
            lblEmail.Text = settings.AboutUs_Email;
            lblQQ.Text = settings.AboutUs_QQ;
            lblQQGroup.Text = settings.AboutUs_QQGroup;
            lblTel.Text = settings.AboutUs_Telephone;
        }
    }
}
