using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreightForwarder.UI.Winform
{
    public partial class FrmUnStateProgressBar : Form
    {
        public string DisplayInfo = "正在执行，请耐心等待。。。。";
        public bool ShowCancel = true;
        private int _mitute;
        private int _second;
        private int _msencond;

        public FrmUnStateProgressBar()
        {
            InitializeComponent();
        }

        private void FrmUnStateProgressBar_Load(object sender, EventArgs e)
        {
            label1.Text = DisplayInfo;

            btnCancel.Visible = ShowCancel;

            _mitute = 0;
            _second = 0;
            _msencond = 0;

            timer1.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FrmUnStateProgressBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _msencond += 10;
            if (_msencond == 1000)
            {
                _second += 1;
                _msencond = 0;
            }
            if (_second == 60)
            {
                _mitute += 1;
                _second = 0;
            }
            lblTimer.Text = (_mitute > 0 ? _mitute + "分:" : string.Empty) + (_second + "秒:") + (_msencond > 0 ? _msencond.ToString() : string.Empty);
        }
    }
}
