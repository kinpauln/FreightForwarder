using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Common
{
    public class NPOIHelper : AbstractProgressBar
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HSSFWorkbook workbook;
        public static IWorkbook LoadFromFile(string filepath)
        {
            using (FileStream fi = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                return new HSSFWorkbook(fi);
            }
        }


        public static ISheet CreateSheet(string sheetname, IWorkbook workbook)
        {
            return workbook.CreateSheet(sheetname);
        }

        public ISheet WriteToTemplate<T>(IList<T> datalist, string sheetname, int fieldstartrowindex, int fieldstartcolindex, int datastartrowindex)
        {
            return null;
        }

        public NPOIHelper(string filetemplatepath)
        {
            workbook = (HSSFWorkbook)LoadFromFile(filetemplatepath);

        }

        /// <summary>
        /// 将List对象转为SHEET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheetname"></param>
        /// <param name="data"></param>       
        /// <param name="titlerowindex">表头列</param>
        /// <param name="datarowindex">表数据列</param>
        /// <returns></returns>
        public int ConvertTOSheet<T>(string sheetname, IList<T> data, int titlerowindex, int datarowindex)
           where T : new()
        {
            ISheet sheet = workbook.GetSheet(sheetname);

            IRow titlerow = sheet.GetRow(titlerowindex);
            int rowstartindex = titlerow.FirstCellNum;
            int rowlastindex = titlerow.LastCellNum;

            IDictionary<int, string> fieldindexdic = new Dictionary<int, string>();
            for (int i = rowstartindex; i <= rowlastindex; i++)
            {
                ICell cell = titlerow.GetCell(i);
                if (cell != null)
                {
                    string fieldstr = cell.ToString();
                    if (!string.IsNullOrEmpty(fieldstr))
                    {
                        fieldindexdic.Add(cell.ColumnIndex, fieldstr.ToUpper());
                    }
                }
            }

            IEnumerable<string> fieldtitle = fieldindexdic.Select(x => x.Value).Distinct();

            IDictionary<string, PropertyInfo> pifdic = GetPropertyInfoDic<T>(fieldtitle);
            for (int i = 0; i < data.Count; i++)
            {
                IRow datarow = sheet.CreateRow(datarowindex + i);
                foreach (var titlekv in fieldindexdic)
                {
                    object dataobject = pifdic[titlekv.Value].GetValue(data[i], null);
                    if (dataobject != null)
                    {
                        ICell datacell = datarow.CreateCell(titlekv.Key);
                        datacell.SetCellValue(dataobject.ToString());
                    }
                }
            }
            return workbook.GetSheetIndex(sheet);
        }

        public IDictionary<string, PropertyInfo> GetPropertyInfoDic<T>(IEnumerable<string> namelist)
            where T : new()
        {
            IDictionary<string, PropertyInfo> pifdic = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] pifs = typeof(T).GetProperties();

            IEnumerable<PropertyInfo> filedpifs = pifs.Where(x => namelist.Contains(x.Name.ToUpper()));
            foreach (var kv in filedpifs)
            {
                pifdic.Add(kv.Name.ToUpper(), kv);
            }
            return pifdic;
        }
        public void DeleteSheet(string sheetname)
        {
            int sheetindex = workbook.GetSheetIndex(sheetname);
            workbook.RemoveSheetAt(sheetindex);
        }
        public void Write(Stream sm)
        {
            workbook.Write(sm);
        }
        public void DeleteRow(string sheetname, int rowindex)
        {
            ISheet sheet = workbook.GetSheet(sheetname);
            IRow row = sheet.GetRow(rowindex);
            sheet.RemoveRow(row);
        }

        #region add by pcitdbt 2013/11/11

        #region 将DataTable的数据读取成MemoryStream
        public static MemoryStream RenderToExcel(DataTable dt)
        {
            MemoryStream ms = new MemoryStream();
            using (dt)
            {
                //创建Workbook
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet(dt.TableName);
                //创建一个日期类型的格式
                ICellStyle dataStyle = book.CreateCellStyle();
                IDataFormat dataFormat = book.CreateDataFormat();
                dataStyle.DataFormat = dataFormat.GetFormat("yyyy-mm-dd");
                //创建表头
                IRow row = sheet.CreateRow(0);
                foreach (DataColumn col in dt.Columns)
                {
                    //给表头添加字段名字
                    row.CreateCell(col.Ordinal).SetCellValue(col.Caption);//Caption没有值则获取ColumnName
                    //设置列宽
                    sheet.SetColumnWidth(col.Ordinal, 30 * 110);
                }
                //创建数据行并添加值
                int rowIndex = 1;//标记数据行的位置
                foreach (DataRow dr in dt.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    //通过列来获取值
                    foreach (DataColumn column in dt.Columns)
                    {
                        //判断是否是DataTime类型
                        ICell newCell = dataRow.CreateCell(column.Ordinal);
                        string drValue = dr[column].ToString();
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                //newCell.SetCellValue(dateV);
                                newCell.SetCellValue(drValue);

                                newCell.CellStyle = dataStyle;//格式化显示
                                break;
                            case "System.Boolean"://布尔型
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }

                    }

                    //循环一行后i的值自增1
                    rowIndex++;
                }

                book.Write(ms);
                ms.Flush();
                ms.Position = 0;//指定内存流的当前位置
            }

            return ms;
        }
        #endregion

        #region 将DataReader的数据转换成MemoryStream并返回
        public static MemoryStream RenderToExcel(IDataReader dataReader)
        {
            MemoryStream ms = new MemoryStream();
            using (dataReader)
            {
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet("数据表1");
                //创建表头
                IRow row = sheet.CreateRow(0);
                //列的数目
                int columnCount = dataReader.FieldCount;
                for (int i = 0; i < columnCount; i++)
                {
                    row.CreateCell(i).SetCellValue(dataReader.GetName(i));
                }

                //创建数据行
                int rowIndex = 1;
                while (dataReader.Read())//dataReader只能一行一行地读取数据
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < columnCount; i++)
                    {

                        dataRow.CreateCell(i).SetCellValue(dataReader[i].ToString());
                    }

                    rowIndex++;
                }

                book.Write(ms);
                ms.Flush();
                ms.Position = 0;

            }


            return ms;
        }

        #endregion

        #region 将流输出到指定的位置
        //保存输出到文件
        public static void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();
                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }
        #endregion

        #region 保存输出到浏览器
        //public static void SaveToBrowser(MemoryStream ms, System.Web.HttpContext context, string fileName)
        //{
        //    // 设置编码和附件格式
        //    context.Response.ContentType = "application/vnd.ms-excel";
        //    context.Response.ContentEncoding = Encoding.UTF8;
        //    context.Response.Charset = "";
        //    context.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8));

        //    //添加请求报文头
        //    //context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
        //    context.Response.BinaryWrite(ms.ToArray());
        //    context.Response.End();
        //}
        #endregion

        #region NPOI读取Excel流的数据相关
        public static DataTable ReadFromExcel(Stream excelStream)
        {
            DataTable dt = new DataTable();
            using (excelStream)
            {
                //创建WorkBook
                HSSFWorkbook book = new HSSFWorkbook(excelStream);
                ISheet sheet = book.GetSheetAt(0);//获取第一个表
                //获取第一行表头
                IRow headRow = sheet.GetRow(0);
                //列数
                int columnCount = headRow.LastCellNum;//LastCellNum=PhysicalNumberOfCells
                int rowCount = sheet.LastRowNum;//LastRowNum=PhysicalNumberOfCellsRow-1
                //创建DataTable的表头
                for (int i = headRow.FirstCellNum; i < columnCount; i++)
                {
                    DataColumn dc = new DataColumn(headRow.GetCell(i).StringCellValue.ToString());
                    dt.Columns.Add(dc);
                }
                //创建数据
                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    //一行一行地创建
                    DataRow dr = dt.NewRow();
                    IRow dataRow = sheet.GetRow(i);
                    if (dataRow != null)
                    {
                        for (int j = headRow.FirstCellNum; j < columnCount; j++)
                        {
                            try
                            {
                                if (dataRow.GetCell(j) == null)
                                {
                                    dr[j] = string.Empty;
                                    continue;
                                }

                                object cellValue = null;
                                string cellType = dataRow.GetCell(j).CellType.ToString();
                                switch (cellType)
                                {
                                    case "Numeric":
                                        cellValue = dataRow.GetCell(j).NumericCellValue;
                                        break;
                                    case "String":
                                        cellValue = dataRow.GetCell(j).StringCellValue;
                                        break;
                                    case "Boolean":
                                        cellValue = dataRow.GetCell(j).BooleanCellValue;
                                        break;
                                    case "Date":
                                        cellValue = dataRow.GetCell(j).DateCellValue;
                                        break;
                                    case "Error":
                                        cellValue = dataRow.GetCell(j).ErrorCellValue;
                                        break;
                                    case "RichString":
                                        cellValue = dataRow.GetCell(j).RichStringCellValue;
                                        break;
                                    default:
                                        cellValue = null;
                                        break;
                                }
                                if (cellValue != null)
                                {
                                    dr[j] = cellValue != null ? cellValue.ToString() : string.Empty;
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error("Excel转换成Table时出错，出错列为：" + j, ex);
                                continue;
                            }
                        }

                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        public static int RenderToDb(Stream excelFileStream, string insertSql)
        {
            int rowAffected = 0;
            using (excelFileStream)
            {
                HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
                ISheet sheet = workbook.GetSheetAt(0);//取第一个工作表
                StringBuilder builder = new StringBuilder();

                IRow headerRow = sheet.GetRow(0);//第一行为标题行
                int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1

                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        builder.Append(insertSql);
                        builder.Append(" values (");
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            builder.AppendFormat("'{0}',", row.GetCell(j).StringCellValue.Replace("'", "''"));
                        }
                        builder.Length = builder.Length - 1;
                        builder.Append(");");
                    }

                    if ((i % 50 == 0 || i == rowCount) && builder.Length > 0)
                    {
                        //每50条记录一次批量插入到数据库
                        //rowAffected += dbAction(builder.ToString());
                        builder.Length = 0;
                    }
                }


            }
            return rowAffected;
        }

        public static bool HasData(Stream excelFileStream)
        {
            using (excelFileStream)
            {
                HSSFWorkbook workbook = new HSSFWorkbook(excelFileStream);
                if (workbook.NumberOfSheets > 0)
                {
                    ISheet sheet = workbook.GetSheetAt(0);
                    return sheet.PhysicalNumberOfRows > 0;

                }
            }
            return false;
        }
        #endregion

        #endregion

    }
}
