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
        public int Ports;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (this.textBox1.Text == "") return;
            if (this.textBox2.Text == "") return;
            if (this.textBox3.Text == "") return;
            ip = textBox1.Text; //保存IP
            name = textBox2.Text; //保存用户名
            Ports = int.Parse(textBox3.Text);
            this.DialogResult = DialogResult.OK;//关闭本对话框，返回OK
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text.Trim(), "^\\d+$"))
            {
                textBox3.Text = "";
            }
        }
    }
}
