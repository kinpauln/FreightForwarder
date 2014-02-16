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

    /// <summary> 
    /// 用户类 
    /// </summary> 
    public class UserInfo
    {

        private int userid;

        private string username;

        private string password;

        private int typeid;

        private string trueName;

        private DateTime lastTime;



        /// <summary> 

        /// 用户最后登录时间 

        /// </summary> 

        public DateTime LastTime
        {

            get { return lastTime; }

            set { lastTime = value; }

        }



        /// <summary> 

        /// 真实姓名 

        /// </summary> 

        public string TrueName
        {

            get { return trueName; }

            set { trueName = value; }

        }



        /// <summary> 

        /// 用户类型ID 

        /// </summary> 

        public int TypeId
        {

            get { return typeid; }

            set { typeid = value; }

        }



        /// <summary> 

        /// 用户密码 

        /// </summary> 

        public string Password
        {

            get { return password; }

            set { password = value; }

        }



        /// <summary> 

        /// 用户名 

        /// </summary> 

        public string Username
        {

            get { return username; }

            set { username = value; }

        }



        /// <summary> 

        /// 用户ID 

        /// </summary> 

        public int UserId
        {

            get { return userid; }

            set { userid = value; }

        }

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
