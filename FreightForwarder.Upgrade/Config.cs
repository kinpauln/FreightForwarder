using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace TransPadUpdater
{
    public class Config
    {
        private static string docurl = string.Format(@"{0}\Config.xml", Application.StartupPath);

        public static string GetValue(string key)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(docurl);
            string sRet = string.Empty;

            if (ds != null)
            {
                sRet = ds.Tables[0].Rows[0][key].ToString();
            }
            return sRet;
        }

        public static bool SetValue(string key, string value)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(docurl);

            if (ds != null)
            {
                ds.Tables[0].Rows[0][key] = value;
                ds.WriteXml(docurl);
            }
            return true;
        }
    }
}
