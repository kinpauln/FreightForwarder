using FreightForwarder.Business;
using FreightForwarder.Common;
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
    public partial class FrmMain : Form
    {
        private FFWCF.FFServiceClient _service = null;
        private FrmUnStateProgressBar formProgressBar = null;
        private Thread threadSearch = null;

        public FrmMain()
        {
            InitializeComponent();
            _service = new FFWCF.FFServiceClient();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            picBoxLoading.Visible = true;
            btnSearch.Enabled = false;

            threadSearch = new Thread(new ThreadStart(new Action(() =>
            {
                IList<RouteInformationItem> rlist = new List<RouteInformationItem>();

                string shipName = txtShipName.Text.Trim();
                string startPort = txtStartPort.Text.Trim();
                string destinationPort = txtDestinationPort.Text.Trim();
                bool? isSingleContainer = null;
                if (rbtnIsNotSingleContainer.Checked)
                {
                    isSingleContainer = false;
                }
                else if (rbtnIsSingleContainer.Checked)
                {
                    isSingleContainer = true;
                }

                rlist = _service.GetRoutItems(shipName, startPort, destinationPort, isSingleContainer);
                //rlist = BusinessBase.GetRoutItems(shipName, startPort, destinationPort, isSingleContainer);
                Thread.Sleep(5000);
                this.Invoke(new Action(() =>
                {
                    gvRoutItems.AutoGenerateColumns = false;
                    gvRoutItems.DataSource = rlist;

                    picBoxLoading.Visible = false;
                    btnSearch.Enabled = true;
                }));

                CloseProgressForm();
            })));
            threadSearch.IsBackground = true;
            threadSearch.Start();

            OpenProgressForm("正在检索，耐心等待。。。", threadSearch, true);
        }

        private void OpenProgressForm(string displayInfo, Thread thread, bool showCancel = false)
        {
            if (formProgressBar == null)
            {
                formProgressBar = new FrmUnStateProgressBar();
            }

            formProgressBar.DisplayInfo = displayInfo;
            formProgressBar.ShowCancel = showCancel;
            System.Windows.Forms.DialogResult dresult = formProgressBar.ShowDialog();
            if (dresult == System.Windows.Forms.DialogResult.Cancel)
            {
                thread.Abort();
            }
        }

        private void CloseProgressForm()
        {
            this.Invoke(new Action(() =>
            {
                if (formProgressBar != null)
                {
                    this.Invoke(new Action(() =>
                    {
                        formProgressBar.Close();
                    })
                    );
                }
            }));
        }

        private void gvRoutItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gvRoutItems.Columns[e.ColumnIndex].Name == "IsNostopString")
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow therow = gvRoutItems.Rows[rowIndex];

                SailNonstopValues nonStopEnumValue = (SailNonstopValues)((int)therow.Cells["Nonstop"].Value);
                e.Value = nonStopEnumValue.GetDescription();

                DataGridViewCell thecell = therow.Cells["IsNostopString"];
                switch (nonStopEnumValue)
                {
                    case SailNonstopValues.Yes:
                        thecell.Style = new DataGridViewCellStyle()
                        {
                            ForeColor = Color.Blue
                        };
                        break;
                    case SailNonstopValues.No:
                        thecell.Style = new DataGridViewCellStyle()
                        {
                            ForeColor = Color.Green
                        };
                        break;
                    case SailNonstopValues.Unknow:
                        thecell.Style = new DataGridViewCellStyle()
                        {
                            ForeColor = Color.Red
                        };
                        break;
                }
            }

            if (gvRoutItems.Columns[e.ColumnIndex].Name == "IsSingleContainerString")
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow therow = gvRoutItems.Rows[rowIndex];

                IsSingleContainerValues isSingleContainerEnumValue = (IsSingleContainerValues)((int)therow.Cells["IsSingleContainer"].Value);
                e.Value = isSingleContainerEnumValue.GetDescription();

                DataGridViewCell thecell = therow.Cells["IsSingleContainerString"];
                switch (isSingleContainerEnumValue)
                {
                    case IsSingleContainerValues.Yes:
                        thecell.Style = new DataGridViewCellStyle()
                        {
                            ForeColor = Color.Blue
                        };
                        break;
                    case IsSingleContainerValues.No:
                        thecell.Style = new DataGridViewCellStyle()
                        {
                            ForeColor = Color.Green
                        };
                        break;
                    case IsSingleContainerValues.Unknow:
                        thecell.Style = new DataGridViewCellStyle()
                        {
                            ForeColor = Color.Red
                        };
                        break;
                }
            }
        }
    }
}
