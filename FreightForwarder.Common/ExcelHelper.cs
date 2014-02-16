using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace FreightForwarder.Common
{
    public partial class ExcelHelper
    {
        public static void Test() { 

        }

        public static void GetSheet(Stream stream)
        {
            IWorkbook workbook = new HSSFWorkbook(stream);//从流内容创建Workbook对象
            ISheet sheet = workbook.GetSheetAt(0);//获取第一个工作表
            IRow row = sheet.GetRow(0);//获取工作表第一行
            ICell cell = row.GetCell(0);//获取行的第一列
            string value = cell.ToString();//获取列的值
        }
    }
}
