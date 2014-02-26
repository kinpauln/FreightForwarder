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
    public partial class FrmSoftInfo : Form
    {
        public FrmSoftInfo()
        {
            InitializeComponent();
        }

        private void FrmSoftInfo_Load(object sender, EventArgs e)
        {
            string machineCode = string.Empty;
            string regCode = string.Empty;

            lblMachineCode.Text = CommonTool.GetMachineCode();
            lblRegCode.Text = (Session.CURRENT_SOFT == null || string.IsNullOrEmpty(Session.CURRENT_SOFT.RegCode)) ? "商未注册" : Session.CURRENT_SOFT.RegCode;
        }

        private void btnCopyMachineCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblMachineCode.Text, true);
            UserUtils.ShowInfo("复制成功！");
        }
    }
}
