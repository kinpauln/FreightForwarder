using FreightForwarder.Common;
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

namespace FreightForwarder.Client
{
    public partial class FormProgressBar : Form
    {
        public FormProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 委托设置文本标签
        /// </summary>
        /// <param name="text"></param>
        public delegate void SetLabeTextEventHandler(string text);
        /// <summary>
        /// 设置进度条相关参数
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="currentValue"></param>
        /// <param name="textValue"></param>
        /// <returns></returns>
        public void SetProgressBar(ProgressBarUpdateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new SetProgessBarEventHandler(this.SetProgressBar), e);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
            else
            {
                if (e.CurrentValue >= 0)
                {
                    progressBar1.Maximum = e.MaxValue;
                    if (e.CurrentValue < progressBar1.Maximum)
                    {
                        progressBar1.Value = e.CurrentValue;
                        SetLabelText(e.DisplayText);
                    }
                    else
                    {
                        progressBar1.Value = progressBar1.Maximum;
                        SetLabelText(e.DisplayText);
                        Thread.Sleep(50);
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }
        /// <summary>
        /// 设置label的显示文本
        /// </summary>
        /// <param name="text"></param>
        public void SetLabelText(string text)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new SetLabeTextEventHandler(this.SetLabelText), new object[] { text });
                }
                catch { }
            }
            else
            {
                this.label1.Text = text;
            }
        }
    }
}
