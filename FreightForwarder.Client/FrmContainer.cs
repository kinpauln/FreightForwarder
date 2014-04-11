using FreightForwarder.Business;
using FreightForwarder.Common;
using FreightForwarder.Data;
using FreightForwarder.Domain.Entities;
using FreightForwarder.UI.Winform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace FreightForwarder.UI.Winform
{
    public partial class FrmContainer : Form
    {
        private FrmStart _load = null;
        private FrmUnStateProgressBar formProgressBar;
        private delegate ValidateSoftInfoStruct ValidateSoftHandler();
        private Properties.Settings _defaultSettings;
        private FFWCF.FFServiceClient _service = new FFWCF.FFServiceClient();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FrmContainer()
        {
            InitializeComponent();
            foreach (Control c in this.Controls)
            {
                if (c is MdiClient)
                {
                    c.BackColor = this.BackColor;
                    c.BackgroundImage = this.BackgroundImage;
                }
            }
            _defaultSettings = Properties.Settings.Default;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;
            this.KeyPreview = true;

            //UserUtils.ShowError(dbconnString);
            //FreightForwarder.Client.DBHelper.GetEntries();

            (new FreightForwarder.Business.BusinessBase()).GetRouteInformationItems(null);

            if (Session.SOFT_VERSION_TYPE == SoftVersionType.Client)
            {
                //Thread validateThread = new Thread(new ParameterizedThreadStart(ValidateSoft));
                //validateThread.IsBackground = true;
                //validateThread.Start();

                toolStrip1.Visible = false;
                menuStrip1.Visible = false;
                _load = new FrmStart();
                _load.TopMost = true;
                _load.StatusInfo = "正在验证软件信息，请耐心等待。。。";
                _load.Show();

                ValidateSoftHandler vsh = new ValidateSoftHandler(ValidateSoft);
                vsh.BeginInvoke(new AsyncCallback(ValidateSoftComplete), null);

                //OpenProgressForm("正在验证软件信息，请耐心等待。", null);
            }

            if (Session.SOFT_VERSION_TYPE == SoftVersionType.Server)
            {
                this.Text = "货代Mini-服务端";
                toolStripStatusLblCompanyInfo.Text = "服务端";
                toolStripMenuItemSoftInfo.Visible = false;
                ShowSingleWindow(typeof(FrmMain), FormWindowState.Maximized);
            }
        }
        
        #region 客户端登陆验证
        struct ValidateSoftInfoStruct
        {
            public bool isValidSoft;
            public string errorString;
            public SoftState softState;
        }

        private ValidateSoftInfoStruct ValidateSoft()
        {
            bool checkResult = true;
            SoftState softState = SoftState.RegisterdAndActived;
            string machineCode = CommonTool.GetMachineCode();
            RegisterCode rc = _service.IsRegistered(machineCode);
            //RegisterCode entity = BusinessBase.IsRegistered(machineCode);

            string errorString = string.Empty;
            if (rc == null)
            {
                errorString = string.Format("还没注册，请联系软件供应商，并提供您的机器码，购买注册码后才能使用。");
                checkResult = false;
                softState = SoftState.UnRegisterd;

            }
            else
            {
                //将注册码信息Session 
                Session.CURRENT_SOFT = new RegisterCode()
                {
                    RegCode = rc.RegCode,
                    MachineCode = machineCode,
                    CompanyId = rc.Company.Id,
                    Company = rc.Company,
                    EndDate = rc.EndDate,
                    CreatedDate = rc.CreatedDate,
                    State = rc.State
                };

                // 机器码不为空，但注册码为空，说明还未注册
                if (string.IsNullOrEmpty(rc.RegCode))
                {
                    errorString = "您的软件尚未完成注册，请用软件商提供给您的注册码完成注册。如果忘记注册码，请向注册商提供机器码，重新获取直至完成注册。";
                    checkResult = false;
                    softState = SoftState.Registering;
                }

                // 数据库中保存的注册码与实际注册码不一致
                string trueRegCode = CommonTool.GetRegCode(machineCode);
                if (trueRegCode != rc.RegCode && !string.IsNullOrEmpty(rc.RegCode))
                {
                    errorString = "您之前完成的注册，填写的注册码有问题，详情请联系软件供应商。";
                    checkResult = false;
                    softState = SoftState.UnmatchedRegistered;
                }

                RegCodeStates softSate = (RegCodeStates)rc.State;
                if (softSate != RegCodeStates.Actived)
                {
                    errorString = string.Format("您的软件尚且不能使用，现在处于【{0}】的状态。", softSate.GetDescription());
                    checkResult = false;
                    softState = SoftState.RegisterdButUnactived;
                }


                if (checkResult)
                {
                    this.Invoke(new Action(() =>
                    {
                        ShowSingleWindow(typeof(FrmMain), FormWindowState.Maximized);
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        toolStripStatusLblCompanyInfo.Text = "未注册公司";
                    }));
                }
            }

            return new ValidateSoftInfoStruct() { isValidSoft = checkResult, errorString = errorString, softState = softState };

            //new Func<bool>(() => {
            //    return false;
            //}).BeginInvoke(new AsyncCallback(ValidateSoftComplete),null);
        }

        private void ValidateSoftComplete(IAsyncResult result)
        {

            ValidateSoftHandler handler = (ValidateSoftHandler)((AsyncResult)result).AsyncDelegate;
            ValidateSoftInfoStruct vsis = (ValidateSoftInfoStruct)handler.EndInvoke(result);

            if (vsis.isValidSoft)
            {
                this.Invoke(new Action(() =>
                {
                    //ShowBackForm();
                    _load.Close();

                    InitClientUI();
                }));
                //CloseProgressForm();
            }
            else
            {
                //CloseProgressForm();
                this.Invoke(new Action(() =>
                {
                    InitClientUI(true);
                    _load.Close();
                    UserUtils.ShowInfo(vsis.errorString);
                }));

                if (vsis.softState != SoftState.Registering && vsis.softState != SoftState.UnRegisterd)
                {
                    //彻底退出程序
                    System.Environment.Exit(0);
                }
            }

            //MessageBox.Show(handler.EndInvoke(result));
            //MessageBox.Show(result.AsyncState);
        } 
        #endregion

        private void DisableMenuBar(MenuStrip menu, bool disable)
        {
            foreach (ToolStripMenuItem curritem in menu.Items)
            {
                curritem.Enabled = !disable;
                DisableSubMenu(curritem, disable);
            }
        }

        private void DisableSubMenu(ToolStripMenuItem submenu, bool disable)
        {
            foreach (ToolStripMenuItem curritem in submenu.DropDownItems)
            {
                curritem.Enabled = !disable;
                DisableSubMenu(curritem, disable);
            }
        }

        private void DisableToolBar(ToolStrip toolbar, bool disable)
        {
            foreach (ToolStripItem curritem in toolbar.Items)
            {
                curritem.Enabled = !disable;
            }
        }

        private void InitClientUI(bool disable = false)
        {
            toolStrip1.Visible = true;
            menuStrip1.Visible = true;
            toolStripMenuItemCompanyMgr.Visible = false;

            this.Text = "货代Mini-客户端";

            toolStripMenuItemTool.Visible = false;
            tsddBtnImport.Visible = false;
            toolStripMenuItemRegCodeViewer.Visible = false;
            tsItemBtnRegCode.Visible = false;
            tsItemBtnAddCompany.Visible = false;

            toolStripStatusLblCompanyInfo.Text = (Session.CURRENT_SOFT != null && Session.CURRENT_SOFT.Company != null) ? Session.CURRENT_SOFT.Company.Name : "未注册";
            toolStripStatusLblCompanyInfo.Text += string.Format(" [版本：{0}]", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            toolStripSeparator1.Visible = false;

            DisableMenuBar(menuStrip1, disable);
            DisableToolBar(toolStrip1, disable);
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

        #region 工具栏按钮事件
        /// <summary>
        /// 导入
        /// </summary>
        private void tsddBtnImport_Click(object sender, EventArgs e)
        {
            string theFile;
            openFileDialog1.InitialDirectory = Application.ExecutablePath;
            openFileDialog1.Filter = "Excel Files(*.xls)|*.xls";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Thread importThread = new Thread(new ThreadStart(new Action(() =>
                {
                    theFile = openFileDialog1.FileName;
                    IList<RouteInformationItem> rlist = null;
                    try
                    {
                        Dictionary<string, int> dicCompanies = _service.GetAllCompanies();
                        rlist = (new ServerBusinesses()).GetExcelData(theFile, dicCompanies);
                    }
                    catch (Exception ex)
                    {
                        log.Error("读取Excel文件失败", ex);
                        CloseProgressForm();
                        this.Invoke(new Action(() =>
                        {
                            UserUtils.ShowInfo("读取Excel文件失败");
                        }));
                        return;
                    }

                    if (rlist == null)
                    {
                        this.Invoke(new Action(() =>
                        {
                            UserUtils.ShowInfo("文件为空");
                        }));
                        CloseProgressForm();
                        return;
                    }

                    bool result = _service.ImportRouteInformationItems(rlist.ToArray());
                    //bool result = (new ServerBusinesses()).ImportRouteInformationItems(rlist.ToArray());

                    CloseProgressForm();

                    string info = string.Empty;
                    if (result)
                    {
                        info = "导入成功！";
                    }
                    else
                    {
                        info = "导入失败！";
                    }
                    this.Invoke(new Action(() =>
                    {
                        UserUtils.ShowInfo(info);
                    }));
                })));
                importThread.IsBackground = true;
                importThread.Start();

                OpenProgressForm("正在导入数据，请耐心等待。。。", importThread, true);
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
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

                Thread exportThread = new Thread(new ThreadStart(new Action(() =>
                {
                    ClientBusinesses cb = new ClientBusinesses();

                    int? companyId = null;

                    if (Session.SOFT_VERSION_TYPE == SoftVersionType.Client)
                    {
                        companyId = Session.CURRENT_SOFT.Company.Id;
                    }
                    IList<RouteInformationItem> exportList = _service.GetRouteInformationItems(companyId);
                    if (exportList == null)
                    {
                        CloseProgressForm();
                        this.Invoke(new Action(() =>
                        {
                            UserUtils.ShowInfo("没有要导出的数据！");
                        }));
                        return;
                    }
                    cb.ExportExcel(localFilePath, exportList);

                    CloseProgressForm();

                    this.Invoke(new Action(() =>
                    {
                        UserUtils.ShowInfo("导出完毕，请查看导出的文件！");
                    }));
                })));
                exportThread.IsBackground = true;
                exportThread.Start();

                OpenProgressForm("正在导出数据，请耐心等待。。。", exportThread, true);
            }
        }

        /// <summary>
        ///  查询
        /// </summary>
        private void toolStripMenuItemSearch_Click(object sender, EventArgs e)
        {
            ShowSingleWindow(typeof(FrmMain), FormWindowState.Maximized);
        }
        
        #endregion

        #region 菜单栏按钮事件
        private void tsItemBtnAddCompany_Click(object sender, EventArgs e)
        {
            //ShowSingleWindow(typeof(AddCompanyForm));
            FrmAddCompany addForm = new FrmAddCompany();
            addForm.StartPosition = FormStartPosition.CenterParent;
            addForm.ShowDialog(this);
        }

        private void tsItemBtnRegCode_Click(object sender, EventArgs e)
        {
            //ShowSingleWindow(typeof(RegCodeForm));
            FrmRegCode regForm = new FrmRegCode();
            regForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dresult = regForm.ShowDialog(this);
        }

        private void toolStripMenuItemSoftInfo_Click(object sender, EventArgs e)
        {
            FrmSoftInfo formInstance = new FrmSoftInfo();
            formInstance.StartPosition = FormStartPosition.CenterParent;
            formInstance.ShowDialog(this);
        }

        private void toolStripMenuItemRegCodeViewer_Click(object sender, EventArgs e)
        {
            FrmRegCodeViewer formInstance = new FrmRegCodeViewer();
            formInstance.StartPosition = FormStartPosition.CenterParent;
            formInstance.ShowDialog(this);
        }

        private void toolStripMenuItemAboutUs_Click(object sender, EventArgs e)
        {
            FrmStart form = new FrmStart();
            form.ShowDialog(this);
        }

        private void toolStripBtnQuery_Click(object sender, EventArgs e)
        {
            ShowSingleWindow(typeof(FrmMain), FormWindowState.Maximized);
        }

        private void toolStripMenuItemCompanyMgr_Click(object sender, EventArgs e)
        {
            FrmCompanyList regForm = new FrmCompanyList();
            regForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult dresult = regForm.ShowDialog(this);
        } 
        #endregion

        /// <summary>
        ///  去除mdi子窗体最大化后的（最大化、最小化）按钮
        /// </summary>
        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0         //隐藏子窗体图标         
                //|| e.Item.Text == "最小化(&N)"  //隐藏最小化按钮         
                //|| e.Item.Text == "还原(&R)"  //隐藏还原按钮         
                //|| e.Item.Text == "关闭(&C)" //隐藏最关闭按钮
                )
            {
                e.Item.Visible = false;
            }
        }

        /// <summary>
        ///  快捷键
        /// </summary>
        private void ContainerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (Session.SOFT_VERSION_TYPE == SoftVersionType.Client)
            {
                // 注册机器码
                if (e.Modifiers == (Keys.Control | Keys.Shift | Keys.Alt) && e.KeyCode == Keys.F8) //Ctrl+Shift+Alt+F8
                {
                    if (Session.CURRENT_SOFT == null)
                        return;
                    if (!string.IsNullOrEmpty(Session.CURRENT_SOFT.RegCode))
                        return;
                    FrmUserRegister formRegCode = new FrmUserRegister();
                    formRegCode.StartPosition = FormStartPosition.CenterParent;
                    DialogResult dresult = formRegCode.ShowDialog(this);
                    if (dresult == System.Windows.Forms.DialogResult.Yes)
                    {
                        UserUtils.ShowInfo("注册成功！");
                        InitClientUI();
                        Session.CURRENT_SOFT.RegCode = CommonTool.GetRegCode(Session.CURRENT_SOFT.MachineCode);
                    }
                    if (dresult == System.Windows.Forms.DialogResult.No)
                    {
                        UserUtils.ShowError("注册失败！");
                    }
                }
                // 查看机器码
                if (e.Modifiers == (Keys.Control | Keys.Shift | Keys.Alt) && e.KeyCode == Keys.F9) //Ctrl+Shift+Alt+F9
                {
                    FrmSoftInfo formInstance = new FrmSoftInfo();
                    formInstance.StartPosition = FormStartPosition.CenterParent;
                    formInstance.ShowDialog(this);
                }
            }
        }

        /// <summary>
        ///  窗体关闭
        /// </summary>
        private void ContainerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #region 进度条窗体
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
                if (thread != null)
                {
                    thread.Abort();
                }
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
        #endregion

        #region 弃用代码
        /// <summary>
        ///  显示背景窗体
        /// </summary>
        private void ShowBackForm()
        {
            //显示背景
            FrmBack frmb = new FrmBack();
            frmb.MdiParent = this;
            //(frmb, true);
            frmb.Dock = DockStyle.Fill;
            frmb.Show();

            //MdiClient ctlMDI;
            //// Loop through all of the form's controls looking
            //// for the control of type MdiClient.
            //foreach (Control ctl in this.Controls)
            //{
            //    try
            //    {
            //        // Attempt to cast the control to type MdiClient.
            //        ctlMDI = (MdiClient)ctl;

            //        // Set the BackColor of the MdiClient control.
            //        ctlMDI.BackColor = this.BackColor;
            //    }
            //    catch (InvalidCastException exc)
            //    {
            //        // Catch and ignore the error if casting failed.
            //    }
            //}
            //

            //int iCnt = this.Controls.Count;
            //for (int i = 0; i < this.Controls.Count; i++)
            //{
            //    if (this.Controls[i].GetType().ToString() == "System.Windows.Forms.MdiClient")
            //    {
            //        this.mdiClient = (System.Windows.Forms.MdiClient)this.Controls[i];
            //        break;
            //    }
            //}
            //string fbImage = Application.StartupPath + "\\Images\\backGround.png";
            //if (File.Exists(fbImage))
            //{
            //    Bitmap bm = new Bitmap(fbImage);
            //    this.mdiClient.BackgroundImage = bm;
            //    mdiClient.BackgroundImageLayout = ImageLayout.Stretch; 
            //}
        }

        private void FrmContainer_MdiChildActivate(object sender, EventArgs e)
        {
            //if (this.ActiveMdiChild.Name == "FrmBack")
            //    foreach (Form f in this.MdiChildren)
            //        if (f.Name != "FrmBack") f.Activate();//或f.BringToFront();
        } 
        #endregion
    }

    public enum SoftState
    {
        [Description("已注册且已激活")]
        RegisterdAndActived = 1,
        [Description("已注册但未激活")]
        RegisterdButUnactived = 2,
        [Description("尚未完成注册")]
        UnRegisterd = 3,
        [Description("尚未注册")]
        Registering = 4,
        [Description("机器码与注册码不匹配")]
        UnmatchedRegistered = 5
    }
}
