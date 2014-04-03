using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Configuration;
using FreightForwarder.Domain.Entities;

namespace FreightForwarder.Upgrade.Service
{
    public partial class PubUpgrade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            HttpPostedFile upFile = fu.PostedFile;
            Int32 fileLength = upFile.ContentLength;

            try
            {
                if (fileLength == 0)
                {
                    Response.Write("<script>alert('请选升级包文件！')</script>");
                    return;
                }

                byte[] filebyte = new byte[fileLength];
                Stream streamObj = upFile.InputStream;
                streamObj.Read(filebyte, 0, fileLength);

                using (DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["FFDB"].ConnectionString))
                {
                    dc.GetTable<UpgradePackage>().InsertOnSubmit(new UpgradePackage()
                    {
                        FileBytes = filebyte,
                        FileName = Path.GetFileName(upFile.FileName),
                        PostTime = DateTime.Now,
                        FileVersion = Path.GetFileNameWithoutExtension(upFile.FileName)
                    });
                    dc.SubmitChanges();
                }
                Response.Write("<script>alert('升级包发布成功！')</script>");
                Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex) { Response.Write(string.Format("<script>alert('{0}')</script>", ex.Message)); }
        }
    }
}
