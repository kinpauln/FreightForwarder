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
            log.Debug(message, exception);
        }

        public static void Debug(object message)
        {
            ILog log = init();
            log.Debug(message);
        }

        public static void Error(object message, Exception exception)
        {
            ILog log = init();
            log.Error(message, exception);
        }

        public static void Error(object message)
        {
            ILog log = init();
            log.Error(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            ILog log = init();
            log.Fatal(message, exception);
        }

        public static void Fatal(object message)
        {
            ILog log = init();
            log.Fatal(message);
        }

        public static void Info(object message, Exception exception)
        {
            ILog log = init();
            log.Info(message, exception);
        }

        public static void Info(object message)
        {
            ILog log = init();
            log.Info(message);
        }

        public static void Warn(object message, Exception exception)
        {
            ILog log = init();
            log.Warn(message, exception);
        }

        public static void Warn(object message)
        {
            ILog log = init();
            log.Warn(message);
        }

        #endregion
    }  
}
