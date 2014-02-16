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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder
{
    public partial class ContainerForm : Form
    {
        private Server.Properties.Settings _defaultSettings;
        public ContainerForm()
        {
            InitializeComponent();
            _defaultSettings = Server.Properties.Settings.Default;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterParent;
            string dbconnString = _defaultSettings.FFDBContext;
            //MessageBox.Show(dbconnString);
            FreightForwarder.Server.DBHelper.GetEntries();

            if (!Validate()) {
                return;
            }

            panelContainer.Visible = true;
            panelContainer.SendToBack();
            ShowSingleWindow(typeof(MainForm), FormWindowState.Maximized);
        }

        private bool Validate() {
            string machineCode = CommonTool.GetMachineCode();
            RegisterCode rc = BusinessBase.IsRegistered(machineCode);
            if (rc == null) {
                MessageBox.Show(string.Format("还没注册，请将机器码{0}发给软件供应商，购买注册码后才能使用。", machineCode));
                return false;
            }
            return true;
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

                ClientBusinesses.ExportExcel(localFilePath);
                MessageBox.Show("导出成功！");
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
            addForm.Show();
        }

        private void tsItemBtnRegCode_Click(object sender, EventArgs e)
        {
            //ShowSingleWindow(typeof(RegCodeForm));
            RegCodeForm regForm = new RegCodeForm();
            regForm.StartPosition = FormStartPosition.CenterParent;
            regForm.Show();
        }
    }
}
