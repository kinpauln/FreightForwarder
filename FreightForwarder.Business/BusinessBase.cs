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
        /// <summary>
        ///  导出文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="rlist">要导出的列表</param>
        public void ExportExcel(string filePath, IList<RouteInformationItem> rlist)
        {
            DataTable rdt = rlist.ToDataTable();

            MemoryStream ms = NPOIHelper.RenderToExcel(rdt);
            NPOIHelper.SaveToFile(ms, filePath);
        }

        public IList<RouteInformationItem> GetRouteInformationItems(int? companyId)
        {
            IList<RouteInformationItem> rlist = (new DBHelper()).GetRouteInformationItems(companyId);
            return rlist;
        }

        public static IList<RouteInformationItem> GetRoutItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer) {
            IList<RouteInformationItem> rlist = (new DBHelper()).GetRouteInformationItems(shipName, startPort, destinationPort, isSingleContainer);

            return rlist;
        }

        public static RegisterCode IsRegistered(string machineCode)
        {
            return (new DBHelper()).SoftwareIsRegistered(machineCode);
        }
    }
}
