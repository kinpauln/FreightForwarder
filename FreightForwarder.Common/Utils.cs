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

    public enum SailNonstopValues
    {
        [Description("否")]
        No = 0,// 非直达
        [Description("是")]
        Yes = 1, // 直达
        [Description("未知")]
        Unknow = 2,// 未知
    }

    public enum IsSingleContainerValues
    {
        [Description("拼箱")]
        No = 0,// 拼箱
        [Description("整柜")]
        Yes = 1, // 整柜
        [Description("未知")]
        Unknow = 2,// 未知
    }
}
