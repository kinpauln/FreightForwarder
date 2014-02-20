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
using System.Threading;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public class BusinessBase : AbstractProgressBar
    {
        public void ExportExcel(string filePath)
        {
            //要导出的列表
            IList<RouteInformationItem> rlist = DBHelper.GetRouteInformationItems(null);
            DataTable rdt = rlist.ToDataTable();

            //int rmin = 0;
            //int rmax = 8;
            //for (int i = rmin; i <= rmax; i++)
            //{
            //    ProgressBarUpdateEventArgs args = new ProgressBarUpdateEventArgs()
            //    {
            //        MaxValue = rmax,
            //        CurrentValue = i,
            //        DisplayText = "现在的进度值是" + i.ToString()
            //    };
            //    OnSetProgessBar(args);
            //}

            //Thread setBarThread = new System.Threading.Thread(new System.Threading.ThreadStart(new Action(() =>
            //{
            //    NPOIHelper helper = new NPOIHelper(null);
            //    helper.UpdateProgessBarEvent += new SetProgessBarEventHandler(new Action<ProgressBarUpdateEventArgs>((args) =>
            //    {
            //        OnSetProgessBar(args);
            //    }));
            //    MemoryStream ms = helper.RenderToExcel(rdt);
            //    NPOIHelper.SaveToFile(ms, filePath);
            //})));
            //setBarThread.IsBackground = true;
            //setBarThread.Start();
            Thread setBarThread = new System.Threading.Thread(new System.Threading.ThreadStart(new Action(() =>
            {
                NPOIHelper helper = new NPOIHelper(null);
                helper.UpdateProgessBarEvent += new SetProgessBarEventHandler(new Action<ProgressBarUpdateEventArgs>((args) =>
                {
                    this.OnSetProgessBar(args);
                }));
                MemoryStream ms = helper.RenderToExcel(rdt);
                NPOIHelper.SaveToFile(ms, filePath);
            })));
            setBarThread.IsBackground = true;
            setBarThread.Start();

            //NPOIHelper helper = new NPOIHelper(null);
            //helper.UpdateProgessBarEvent += new SetProgessBarEventHandler(new Action<ProgressBarUpdateEventArgs>((args) =>
            //{
            //    this.OnSetProgessBar(args);
            //}));
            //MemoryStream ms = helper.RenderToExcel(rdt);
            //NPOIHelper.SaveToFile(ms, filePath);
        }

        public static IList<RouteInformationItem> GetRoutItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer) {
            IList<RouteInformationItem> rlist = DBHelper.GetRouteInformationItems(shipName, startPort, destinationPort, isSingleContainer);

            return rlist;
        }

        public static RegisterCode IsRegistered(string machineCode)
        {
            return DBHelper.SoftwareIsRegistered(machineCode);
        }
    }
}
