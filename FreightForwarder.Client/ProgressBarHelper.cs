using FreightForwarder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Client
{
    public class ProgressBarHelper
    {
        /// <summary>
        /// 进度条窗口
        /// </summary>
        private FormProgressBar myProcessBar = null;
        SetProgessBarEventHandler mSetProgressBar;
        #region 绑定响应进度条的事件
        private BindProgessBarEventHandler mBindProgressBar = null;
        /// <summary>
        /// 绑定响应进度条的事件
        /// </summary>
        public BindProgessBarEventHandler BindProgressBar
        {
            get
            {
                return mBindProgressBar;
            }
            set
            {
                mBindProgressBar = value;
            }
        }
        #endregion
        /// <summary>
        /// 显示进度条窗口
        /// </summary>
        public void ShowProcessBar(IAsyncResult result)
        {
            myProcessBar = new FormProgressBar();
            // Init increase event
            mSetProgressBar = new SetProgessBarEventHandler(myProcessBar.SetProgressBar);
            myProcessBar.ShowDialog();
            myProcessBar = null;
        }
        private void ThreadFun()
        {
            if (mBindProgressBar != null)
            {
                ///声明了一个空的匿名函数作为参数
                ///就是为了使用BeginInvoke方法
                ///BeginInvoke不用等待函数执行完成
                ///Invoke需要等待函数执行完成
                MethodInvoker mi = new MethodInvoker(delegate() { });
                AsyncCallback callBack = new AsyncCallback(ShowProcessBar);
                mi.BeginInvoke(callBack, null);
                //最多等待1000单位
                int maxWaitFor = 1000;
                do
                {
                    Thread.Sleep(50);//Sleep a while to show window
                    maxWaitFor -= 50;
                    if (mSetProgressBar != null)
                    {
                        break;
                    }
                }
                while (maxWaitFor > 0);
                mBindProgressBar(mSetProgressBar);
            }

        }
        /// <summary>
        /// 启动进度条
        /// </summary>
        public void Start()
        {
            Thread thdSub = new Thread(new ThreadStart(ThreadFun));
            thdSub.Start();
        }
        /// <summary>
        /// 停止进度条
        /// </summary>
        public void Stop()
        {
            if (myProcessBar != null)
            {
                myProcessBar.Close();
                myProcessBar = null;
            }
        }
    }
}
