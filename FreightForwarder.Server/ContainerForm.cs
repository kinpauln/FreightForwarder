using FreightForwarder.Business;
using FreightForwarder.Common;
using FreightForwarder.Data;
using FreightForwarder.Domain.Entities;
using FreightForwarder.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder
{
    public partial class ContainerForm : Form
    {
        FrmStart _load = null;
        private Server.Properties.Settings _defaultSettings;
        private FreightForwarder.Server.FFWCF.FFServiceClient _service = new Server.FFWCF.FFServiceClient();

        public ContainerForm()
        {
            _load = new FrmStart();
            _load.TopMost = true;
            _load.Show();

            InitializeComponent();
            _defaultSettings = Server.Properties.Settings.Default;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterParent;
            string dbconnString = _defaultSettings.FFDBContext;
            //MessageBox.Show(dbconnString);
            //FreightForwarder.Server.DBHelper.GetEntries();

            Thread.Sleep(2000);
            if (_load != null)
            {
                _load.Close();
                this.Focus();
            }

            if (!ValidateSoft())
            {
                return;
            }

            panelContainer.Visible = true;
            panelContainer.SendToBack();
            ShowSingleWindow(typeof(MainForm), FormWindowState.Maximized);

        }

        private bool ValidateSoft()
        {
            string machineCode = CommonTool.GetMachineCode();
            RegisterCode rc = _service.IsRegistered(machineCode);
            //RegisterCode rc = BusinessBase.IsRegistered(machineCode);
            if (rc == null)
            {
                MessageBox.Show(string.Format("还没注册，请将机器码{0}发给软件供应商，购买注册码后才能使用。", machineCode));
                return false;
            }
            else
            {
                //IIdentity _identity = new GenericIdentity(rc.RegCode);
                //IPrincipal _principal = new GenericPrincipal(_identity, new string[] { "管理员" });
                //Thread.CurrentPrincipal = _principal;//将其附加到当前线程的CurrentPrincipal

                //将注册码信息Session 
                Session.CURRENT_SOFT = new RegisterCode()
                {
                    RegCode = rc.RegCode,
                    MachineCode = machineCode,
                    CompanyId = rc.CompanyId,
                    State = rc.State
                };

                return true;
            }
        }

        /// <summary>
        /// 确保child form实例只有一个
        /// </summary>
        private void ShowSingleWindow(Type type, FormWindowState windowState)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == type)
                {
                    f.Activate();
                    return;
                }
            }

            Form frm = type.Assembly.CreateInstance(type.ToString()) as Form;
            frm.MdiParent = this;
            frm.WindowState = windowState;
            //frm.ControlBox = false;
            //frm.MaximizeBox = false;
            //frm.MinimizeBox = false;
            //frm.ShowIcon = false;
            //frm.Text = string.Empty;
            //frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void tsddBtnImport_Click(object sender, EventArgs e)
        {
            string theFile;
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.Filter = "Excel Files(*.xls)|*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                theFile = openFileDialog1.FileName;
                bool result = (new ServerBusinesses()).ImportExcelData(theFile);
                MessageBox.Show(result.ToString());
            }
        }
        FormProgressBar frmProgress = new FormProgressBar();
        delegate void dShowForm();
        public SetProgessBarEventHandler frmSetProgressBar;
        private void tlspBtnExport_Click(object sender, EventArgs e)
        {
            //string localFilePath, fileNameExt, newFileName, FilePath;   
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型   
            sfd.Filter = "Excel Files(*.xls)|*.xls";
            //设置默认文件类型显示顺序   
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录
            sfd.RestoreDirectory = true;
            //点了保存按钮进入   
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径 
                string localFilePath = sfd.FileName.ToString();
                //获取文件名，不带路径
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                //获取文件路径，不带文件名   
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                //System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件 


                //ProgressBarHelper pBarHelper = new ProgressBarHelper();
                ////此处使用了一个匿名函数，当然也可以才要满足要求的函数(BindProgessBarEventHandler的实例)
                //pBarHelper.BindProgressBar = new BindProgessBarEventHandler(new Action<SetProgessBarEventHandler>((esb) =>
                //{
                //    int maxVal = 9999;
                //    int currentVal = 1;
                //    string txt = "";
                //    for (int i = 0; i < 10000; i++)
                //    {
                //        txt = String.Format("当前的值为：{0}", i);
                //        ProgressBarUpdateEventArgs args = new ProgressBarUpdateEventArgs() { 
                //            MaxValue = maxVal,
                //            CurrentValue = currentVal++,
                //            DisplayText = txt
                //        };
                //        esb.Invoke(args);
                //    }
                //})); 
                //pBarHelper.Start();

                Thread showFormThread = new Thread(new System.Threading.ThreadStart(ShowProgressForm));
                showFormThread.IsBackground = true;
                showFormThread.Start();

                //Thread setBarThread = new System.Threading.Thread(new System.Threading.ThreadStart(new Action(() =>
                //{
                //    ClientBusinesses cb = new ClientBusinesses();
                //    cb.UpdateProgessBarEvent += new SetProgessBarEventHandler(new Action<ProgressBarUpdateEventArgs>((args) =>
                //    {
                //        frmProgress.SetProgressBar(args);
                //    }));
                //    cb.ExportExcel(localFilePath);
                //})));
                //setBarThread.IsBackground = true;
                //setBarThread.Start();

                ClientBusinesses cb = new ClientBusinesses();
                cb.UpdateProgessBarEvent += new SetProgessBarEventHandler(new Action<ProgressBarUpdateEventArgs>((args) =>
                {
                    frmProgress.SetProgressBar(args);
                }));
                cb.ExportExcel(localFilePath);
            }
        }

        // 控制进度 
        void Export(string localFilePath)
        {
            ClientBusinesses cb = new ClientBusinesses();
            cb.UpdateProgessBarEvent += new SetProgessBarEventHandler(new Action<ProgressBarUpdateEventArgs>((args) =>
            {
                frmProgress.SetProgressBar(args);
            }));
            cb.ExportExcel(localFilePath);
            MessageBox.Show("导出成功！");
        }

        // 控制进度 
        void SetProgress(ProgressBarUpdateEventArgs e)
        {
            frmProgress.SetProgressBar(e);
            System.Threading.Thread.Sleep(50);
        }

        void ShowProgressForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new dShowForm(this.ShowProgressForm));
            }
            else
            {
                frmProgress.ShowDialog(this);
            }
        }

        private void toolStripMenuItemSearch_Click(object sender, EventArgs e)
        {
            ShowSingleWindow(typeof(MainForm), FormWindowState.Maximized);
        }

        private void tsItemBtnAddCompany_Click(object sender, EventArgs e)
        {
            //ShowSingleWindow(typeof(AddCompanyForm));
            AddCompanyForm addForm = new AddCompanyForm();
            addForm.StartPosition = FormStartPosition.CenterParent;
            addForm.ShowDialog(this);
        }

        private void tsItemBtnRegCode_Click(object sender, EventArgs e)
        {
            //ShowSingleWindow(typeof(RegCodeForm));
            RegCodeForm regForm = new RegCodeForm();
            regForm.StartPosition = FormStartPosition.CenterParent;
            regForm.ShowDialog(this);
        }

        private void ContainerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread.CurrentPrincipal = null;
        }
    }
}
