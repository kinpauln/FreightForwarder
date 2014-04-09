using FreightForwarder.Common;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.UI.Winform
{

    /// <summary> 

    /// 虚拟会话类 

    /// </summary> 

    public class Session
    {

        private static RegisterCode current_soft;
        private static SoftVersionType soft_version_type;

        /// <summary>
        /// 当前登录用户信息
        /// </summary> 
        public static RegisterCode CURRENT_SOFT
        {
            get { return Session.current_soft; }
            set { Session.current_soft = value; }
        }

        /// <summary>
        /// 软件版本
        /// </summary> 
        public static SoftVersionType SOFT_VERSION_TYPE
        {
            get { return Session.soft_version_type; }
            set { Session.soft_version_type = value; }
        }
    }
}
