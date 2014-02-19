using FreightForwarder.Common;
using FreightForwarder.Data;
using FreightForwarder.Domain.Entities;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public class BusinessBase
    {
        public void ExportExcel(string filePath)
        {
            //要导出的列表
            IList<RouteInformationItem> rlist = DBHelper.GetRouteInformationItems(null);
            DataTable rdt = rlist.ToDataTable();
            int rmin = 0;
            int rmax = 10000;

            //if (rdt == null || rdt.Rows.Count < 0) { rmax = 0; }
            //else {
            //    rmax = rdt.Rows.Count;
            //}

            for (int i = rmin; i <= rmax; i++) {
                ProgressBarUpdateEventArgs args = new ProgressBarUpdateEventArgs() { 
                    MaxValue = rmax,
                    CurrentValue = i,
                    DisplayText = "现在的进度值是"+i.ToString()
                };
                OnSetProgessBar(args);
            }

            MemoryStream ms = NPOIHelper.RenderToExcel(rdt);
            NPOIHelper.SaveToFile(ms, filePath);
        }

        public static IList<RouteInformationItem> GetRoutItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer) {
            IList<RouteInformationItem> rlist = DBHelper.GetRouteInformationItems(shipName, startPort, destinationPort, isSingleContainer);

            return rlist;
        }

        public static RegisterCode IsRegistered(string machineCode)
        {
            return DBHelper.SoftwareIsRegistered(machineCode);
        }

        /// <summary>
        /// 设置进度条的事件
        /// </summary>
        public event SetProgessBarEventHandler UpdateProgessBarEvent;

        /// <summary>
        /// 定义事件处理方法
        /// </summary>
        protected virtual void OnSetProgessBar(ProgressBarUpdateEventArgs e){
            if (UpdateProgessBarEvent != null)
                UpdateProgessBarEvent(e);
        }
    }
}
