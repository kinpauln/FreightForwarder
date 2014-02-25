using FreightForwarder.Business;
using FreightForwarder.Common;
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
    public partial class FrmRegCodeViewer : Form
    {
        public FrmRegCodeViewer()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mcode = txtMachineCode.Text.Trim();
            if (string.IsNullOrEmpty(mcode))
            {
                UserUtils.ShowError("机器码不能为空");
                txtMachineCode.Focus();
                return;
            }

            try
            {
                lblRegCode.Text = CommonTool.GetRegCode(mcode);
                btnCopy.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.Error("注册码查看失败", ex);
                UserUtils.ShowError("请输入合法的机器码！");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblRegCode.Text, true);
            UserUtils.ShowInfo("复制成功！");
        }

        private void FrmRegCodeViewer_Load(object sender, EventArgs e)
        {
            btnCopy.Visible = false;
        }
    }
}
