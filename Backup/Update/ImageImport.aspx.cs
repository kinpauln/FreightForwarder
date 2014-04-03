using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;
using System.Data.Linq;


namespace BigzoneBusinessCenterService
{
    public partial class ImageImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string Constr = ConfigurationManager.ConnectionStrings["TransPad"].ConnectionString;
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //上传
            /*HttpPostedFile upFile = fp.PostedFile;
            Int32 fileLength = upFile.ContentLength;

            try
            {
                if (fileLength == 0)
                {
                    Response.Write("<script>alert('请选择要上传的文件！')</script>;");
                    return;
                }

                byte[] filebyte = new byte[fileLength];
                Stream streamObj = upFile.InputStream;

                UnzipAndSave(streamObj);
            }
            catch { }*/
        }

        //解压升级程序
        private void UnzipAndSave(Stream zipStream)
        {

            try
            {
                ZipInputStream stream = new ZipInputStream(zipStream);
                ZipEntry entry = null;

                while ((entry = stream.GetNextEntry()) != null)
                {
                    string filename = Path.GetFileName(entry.Name).Trim();
                    string barcode = Path.GetFileNameWithoutExtension(entry.Name).Split(new char[] { '-' })[0];
                    using (MemoryStream mem = new MemoryStream())
                    {
                        int size = (int)entry.Size;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = stream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                mem.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        string type = "image/jpeg";
                        switch (Path.GetExtension(entry.Name))
                        {
                            case ".jpg":
                                type = "image/jpeg";
                                break;
                            case ".png":
                                type = "image/x-png";
                                break;
                            case ".gif":
                                type = "image/gif";
                                break;
                        }

                        using (DataContext dc = new DataContext(Constr))
                        {
                            //上传照片
                            dc.GetTable<Images>().InsertOnSubmit(new Images()
                            {
                                Image = data,
                                FileName = filename,
                                FileSize = data.Length,
                                FileType = type,
                                BarCode = barcode
                            });

                            //更新照片状态
                            var orders = from o in dc.GetTable<Order>()
                                         where o.BarCode.Trim().ToLower() == barcode.Trim().ToLower()
                                         select o;
                            Order order = orders.FirstOrDefault<Order>();
                            if (order != null && !order.ImageExists)
                            {
                                order.ImageExists = true;
                                order.ImageReload = true;
                            }

                            dc.SubmitChanges();
                        }
                    }
                }
                stream.Close();
                Response.Redirect("ImageImport.aspx");
            }
            catch (Exception ex) { }
        }


    }
}
