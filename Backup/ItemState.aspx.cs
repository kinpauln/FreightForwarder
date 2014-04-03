using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace BigzoneBusinessCenterService
{
    public partial class ItemState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["OrderId"] != null)
            {
                int iOrderId = Convert.ToInt32(Request.QueryString["OrderId"]);
                DataTable dt = new DataTable();

                using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TransPad"].ConnectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(string.Format("select * from [Log] where orderid={0}", iOrderId), con);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    //新业务登记
                    DataRow row = GetRow("LastState=2", dt);
                    if (row != null)
                        ltReg.Text = Convert.ToString(row["Entry"]);

                    //档案确认
                    row = GetRow("LastState=3", dt);
                    if (row != null)
                        ltFilerConfirm.Text = Convert.ToString(row["Entry"]);

                    //档案邮寄
                    row = GetRow("LastState=4", dt);
                    if (row != null)
                        ltFilerPost.Text = Convert.ToString(row["Entry"]);

                    //制证收到材料
                    row = GetRow("LastState=5", dt);
                    if (row != null)
                        ltMakerAcceptFiles.Text = Convert.ToString(row["Entry"]);

                    //制证完成
                    row = GetRow("LastState=6", dt);
                    if (row != null)
                        ltMakerDone.Text = Convert.ToString(row["Entry"]);

                    //制证已经邮寄
                    row = GetRow("LastState=7", dt);
                    if (row != null)
                        ltMakerPost.Text = Convert.ToString(row["Entry"]);

                    //申请人已经签收
                    row = GetRow("LastState=8", dt);
                    if (row != null)
                        ltApplyConfirm.Text = Convert.ToString(row["Entry"]);
                }
            }
        }

        private DataRow GetRow(string where,DataTable dt)
        {
            DataRow[] rows = dt.Select(where);
            if (rows.Length > 0)
            {
                return rows[0];
            }
            else
            {
                return null;
            }
        }
    }
}
