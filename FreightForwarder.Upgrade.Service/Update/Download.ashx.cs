using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Configuration;
using FreightForwarder.Domain.Entities;

namespace FreightForwarder.Upgrade.Service {
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Download : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            //using (DataContext dc = new DataContext(
            //    WebConfigurationManager.ConnectionStrings["FFDB"].ConnectionString)) {

            //    UpgradePackage UpgradePackage = dc.GetTable<UpgradePackage>().Where(k => k.FileVersion == context.Request.QueryString["version"]).FirstOrDefault();
            //    if (UpgradePackage != default(UpgradePackage)) {
            //        context.Response.ContentType = "application/octet-stream";
            //        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + UpgradePackage.FileName);
            //        context.Response.BinaryWrite(UpgradePackage.FileBytes);
            //    }
            //}

        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}
