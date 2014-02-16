using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.Server
{
    public class BaseForm:Form
    {
        public BaseForm() {
            IPrincipal _principal = Thread.CurrentPrincipal;
            if (_principal.Identity.IsAuthenticated)
            {
                MessageBox.Show(_principal.Identity.Name);
                //MessageBox.Show(_principal.IsInRole("管理员").ToString());
            }
            else
            {
                MessageBox.Show("你还没有注册");
            }
        }
    }
}
