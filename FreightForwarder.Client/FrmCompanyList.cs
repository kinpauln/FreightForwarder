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
    public partial class FrmCompanyList : Form
    {
        private FFWCF.FFServiceClient _service = new FFWCF.FFServiceClient();
        private Thread _initialCompaniesThread = null;
        private Thread _regCodeThread = null;

        public FrmCompanyList()
        {
            InitializeComponent();
        }

        private void FrmCompanyList_Load(object sender, EventArgs e)
        {
            _initialCompaniesThread = new Thread(() =>
            {
                this.Invoke(new Action(() =>
                {
                    dgvCompanies.AutoGenerateColumns = false;
                    dgvCompanies.DataSource = _service.GetAllCompanyList();
                }));
            });
            _initialCompaniesThread.IsBackground = true;
            _initialCompaniesThread.Start();
        }
    }
}
