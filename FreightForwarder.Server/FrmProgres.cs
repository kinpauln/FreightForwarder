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
    public partial class FrmProgres : Form
    {
        public FrmProgres()
        {
            InitializeComponent();
        }

        #region 属性
        /// <summary>
        /// 设置进度条的最小值(默认1)
        /// </summary>
        public int ProgersMinimum
        {
            set { pbar.Minimum = value; }
            get { return pbar.Minimum; }
        }
        /// <summary>
        /// 设置进度条的最最大值(默认100)
        /// </summary>
        public int ProgersMaximum
        {
            set { pbar.Maximum = value; }
            get { return pbar.Maximum; }
        }
        /// <summary>
        /// 设置进度条的当前值(默认1)
        /// </summary>
        public int ProgersValue
        {
            set { pbar.Value = value; }
            get { return pbar.Value; }
        }
        /// <summary>
        /// 设置进度条的步长大值(默认1)
        /// </summary>
        public int ProgersStep
        {
            set { pbar.Step = value; }
            get { return pbar.Step; }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 增加进度
        /// </summary>
        /// <param name="value">需要增加的值</param>
        /// <returns></returns>
        public bool Increase(int value)
        {
            if (!this.IsHandleCreated)
                return false;
            //关闭进度条窗口
            Action WinClose = (() =>
            {
                this.Close();
            });

            //设置当前进度条位置
            Action<int> SetProgresValue = ((val) =>
            {
                if (val < 0 || val > pbar.Maximum || val < pbar.Minimum)
                {
                    pbar.Value = pbar.Maximum;
                    this.Close();
                    return;
                }

                pbar.Value = val;
            });

            try
            {
                this.Invoke(SetProgresValue, value);
            }
            catch
            {
            }

            return !(value < 0 || value > pbar.Maximum || value < pbar.Minimum);
        }

        #endregion
    }
}
