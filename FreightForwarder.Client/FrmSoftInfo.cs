using FreightForwarder.Business;
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
    public partial class FrmSoftInfo : Form
    {
        private Thread _initialInfoThread = null;

        public FrmSoftInfo()
        {
            InitializeComponent();
        }

        private void FrmSoftInfo_Load(object sender, EventArgs e)
        {
            _initialInfoThread = new Thread(() =>
            {
                this.Invoke(new Action(() =>
                {
                    lblMachineCode.Text = "初始化中。。。";
                    lblRegCode.Text = "初始化中。。。";
                    btnCopyMachineCode.Enabled = false;

                    string machineCode = string.Empty;
                    string regCode = string.Empty;

                    lblMachineCode.Text = CommonTool.GetMachineCode();
                    lblRegCode.Text = (Session.CURRENT_SOFT == null || string.IsNullOrEmpty(Session.CURRENT_SOFT.RegCode)) ? "尚未注册" : Session.CURRENT_SOFT.RegCode;

                    btnCopyMachineCode.Enabled = true;
                }));
            });
            _initialInfoThread.IsBackground = true;
            _initialInfoThread.Start();
        }

        private void btnCopyMachineCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblMachineCode.Text, true);
            UserUtils.ShowInfo("复制成功！");
        }
    }
}
