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
                //UserUtils.ShowError("生成注册码失败！");
                throw ex;
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
