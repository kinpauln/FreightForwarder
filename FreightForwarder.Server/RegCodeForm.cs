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
    public partial class RegCodeForm : Form
    {
        private FreightForwarder.Server.FFWCF.FFServiceClient _service = new Server.FFWCF.FFServiceClient();

        public RegCodeForm()
        {
            InitializeComponent();
        }

        private void btnRegCode_Click(object sender, EventArgs e)
        {
            string mcode = CommonTool.GetMachineCode();
            string regCode = CommonTool.GetRegCode(mcode);
            int companyId = (int)cbBoxCompanies.SelectedValue;

            if (_service.AddMachineCode(mcode, companyId))
            {
                lblRegCode.Text = regCode;
                btnCopy.Visible = true;
            }
            else
                UserUtils.ShowError("生成注册码失败");

        }

        private void RegCodeForm_Load(object sender, EventArgs e)
        {
            ServerBusinesses sb = new ServerBusinesses();
            IEnumerable<Company> companies = sb.GetAllCompanies();
            cbBoxCompanies.DataSource = companies;
            cbBoxCompanies.ValueMember = "ID";
            cbBoxCompanies.DisplayMember = "Name";

            btnCopy.Visible = false;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblRegCode.Text, true);
            UserUtils.ShowInfo("复制成功！");
        }
    }
}
