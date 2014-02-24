using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Server
{
    public class UserUtils
    {
        #region Dialog

        //显示错误
        public static DialogResult ShowError(string text)
        {
            return MessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //显示提示消息
        public static DialogResult ShowInfo(string text)
        {
            return MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //显示警告
        public static DialogResult ShowWarning(string text)
        {
            return MessageBox.Show(text, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //显示询问
        public static DialogResult ShowConfirm(string text)
        {
            return MessageBox.Show(text, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        #endregion

        /// <summary>
        /// 发邮件
        /// </summary>
        /// <param name="userEmail">发件人邮箱</param>
        /// <param name="userPswd">邮箱帐号密码</param>
        /// <param name="toEmail">收件人邮箱</param>
        /// <param name="mailServer">邮件服务器</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="mailBody">邮件内容</param>
        /// <param name="attachFiles">邮件附件</param>
        private static void SendEmail(
    string userEmail,  
    string userPswd,   
    string toEmail,    
    string mailServer, 
    string subject,    
    string mailBody,   
    string[] attachFiles 
    )
        {
            //邮箱帐号的登录名
            string username = userEmail.Substring(0, userEmail.IndexOf('@'));
            //邮件发送者
            MailAddress from = new MailAddress(userEmail);
            //邮件接收者
            MailAddress to = new MailAddress(toEmail);
            MailMessage mailobj = new MailMessage(from, to);
            // 添加发送和抄送
            // mailobj.To.Add("");
            // mailobj.CC.Add("");

            //邮件标题
            mailobj.Subject = subject;
            //邮件内容
            mailobj.Body = mailBody;
            if (attachFiles != null)
            {
                foreach (string attach in attachFiles)
                {
                    mailobj.Attachments.Add(new Attachment(attach));
                }
            }
            //邮件不是html格式
            mailobj.IsBodyHtml = false;
            //邮件编码格式
            mailobj.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //邮件优先级
            mailobj.Priority = MailPriority.High;

            //Initializes a new instance of the System.Net.Mail.SmtpClient class 
            //that sends e-mail by using the specified SMTP server.
            SmtpClient smtp = new SmtpClient(mailServer);
            //或者用：
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = mailServer;

            //不使用默认凭据访问服务器
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(username, userPswd);
            //使用network发送到smtp服务器
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                //开始发送邮件
                smtp.Send(mailobj);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
            }
        }

        public static void SendEmail(string subject, string emailbody) {
            using (MailMessage message = new MailMessage())
            {
                try
                {
                    string emailTo = FreightForwarder.Server.Properties.Settings.Default.ServiceEmail;
                    message.To.Add(emailTo); //收件人邮箱
                    message.Subject = subject;//邮件主题
                    message.Body = emailbody;  //邮件正文

                    //SendEmail("chimengliang@126.com", "cml7123456", emailTo, "smtp.126.com", subject, emailbody, null);
                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Send(message);
                }
                catch (Exception ex)
                {
                    Logger.Warn("发错误日志邮件失败", ex);
                }
            }
        }
    }
}
