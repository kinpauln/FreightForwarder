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

namespace FreightForwarder.Server
{
    public partial class MainForm : Form
    {
        private FreightForwarder.Server.FFWCF.FFServiceClient _service = null;

        public MainForm()
        {
            InitializeComponent();
            _service = new Server.FFWCF.FFServiceClient();
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

            rlist = _service.GetRoutItems(shipName, startPort, destinationPort, isSingleContainer);
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

        #region ProgressBar

        //2.1 声明 3 个类的成员变量

        /// <summary>
        /// 进度条窗体
        /// </summary>
        private FrmProgres m_frmProgres = null;
        /// <summary>
        /// 控制进度条进度的代理
        /// </summary>
        /// <returns></returns>
        private delegate bool DelIncreaseHandle(int value);
        /// <summary>
        /// 控制进度条进度的代理变量
        /// </summary>
        private DelIncreaseHandle m_ctrlProgresIncrease = null;



        //2.2 编写一个打开进度条窗体的方法，如果要使用进度条时调一下就 OK 了

        /// <summary>
        /// 显示进度条
        /// </summary>
        /// <param name="maxiNum">进度条最大值</param>
        /// <param name="miniNum">进度条最小值</param>
        private void ShowProgress(int miniNum, int maxiNum)
        {
            Thread thread = null;
            //1 打开进度条窗体
            Action showDialogProgress = (() =>
            {
                m_frmProgres = new FrmProgres();
                m_frmProgres.FormClosed += delegate(object sender, FormClosedEventArgs e)
                {

                    //添加这行代码会引发异常
                    //if (null != thread)  //如果进度条界面关闭了，就终止打开窗体的进程
                    //  thread.Abort();
                };
                m_ctrlProgresIncrease = new DelIncreaseHandle(m_frmProgres.Increase);
                m_frmProgres.ProgersMinimum = miniNum;  //设置进度条的最小值
                m_frmProgres.ProgersMaximum = maxiNum;//设置进度条的最大值
                m_frmProgres.StartPosition = FormStartPosition.CenterScreen; //进度条窗体打开的位置
                m_frmProgres.ShowDialog();  //以模式状态打开进度条窗体

                m_ctrlProgresIncrease = null;
                m_frmProgres = null;
            });

            //2 启动线程
            thread = new Thread(new ThreadStart(showDialogProgress));
            thread.IsBackground = false;  // 测试的时候这个东西似乎有点儿影响，没搞明白具体意思呢！
            thread.Start();
            /*
             * 使线程睡眠一会儿，给窗口展现预留时间
             * 窗体不展现前 m_ctrlProgresIncrease 为 null 
             * 将导致使用 this.Invoke 调用 m_ctrlProgresIncrease 委托时页面无限等待
             */
            Thread.Sleep(1000);
        } 
        #endregion

        /// <summary>
        /// 测试
        /// </summary>
        private void btnTest_Click(object sender, EventArgs e)
        {
            int miniMun = 0;
            int maxiNum = 100;
            this.ShowProgress(miniMun, maxiNum);  // 打开模式进度条窗口
            for (int i = miniMun; i <= maxiNum; i++)
            {
                if (null != m_ctrlProgresIncrease)
                    this.BeginInvoke(m_ctrlProgresIncrease, i); // 设置进度条的值

                Thread.Sleep(10);
            }
            if (null != m_ctrlProgresIncrease)
                this.BeginInvoke(m_ctrlProgresIncrease, -1);
        }
    }
}
