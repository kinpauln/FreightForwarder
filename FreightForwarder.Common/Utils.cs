using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Common
{
    public class Utils
    {
    }

    [Description("是否直达")]
    public enum SailNonstopValues
    {
        [Description("否")]
        No = 0,// 非直达
        [Description("是")]
        Yes = 1, // 直达
        [Description("未知")]
        Unknow = 2,// 未知
    }


    [Description("整柜/拼箱")]
    public enum IsSingleContainerValues
    {
        [Description("拼箱")]
        No = 0,// 拼箱
        [Description("整柜")]
        Yes = 1, // 整柜
        [Description("未知")]
        Unknow = 2,// 未知
    }

    [Description("注册码状态")]
    public enum RegCodeStates
    {
        [Description("停用")]
        Stopped = 0,// 停用
        [Description("有效")]
        Actived = 1, // 有效
        [Description("暂停")]
        Suspend = 2,// 暂停
        [Description("过期")]
        Invalid = 3,// 过期
    }
}
