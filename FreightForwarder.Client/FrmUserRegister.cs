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
    public partial class FrmUserRegister : Form
    {
        private FreightForwarder.Client.FFWCF.FFServiceClient _service = new Client.FFWCF.FFServiceClient();
        private Thread _registerThread = null;

        public FrmUserRegister()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            _registerThread = new Thread(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btnReg.Enabled = false;

                    StringBuilder sb = new StringBuilder();
                    sb.Append(txtPart1.Text.Trim());
                    sb.Append("-");
                    sb.Append(txtPart2.Text.Trim());
                    sb.Append("-");
                    sb.Append(txtPart3.Text.Trim());
                    sb.Append("-");
                    sb.Append(txtPart4.Text.Trim());
                    string regcode = sb.ToString();

                    bool result = _service.AssociatMachineAndRegCode(Session.CURRENT_SOFT.MachineCode, regcode, Session.CURRENT_SOFT.CompanyId);
                    if (result)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                        this.Close();
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.No;
                        this.Close();
                    }
                }));
            });
            _registerThread.IsBackground = true;
            _registerThread.Start();
        }

        private void txtPart1_KeyDown(object sender, KeyEventArgs e)
        {
            //// 组合键
            //if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)         //Ctrl+F1
            //{
            //    string clipboardContent = GetContextFromClipboard();
            //    if (string.IsNullOrEmpty(clipboardContent)) return;
            //    string[] contentSegment = clipboardContent.Split(new char[] { '-' });
            //    txtPart1.Text = contentSegment[0];
            //}
        }

        private string GetContextFromClipboard()
        {
            // GetDataObject检索当前剪贴板上的数据
            IDataObject iData = Clipboard.GetDataObject();
            // 将数据与指定的格式进行匹配，返回bool
            if (iData.GetDataPresent(DataFormats.Text))
            {
                // GetData检索数据并指定一个格式
                string content = (string)iData.GetData(DataFormats.Text);
                return content;
            }
            else
            {
                return null;
            }
        }

        private void FrmUserRegister_Click(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}
