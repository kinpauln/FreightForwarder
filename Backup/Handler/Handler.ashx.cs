using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Cobainsoft.Windows.Forms;
using System.Data.Linq;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ICSharpCode.SharpZipLib.Zip;

namespace BigzoneBusinessCenterService.Handler
{
    public class Handler : IHttpHandler
    {

        private string Constr = ConfigurationManager.ConnectionStrings["TransPad"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request.QueryString["op"].Trim().ToLower();
            int OrderId = 0;
            string sRet = "";

            switch (op)
            {
                case "barcode":
                    OutPutBarCode(context);
                    break;
                case "write-cause":
                    string text = context.Request.Form["text"].Trim();
                    OrderId = Convert.ToInt32(context.Request.Form["OrderId"]);
                    SaveCause(OrderId, text, context);
                    break;
                case "check-cause":
                    OrderId = Convert.ToInt32(context.Request.Form["OrderId"]);
                    context.Response.ContentType = "text/plain";
                    sRet = CheckOrder(OrderId, context);
                    context.Response.Write(sRet);
                    break;
                case "getimg":
                    GetImage(context);
                    break;
                case "import-image":
                    context.Response.Write(UploadUpgrade(context));
                    break;
                case "pub-notice":
                    string content= context.Request.Form["content"];
                    context.Response.Write(PubNotice(context, content));
                    break;
                case "get-notice":
                    context.Response.Write(GetNotice());
                    break;
            }

        }

        //取公告信息
        private string GetNotice()
        {
            string sRet = string.Empty;
            using (DataContext dc = new DataContext(Constr))
            {
                var notes = from n in dc.GetTable<Info>()
                            select n;
                if (notes.ToList().Count > 0)
                {
                    sRet= notes.FirstOrDefault<Info>().Notice;
                }
            }
            return sRet;
        }

        //发布公告信息
        private string PubNotice(HttpContext context,string content)
        {
            string sRet = string.Empty;

            try
            {
                using (DataContext dc = new DataContext(Constr))
                {
                    var notes = from n in dc.GetTable<Info>()
                                select n;
                    if (notes.ToList().Count > 0)
                    {
                        //修改
                        notes.FirstOrDefault<Info>().Notice = content;
                        dc.SubmitChanges();
                    }
                    else
                    {
                        dc.GetTable<Info>().InsertOnSubmit(new Info()
                        {
                            Notice = content
                        });
                        dc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                sRet = string.Format("0:{0}", ex.Message);
            }

            return sRet;
        }

        //上传文件
        private string UploadUpgrade(HttpContext context)
        {
            string sRet = string.Empty;
            HttpPostedFile upFile = context.Request.Files["fp"];
            Int32 fileLength = upFile.ContentLength;
            if (fileLength == 0)
            {
                return "请选择要上传的文件！";
            }

            byte[] filebyte = new byte[fileLength];
            Stream streamObj = upFile.InputStream;

            sRet = UnzipAndSave(streamObj);
            return sRet;
        }

        //解压升级程序
        private string UnzipAndSave(Stream zipStream)
        {
            try
            {
                string sRet = "文件已经上传成功！";
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
                return sRet;
            }
            catch (Exception ex) {
                return string.Format("上传失败，请重试！(错误代码：{0})", ex.Message);
            }
        }

        private void GetImage(HttpContext context)
        {
            using (DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["TransPad"].ConnectionString))
            {
                var images = from i in dc.GetTable<Images>()
                             select i;
                List<Images> list = images.ToList();

                if (list.Count == 0)
                    return;

                Random rnd = new Random(list.Count);
                Images img = list[rnd.Next(0, list.Count - 1)];

                if (img != null)
                {
                    context.Response.ContentType = img.FileType.Trim();
                    context.Response.OutputStream.Write((byte[])img.Image, 0, (int)img.FileSize);
                    context.Response.End();
                }
            }
        }

        //检查业务是不是与档案不符或被申请人拒收
        private string CheckOrder(int OrderId, HttpContext context)
        {
            string sRet = string.Empty;
            using (DataContext dc =
                new DataContext(Constr))
            {
                var os = from o in dc.GetTable<Order>()
                         where o.Id == (int)OrderId
                         select o;

                List<Order> list = os.ToList();
                if (list.Count > 0)
                {
                    if (!list[0].FileCorrect || list[0].ApplyDenial)
                    {
                        return "show";
                    }
                }
            }
            return "hide";
        }

        //提交原因
        private void SaveCause(int OrderId, string Text,HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                using (DataContext dc = new DataContext(Constr))
                {
                    //提交原因
                    dc.GetTable<Cause>().InsertOnSubmit(new Cause()
                    {
                        OrderId = OrderId,
                        Memo = Text
                    });

                    //设置order完成标志为失败
                    var orders = from o in dc.GetTable<Order>()
                                 where o.Id == OrderId
                                 select o;
                    List<Order> list = orders.ToList();
                    if (list.Count > 0)
                    {
                        list[0].Failed = true;
                    }
                    dc.SubmitChanges();
                }
                context.Response.Write("1:填写成功！");
            }
            catch (Exception ex)
            {
                context.Response.Write(string.Format("0:{0}", ex.Message));
            }
        }

        //显示条形码
        private void OutPutBarCode(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "image/gif";
                string code = context.Request.QueryString["BarCode"].Trim();
                BarcodeControl bc = new BarcodeControl();
                bc.Data = code;
                bc.BarcodeType = BarcodeType.CODE39;
                bc.CopyRight = string.Empty;
                bc.ShowCode39StartStop = true;
                bc.StretchText = true;
                bc.Text = code;
                bc.TextPosition = BarcodeTextPosition.Below;
                bc.MakeImage(ImageFormat.Gif, 1, 80, true, false, null, context.Response.OutputStream);
            }
            catch { }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
