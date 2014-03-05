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

namespace FreightForwarder.UI.Winform
{
    public partial class FrmRegCode : Form
    {
        private FFWCF.FFServiceClient _service = new FFWCF.FFServiceClient();
        private Thread _initialCompaniesThread = null;
        private Thread _regCodeThread = null;

        public FrmRegCode()
        {
            InitializeComponent();
        }

        private void btnRegCode_Click(object sender, EventArgs e)
        {
            string mcode = txtMachineCode.Text.Trim();
            if (string.IsNullOrEmpty(mcode))
            {
                UserUtils.ShowError("机器码不能为空");
                return;
            }

            if (mcode.Length != 24)
            {
                UserUtils.ShowError("请输入合法的机器码");
                return;
            }

            _regCodeThread = new Thread(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btnRegCode.Enabled = false;
                    string regCode = CommonTool.GetRegCode(mcode);
                    int companyId = (int)cbBoxCompanies.SelectedValue;
                    if (_service.AddMachineCode(mcode, companyId))
                    {
                        lblRegCode.Text = regCode;
                        btnCopy.Visible = true;
                        //if (lblRegCode.InvokeRequired && btnCopy.InvokeRequired)
                        //{
                        //    lblRegCode.Text = regCode;
                        //    btnCopy.Visible = true;
                        //}
                    }
                    else
                    {
                        UserUtils.ShowError("生成注册码失败");
                    }

                    btnRegCode.Enabled = true;
                }));
            });
            _regCodeThread.IsBackground = true;
            _regCodeThread.Start();
        }

        private void RegCodeForm_Load(object sender, EventArgs e)
        {
            btnCopy.Visible = false;
            lblCompanyInfo.Text = "正在初始化货代公司信息。。。";
            btnRegCode.Enabled = false;

            _initialCompaniesThread = new Thread(() =>
            {
                IEnumerable<Company> companies = _service.GetAllCompanyList();
                if (cbBoxCompanies.InvokeRequired && lblCompanyInfo.InvokeRequired && btnRegCode.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        cbBoxCompanies.DataSource = companies;
                        cbBoxCompanies.ValueMember = "ID";
                        cbBoxCompanies.DisplayMember = "Name";

                        lblCompanyInfo.Text = string.Empty;
                        lblCompanyInfo.Visible = false;
                        btnRegCode.Enabled = true;
                    }));
                }
            });
            _initialCompaniesThread.IsBackground = true;
            _initialCompaniesThread.Start();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblRegCode.Text, true);
            UserUtils.ShowInfo("复制成功！");
        }
    }
}
