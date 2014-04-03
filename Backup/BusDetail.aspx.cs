using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BigzoneBusinessCenterService
{
    public partial class BusDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].Trim().ToLower();
                string orderid = Request.QueryString["orderid"].Trim();
                switch (type)
                { 
                    case "drive":
                        ssData.SelectCommand = string.Format("select * from v_driveinfo where orderid = {0}", orderid);
                        break;
                    case "card":
                        ssData.SelectCommand = string.Format("select * from v_cardinfo where orderid = {0}", orderid);
                        break;
                    case "steer":
                        ssData.SelectCommand = string.Format("select * from v_steerinfo where orderid = {0}", orderid);
                        break;
                }
                rep.DataBind();
            }
        }
    }
}
