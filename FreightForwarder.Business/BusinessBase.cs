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
        public void ExportExcel(string filePath, int? companyId)
        {
            //要导出的列表
            IList<RouteInformationItem> rlist = DBHelper.GetRouteInformationItems(companyId);
            DataTable rdt = rlist.ToDataTable();

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
    }
}
