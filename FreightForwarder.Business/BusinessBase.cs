using FreightForwarder.Common;
using FreightForwarder.Data;
using FreightForwarder.Domain.Entities;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public class BusinessBase
    {
        public static void ExportExcel(string filePath)
        {
            //要导出的列表
            IList<RouteInformationItem> rlist = DBHelper.GetRouteInformationItems(null);
            MemoryStream ms = NPOIHelper.RenderToExcel(rlist.ToDataTable());
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
