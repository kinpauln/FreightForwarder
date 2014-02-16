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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Server
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
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

            rlist = BusinessBase.GetRoutItems(shipName, startPort, destinationPort, isSingleContainer);
            gvRoutItems.AutoGenerateColumns = false;
            gvRoutItems.DataSource = rlist;
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
                switch (isSingleContainerEnumValue) { 
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
