using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Client
{

    /// <summary> 

    /// 虚拟会话类 

    /// </summary> 

    public class Session
    {

        private static RegisterCode current_soft;

        /// <summary>
        /// 当前登录用户信息
        /// </summary> 
        public static RegisterCode CURRENT_SOFT
        {
            get { return Session.current_soft; }
            set { Session.current_soft = value; }
        }
    }
}
