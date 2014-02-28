using FreightForwarder.Business;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Client
{
    public partial class FrmRegCode : Form
    {
        private FreightForwarder.Client.FFWCF.FFServiceClient _service = new Client.FFWCF.FFServiceClient();

        public FrmRegCode()
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
            btnCopy.Visible = false;
            lblCompanyInfo.Text = "正在初始化货代公司信息。。。";
            btnRegCode.Enabled = false;
            
            Thread initialCompaniesThread = new Thread(() =>
            {
                ServerBusinesses sb = new ServerBusinesses();
                IEnumerable<Company> companies = sb.GetAllCompanies();
                this.Invoke(new Action(() =>
                {
                    cbBoxCompanies.DataSource = companies;
                    cbBoxCompanies.ValueMember = "ID";
                    cbBoxCompanies.DisplayMember = "Name";

                    lblCompanyInfo.Text = string.Empty;
                    lblCompanyInfo.Visible = false;
                    btnRegCode.Enabled = true;
                }));
            });
            initialCompaniesThread.IsBackground = true;
            initialCompaniesThread.Start();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblRegCode.Text, true);
            UserUtils.ShowInfo("复制成功！");
        }
    }
}
