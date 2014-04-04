using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Configuration;
using FreightForwarder.Upgrade.FFUpgrade.Service;

namespace TransPadUpdater {
    public partial class MainForm : Form
    {
        private FreightForwarder.Upgrade.Properties.Settings _settings = FreightForwarder.Upgrade.Properties.Settings.Default;
        private UpdateServiceClient _service = null;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        public MainForm() {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.Security.Mode = BasicHttpSecurityMode.None;

            _service = new UpdateServiceClient(
                binding, new EndpointAddress(string.Format("{0}{1}", _settings.ServerUrl, _settings.UpdateUrl)));
        }

        //加载
        private void MainForm_Load(object sender, EventArgs e) {
            Init();

            Thread thread = new Thread(new ThreadStart(StartUpdate));
            thread.Start();

            //自动远行
            SetAutoRun();
        }

        //系统升级
        private void StartUpdate() {
            try
            {
                this.Invoke(new Action(() =>
                {
                    sbStatus.Text = "正在检查是否存在新版本...";
                    sbPbar.Style = ProgressBarStyle.Marquee;
                    sbPbar.Visible = true;
                }));

                Assembly assembly = Assembly.Load(
                    File.ReadAllBytes(Application.StartupPath + "\\" + _settings.ExecutablePath));
                string[] versions = _service.CheckUpdate(assembly.GetName().Version.ToString());

                lblVersion.Text = string.Format("程序版本：{0}", assembly.GetName().Version.ToString());

                if (versions != null && versions.Length > 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        sbPbar.Style = ProgressBarStyle.Blocks;
                        sbPbar.Maximum = versions.Length;
                        sbPbar.Value = 0;
                    }));
                    foreach (string version in versions.OrderBy(k => k))
                    {
                        this.Invoke(new Action(() =>
                        {
                            sbStatus.Text = "正在升级至 " + version + " ...";
                        }));

                        UpgradePackage updateFile = _service.GetUpdate(version);
                        if (updateFile != default(UpgradePackage))
                        {
                            using (Stream stream = new MemoryStream(updateFile.FileBytes))
                            {
                                Unzip(stream, Application.StartupPath + "\\");
                            }
                        }

                        this.Invoke(new Action(() =>
                        {
                            ++sbPbar.Value;
                            //sbStatus.Text = "已升级至 " + version;
                        }));

                        Thread.Sleep(500);
                    }
                }

                this.Invoke(new Action(() =>
                {
                    sbPbar.Style = ProgressBarStyle.Marquee;
                    sbPbar.Visible = false;
                    sbStatus.Text = "准备就绪";

                    assembly = Assembly.Load(
                    File.ReadAllBytes(Application.StartupPath + "\\" + _settings.ExecutablePath));
                    lblVersion.Text = string.Format("程序版本：{0}", assembly.GetName().Version.ToString());

                    //允许开始登录
                    pnlLogin.Enabled = true;
                    txtUserName.Focus();
                }));

                Upgrade();

                Application.ExitThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Application.ExitThread();
            }
        }

        //解压升级程序
        private void Unzip(Stream zipStream, string rootDirectory) {
            if (!Directory.Exists(rootDirectory)) {
                Directory.CreateDirectory(rootDirectory);
            }

            ZipInputStream stream = new ZipInputStream(zipStream);

            ZipEntry entry = null;
            while ((entry = stream.GetNextEntry()) != null) {
                string directoryName = Path.GetDirectoryName(entry.Name);
                string fileName = Path.GetFileName(entry.Name);

                if (!String.IsNullOrEmpty(directoryName)) {
                    Directory.CreateDirectory(rootDirectory + directoryName);
                }

                if (!String.IsNullOrEmpty(fileName)) {
                    FileStream streamWriter = File.Create(rootDirectory + entry.Name);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true) {
                        size = stream.Read(data, 0, data.Length);
                        if (size > 0) {
                            streamWriter.Write(data, 0, size);
                        }
                        else {
                            break;
                        }
                    }

                    streamWriter.Close();
                }
            }

            stream.Close();
        }

        //初始化界面
        private void Init()
        {
            pnlLogin.Enabled = false;
            //ad.Navigate(new Uri(string.Format("{0}{1}", _settings.ServerUrl, _settings.NotifyUrl), false));
        }

        /// <summary>
        ///  升级
        /// </summary>
        private void Upgrade()
        {
            try
            {
                //登录成功
                Process process = new Process();
                string startfile = Application.StartupPath + "\\";// +_settings.ExecutablePath;

                ProcessStartInfo StartInfo = new ProcessStartInfo(startfile, null);
                StartInfo.UseShellExecute = false;
                process.StartInfo = StartInfo;
                process.Start();

                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("系统启动失败:[{0}]", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //自动远行
        private string AutoRunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private void SetAutoRun()
        {
            try
            {
                /*获得当前登录的Windows用户标示
                System.Security.Principal.WindowsIdentity identity = 
                    System.Security.Principal.WindowsIdentity.GetCurrent();

                //判断当前登录用户是否为管理员
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal( identity );
                if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    //创建启动对象
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                    //设置运行文件
                    startInfo.FileName = Application.ExecutablePath;


                    //如果不是管理员，则启动UAC
                    System.Diagnostics.Process.Start(startInfo);

                    //退出
                    System.Windows.Forms.Application.Exit();
                }*/

                string path = Application.ExecutablePath;
                RegistryKey root = Registry.LocalMachine;

                //判断键值是不是存在，如果不在则创建
                RegistryKey run = root.CreateSubKey(AutoRunKey);
                object obj = run.GetValue(Application.ProductName);
                if (obj == null)
                {
                    run.SetValue(Application.ProductName, path);
                }

                root.Close();

            }
            catch { }
        }
    }
}
