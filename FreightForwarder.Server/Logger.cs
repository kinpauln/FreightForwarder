using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Server
{
    public class Logger
    {
        #region
        public static ILog init()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            int date = now.Day;
            int hour = now.Hour;
            int minute = now.Minute;
            int second = now.Second;
            int millisecond = now.Millisecond;
            string format = year + "/" + month + "/" + date + " " + hour + ":" + minute + ":" + second + ":" + millisecond;
            //string pattern = "["+format+"]%n MESSAGE:%message 日志级别:%-5level%n";  
            string pattern = "[" + format + "]%n %-5level:[%message]%n%n";
            log4net.Layout.PatternLayout pl = new log4net.Layout.PatternLayout(pattern);
            log4net.Appender.FileAppender file = new log4net.Appender.FileAppender(pl, "D:/log.txt");
            log4net.Config.BasicConfigurator.Configure(file);
            return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void Debug(object message, Exception exception)
        {
            ILog log = init();
            log.Debug(GetExceptionMsg(exception, message.ToString()));
        }

        public static void Debug(object message)
        {
            ILog log = init();
            log.Debug(GetExceptionMsg(null, message.ToString()));
        }

        public static void Error(object message, Exception exception)
        {
            ILog log = init();
            log.Error(GetExceptionMsg(exception, message.ToString()));
        }

        public static void Error(object message)
        {
            ILog log = init();
            log.Error(GetExceptionMsg(null, message.ToString()));
        }

        public static void Fatal(object message, Exception exception)
        {
            ILog log = init();
            log.Fatal(GetExceptionMsg(exception, message.ToString()));
        }

        public static void Fatal(object message)
        {
            ILog log = init();
            log.Fatal(GetExceptionMsg(null, message.ToString()));
        }

        public static void Info(object message, Exception exception)
        {
            ILog log = init();
            log.Info(GetExceptionMsg(exception, message.ToString()));
        }

        public static void Info(object message)
        {
            ILog log = init();
            log.Info(GetExceptionMsg(null, message.ToString()));
        }

        public static void Warn(object message, Exception exception)
        {
            ILog log = init();
            log.Warn(GetExceptionMsg(exception, message.ToString()));
        }

        public static void Warn(object message)
        {
            ILog log = init();
            log.Warn(GetExceptionMsg(null,message.ToString()));
        }

        #endregion

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【出错信息】：" + backStr);
            }

            sb.AppendLine("***************************************************************");

            return sb.ToString();
        }
    }  
}
