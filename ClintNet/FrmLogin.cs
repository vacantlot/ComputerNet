using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClintNet
{
    public partial class FrmLogin : Form
    {
        public string ip; //运行时获取IP地址
        public string name; //运行时获取用户名

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.textBox1.Text == "") return;
            if (this.textBox2.Text == "") return;
            ip = textBox1.Text; //保存IP
            name = textBox2.Text; //保存用户名
            this.DialogResult = DialogResult.OK;//关闭本对话框，返回OK
        }

    }
}
