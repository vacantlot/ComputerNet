using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //程序中用到的网络输入/输出流类所在名字空间
using System.Net; //网络相关类名字空间
using System.Net.Sockets; //网络套接字名字空间
using System.Threading; //线程类的名字空间

namespace ClintNet
{
    public partial class FrmClient : Form
    {
        TcpClient client; //定义客户套接字，TcpClient类中封装了Socket类，简化使用
        bool loop = true; //loop定义为进程代码中的循环控制变量
        Thread thread; //定义接收数据进程
        string name; //保存登录用户名

        public FrmClient()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FrmLogin dlg = new FrmLogin();
            if (dlg.ShowDialog() == DialogResult.OK)//显示对话框dlg
            {
                try
                {                   
                    client = new TcpClient(dlg.ip,1234); //用对话框的IP和端口生成套接字
                }
                catch (SocketException ex)//try……catch……结构是捕获程序异常
                { 
                 MessageBox.Show("连接失败！\n"+ex.Message);
                }
                    name = dlg.name; //连接成功后保存用户名
                    send("conn|" + name); //向服务端发送conn（上线）信号
                    thread = new Thread(new ThreadStart(Run)); //创建接收信息的线程
                    thread.IsBackground = true;
                    thread.Start(); //线程工作，开始接收信号
                    this.button1.Enabled = true; //断开连接按钮可用
                    this.button2.Enabled = true; //发送按钮可用
                    this.button3.Enabled = false; //重新连接按钮禁用
                }            
            else
            { //如果不是单击连接按钮关闭对话框
                this.button1.Enabled = false; //断开连接按钮禁用
                this.button2.Enabled = false; //发送按钮禁用
                this.button3.Enabled = true; //重新连接按钮可用
            }
        }

        void Run()
        {
            try
            {
                string msg = string.Empty; //定义接收信息字符串（可不赋初始值）
                NetworkStream stream = client.GetStream(); //取得套接字网络数据流
                Byte[] data = new Byte[256]; //定义接收信息的字节数组
                while (loop)
                {
                    //当loop为真时循环
                    Int32 bytes = stream.Read(data, 0, data.Length); //等待读取数据
                    msg = System.Text.Encoding.Unicode.GetString(data, 0, bytes);//转换成字符串
                    process(msg); //调用自定义方法以处理接收到的信息
                }
                stream.Close(); //循环退出时关闭数据流
                client.Close(); //关闭套接字
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        void process(string msg)
        {
            string[] temp = msg.Split(new char[] { '|' });//分开用“|”分隔的信息
            switch (temp[0])
            {//temp[0]中为信息类型，根据具体类型调度不同的处理方法
                case "conn": conn(msg); break;
                case "chat": chat(temp[1]); break;
                case "priv": priv(msg); break;
                case "gone": gone(temp[1]); break;
                case "end": svr_end(); break;
            }
        }
        void conn(string msg)
        {
            string[] temp = msg.Split(new char[] { '|' });
            for (int i = 1; i < temp.Length; i++)
            {
                   comboBox1.Items.Add(temp[i]);//向组合框中添加用户名
            }
            textBox2.AppendText("用户:"+temp[1] + "已上线"); //编辑框添加新用户上线
            textBox2.AppendText("\r\n\r\n");
        }
        //在数组temp中，temp[0]为conn标识，其它元素为用户名，如果当前用户不是刚
        //刚上线用户，则数组中将只有两个元素，其中temp[1]为刚上线用户名

        void chat(string msg)
        {
            textBox2.AppendText(msg); //添加信息到公共频道显示框
            textBox2.AppendText("\r\n\r\n"); //空一行
        }
        void priv(string msg)
        {
            string[] temp = msg.Split(new char[] { '|' });
            int ind = comboBox1.FindString(temp[1]);//temp[1]中为信息来源用户
            if (ind != -1)
            {
                comboBox1.SelectedIndex = ind;//在组合框上选中信息来源用户
                textBox3.AppendText(temp[2]);//在私有频道显示框中添加信息
                textBox3.AppendText("\r\n\r\n");//空一行
            }
        }
        void gone(string name)
        {
            int ind = comboBox1.FindString(name); //在组合框中找到下线用户
            if (ind != -1)
            {//如果找到
                comboBox1.Items.RemoveAt(ind); //删除找到的用户名
                textBox2.AppendText("用户:"+name + "已下线");//显示用户己离开的信息
                textBox2.AppendText("\r\n\r\n"); //空一行
            }
        }
        void svr_end()
        {
            MessageBox.Show("聊天服务器关闭。");
            comboBox1.Items.Clear(); //清除组合框中所有用户
            textBox2.Text = "";
            textBox3.Text = "";
            button1.Enabled = !button1.Enabled; //发送按钮由可用变为禁用
            button2.Enabled = !button2.Enabled; //断开连接按钮由可用变为禁用
            button3.Enabled = !button3.Enabled; //重新连接按钮由禁用变为可用
            client.Close(); //关闭套接字
            loop = false; //终止线程中的循环语句
            thread.Abort(); //终止接收信息的thread线程
        }
        void send(string msg)
        {
            try
            {
                Byte[] buff = System.Text.Encoding.Unicode.GetBytes(msg);
                NetworkStream stream = client.GetStream();//取得套接字网络数据流
                stream.Write(buff, 0, buff.Length); //发送消息
                textBox1.Text = ""; //清空发送框
                textBox1.Focus(); //焦点重新定位到发送框
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            send("gone|" + name); //向服务端发送下线的信息
            client.Close(); //关闭套接字
            loop = false; //将线程中的循环条件置为假

            thread.Abort(); //终止线程
            comboBox1.Items.Clear(); //清空组合框
            textBox2.Text = "";
            textBox3.Text = ""; ;
            button1.Enabled = false; //发送按钮设为禁用
            button2.Enabled = false; //断开按钮设为禁用
            button3.Enabled = true; //重新连接按钮设为可用
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmLogin dlg = new FrmLogin();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    client = new TcpClient(dlg.ip, 1234);
                }
                catch (SocketException ex)
                {
                    MessageBox.Show("连接失败！\n"+ex.Message);
                }
                name = dlg.name;
                loop = true;
                thread = new Thread(new ThreadStart(Run));
                thread.IsBackground = true;
                thread.Start();
                send("conn|" + name);
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.button3.Enabled = false;
            }
            else
            {
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg;

            if (comboBox1.SelectedIndex != -1)
            {
                string name = (string)comboBox1.SelectedItem;
                if (radioButton1.Checked)
                    msg = "priv|" + name + "|用户: " + this.name + " 对 " + name + "说:" + textBox1.Text;
                else
                    msg = "chat|用户: " + this.name + " 对 " + name + " 说:" + textBox1.Text;
                send(msg);
            }

        }

        
    }
}
