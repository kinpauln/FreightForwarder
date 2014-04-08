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
    public partial class FrmStart : Form
    {
        public string StatusInfo = string.Empty;
        private Properties.Settings _settings = Properties.Settings.Default;

        public FrmStart()
        {
            InitializeComponent();
            webBrowser1.Navigate(new Uri(string.Format("{0}{1}", _settings.ServerUrl, _settings.NotifyUrl), false));
        }

        private void FrmStart_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = StatusInfo;
        }
    }
}
