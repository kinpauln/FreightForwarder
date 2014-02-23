using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
