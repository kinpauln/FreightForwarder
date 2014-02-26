using FreightForwarder.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Client
{
    public partial class AddCompanyForm : Form
    {
        public AddCompanyForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtCompanyName.Text.Trim();
            string code = txtCompanyCode.Text.Trim();
            ServerBusinesses sb = new ServerBusinesses();
            bool result = sb.AddCompany(name, code);
            UserUtils.ShowError(result.ToString());
        }
    }
}
