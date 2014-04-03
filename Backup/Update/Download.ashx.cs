using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Linq;
using System.Web.Configuration;

namespace BigzoneBusinessCenterService {
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Download : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            using (DataContext dc = new DataContext(
                WebConfigurationManager.ConnectionStrings["TransPad"].ConnectionString)) {

                UpdateFile updateFile = dc.GetTable<UpdateFile>().Where(k => k.FileVersion == context.Request.QueryString["version"]).FirstOrDefault();
                if (updateFile != default(UpdateFile)) {
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + updateFile.FileName);
                    context.Response.BinaryWrite(updateFile.FileBytes);
                }
            }

        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}
