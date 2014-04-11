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

namespace FreightForwarder.UI.Winform
{
    public partial class FrmAddCompany : Form
    {
        private FFWCF.FFServiceClient _service = new FFWCF.FFServiceClient();
        private Thread _addCompanieThread = null;

        public FrmAddCompany()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtCompanyName.Text.Trim();
            string code = txtCompanyCode.Text.Trim();
            ServerBusinesses sb = new ServerBusinesses();

            _addCompanieThread = new Thread(() =>
            {
                this.Invoke(new Action(() =>
                {
                    btnSave.Enabled = false;
                    if (_service.AddCompany(name,code))
                    {
                        btnSave.Visible = true;
                        UserUtils.ShowInfo("添加公司成功");
                    }
                    else
                    {
                        UserUtils.ShowError("添加公司失败");
                    }

                    btnSave.Enabled = true;
                }));
            });
            _addCompanieThread.IsBackground = true;
            _addCompanieThread.Start();
        }
    }
}
