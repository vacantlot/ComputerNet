using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClintNet;
namespace ComputerNet
{
    public partial class FrmMDI : Form
    {
        public FrmMDI()
        {
            InitializeComponent();
        }

        private void 通讯一ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frmUDP = new Form1();
            frmUDP.MdiParent = this;
            frmUDP.Show();
        }

        private void 服务器端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSever severTCP = new FrmSever();
            severTCP.MdiParent = this;
            severTCP.Show();
        }

        private void 客户端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClient clientTCP = new FrmClient();
            clientTCP.MdiParent = this;
            clientTCP.Show();
        }

        private void 使用说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDescribe describe = new FrmDescribe();
            describe.MdiParent = this;
            describe.Show();
        }

        private void FrmMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
            Application.Exit();
        }
    }
}
