using FreightForwarder.Business;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Server
{
    public partial class RegCodeForm : BaseForm
    {
        public RegCodeForm()
        {
            InitializeComponent();
        }

        private void btnRegCode_Click(object sender, EventArgs e)
        {
            //string mcode = txtMachineCode.Text.Trim();
            string mcode = CommonTool.GetMachineCode();
            string regCode = CommonTool.GeRegCode(mcode);
            int companyId = (int)cbBoxCompanies.SelectedValue;
            ServerBusinesses sb = new ServerBusinesses();
            if (sb.RegCode(mcode, regCode, companyId))
                txtRegCode.Text = regCode;
            else
                MessageBox.Show("生成注册码失败");

        }

        private void RegCodeForm_Load(object sender, EventArgs e)
        {
            ServerBusinesses sb = new ServerBusinesses();
            IEnumerable<Company> companies = sb.GetAllCompanies();
            cbBoxCompanies.DataSource = companies;
            cbBoxCompanies.ValueMember = "ID";
            cbBoxCompanies.DisplayMember = "Name";
        }
    }
}
