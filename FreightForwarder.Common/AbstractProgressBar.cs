using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Common
{
    public abstract class AbstractProgressBar
    {
        /// <summary>
        /// 设置进度条的事件
        /// </summary>
        public event SetProgessBarEventHandler UpdateProgessBarEvent;

        /// <summary>
        /// 定义事件处理方法
        /// </summary>
        protected virtual void OnSetProgessBar(ProgressBarUpdateEventArgs e)
        {
            if (UpdateProgessBarEvent != null)
                UpdateProgessBarEvent(e);
        }
    }
}
