using FreightForwarder.Common;
using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Business
{
    public static class Extentions
    {
        public static IList<RouteInformationItem> ToRoutItemList(this DataTable dt, Dictionary<string, int> dicCompanies)
        {
            IList<RouteInformationItem> rlist = new List<RouteInformationItem>();
            foreach (DataRow row in dt.Rows)
            {
                RouteInformationItem entity = new RouteInformationItem()
                {
                    ShipName = row["船名"].ToString(),
                    StartPort = row["起运港"].ToString(),
                    DestinationPort = row["目的港"].ToString(),
                    StartDay = row["船期"].ToString(),

                    SailDayLength = row["航程"].ToString(),
                    Remarks = row["备注"].ToString()
                };

                int keyId = 0;
                int.TryParse(row["序号"].ToString().Trim(), out keyId);
                entity.Id = keyId;

                float _20GP = 0;
                float _40GP = 0;
                float _40HQ = 0;
                float.TryParse(row["20GP"].ToString(), out _20GP);
                float.TryParse(row["40GP"].ToString(), out _40GP);
                float.TryParse(row["40HQ"].ToString(), out _40HQ);
                entity.Price_20GP = _20GP;
                entity.Price_40GP = _40GP;
                entity.Price_40HQ = _40HQ;

                string nonstopString = row["是否直达"].ToString();
                if (nonstopString.Trim().Equals("是"))
                {
                    entity.Nonstop = (int)SailNonstopValues.Yes;
                }
                else if (nonstopString.Trim().Equals("否"))
                {
                    entity.Nonstop = (int)SailNonstopValues.No;
                }
                else
                {
                    entity.Nonstop = (int)SailNonstopValues.Unknow;
                }

                string isSingleContainerString = row["整柜/拼箱"].ToString();
                if (isSingleContainerString.Trim().Equals("整柜"))
                {
                    entity.IsSingleContainer = (int)IsSingleContainerValues.Yes;
                }
                else if (isSingleContainerString.Trim().Equals("拼箱"))
                {
                    entity.IsSingleContainer = (int)IsSingleContainerValues.No;
                }
                else
                {
                    entity.IsSingleContainer = (int)IsSingleContainerValues.Unknow;
                }

                DateTime validDate = DateTime.MinValue.Date;
                DateTime.TryParse(row["有效期"].ToString(), out validDate);
                entity.ValidDate = validDate;

                int companyId = 0;
                string companyCode = row["货代公司代号"].ToString();
                if (dicCompanies.Keys.Contains(companyCode))
                {
                    companyId = dicCompanies[companyCode];
                }
                entity.CompanyId = companyId;

                rlist.Add(entity);
            }

            return rlist;
        }

        public static DataTable ToDataTable(this IList<RouteInformationItem> rlist)
        {
            DataTable dt = new DataTable("导出列表");
            dt.Columns.Add("船名",typeof(string));
            dt.Columns.Add("起运港",typeof(string));
            dt.Columns.Add("目的港",typeof(string));
            dt.Columns.Add("船期",typeof(string));
            dt.Columns.Add("20GP",typeof(string));
            dt.Columns.Add("40GP",typeof(string));
            dt.Columns.Add("40HQ",typeof(string));
            dt.Columns.Add("是否直达",typeof(string));
            dt.Columns.Add("整柜/拼箱",typeof(string));
            dt.Columns.Add("航程",typeof(string));
            dt.Columns.Add("有效期",typeof(string));
            dt.Columns.Add("备注",typeof(string));
            dt.Columns.Add("货代公司代号",typeof(string));
            dt.Columns.Add("序号",typeof(string));
            
            foreach(RouteInformationItem item in rlist){
                DataRow row  = dt.NewRow();
                row["船名"] = item.ShipName;
                row["起运港"] = item.StartPort;
                row["目的港"] = item.DestinationPort;
                row["船期"] = item.StartDay;
                row["20GP"] = item.Price_20GP;
                row["40GP"] = item.Price_40GP;
                row["40HQ"] = item.Price_40HQ;

                string nostopString = string.Empty;
                switch (item.Nonstop) {
                    case (int)SailNonstopValues.No:
                        nostopString = (SailNonstopValues.No).GetDescription();
                        break;
                    case (int)SailNonstopValues.Yes:
                        nostopString = (SailNonstopValues.Yes).GetDescription();
                        break;
                    default:
                        nostopString = string.Empty;
                        break;
                }
                row["是否直达"] = nostopString;

                string isSingleContainerString = string.Empty;
                switch (item.IsSingleContainer)
                {
                    case (int)IsSingleContainerValues.No:
                        isSingleContainerString = (IsSingleContainerValues.No).GetDescription();
                        break;
                    case (int)IsSingleContainerValues.Yes:
                        isSingleContainerString = (IsSingleContainerValues.Yes).GetDescription();
                        break;
                    case (int)IsSingleContainerValues.Unknow:
                    default:
                        isSingleContainerString = string.Empty;
                        break;
                }
                row["整柜/拼箱"] = isSingleContainerString;

                row["航程"] = item.SailDayLength;
                row["有效期"] = item.ValidDate.ToString("yyyy-MM-dd");
                row["备注"] = item.Remarks;
                row["货代公司代号"] = item.Company.Code;
                if(item.Id!=0){
                row["序号"] = item.Id;}

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
