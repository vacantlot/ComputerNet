using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using ClintNet;
namespace ComputerNet
{
    public partial class FrmSever : Form
    {
        TcpListener Server; //该类封了基础Socket，开始后可自动绑定并监听客户连接
        ArrayList Clients = new ArrayList(10);//用数组链表保存每一个客户的连接
        Thread thread; //该线程用以监并接受客户的连接请求
        bool stop = true; //作为thread线程代码中的一个循环条件，是否继             
        //续接受连接请求

        public FrmSever()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Listen()
        {
            try
            {                
                Server = new TcpListener(int.Parse(textBox3.Text));
                Server.Start(); //开始侦听
                while (!stop)
                {
                    Socket temp = Server.AcceptSocket(); //返回可以用以处理连接的Socket
                    //实例
                    ClientItem ci = new ClientItem(this, "", temp);//this代表本窗体对象，网名
                    //暂时为空，temp为同客户端连接的套接字，构造生成一个ClientItem类的实例
                    Clients.Add(ci);//将一个代表同客户端连接的实例加入到列表
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            stop = false;
            thread = new Thread(new ThreadStart(Listen));//创建线程
            thread.IsBackground = true;
            thread.Start(); //启动线程
            button2.Enabled = false; //当前按钮禁用
            button1.Enabled = true; //停止监听按钮可用

        }

        public void Receive(ClientItem ci, string msg)//接收消息
        {
            string[] temp = msg.Split(new char[] { '|' });//调用字符串的Split方法，分离出
            //用“|”分隔的多个字符串，保存到字符串数组temp中。

            switch (temp[0])
            {//根据不同的预定义的消息头，调用不同的自定义方法
                case "conn": login(ci, msg); break;//用login方法处理conn(上线)类型消息
                case "chat": chat(ci, msg); break; //用chat方法处理chat（聊天）类型消息
                case "priv": priv(ci, msg); break; //用priv方法处理priv（私聊）类型消息
                case "gone": gone(ci); break; //用gone方法处理gone（离开）类型消息
            }
        }

        private void login(ClientItem ci, string msg)//登录
        {
            string[] temp = msg.Split(new char[] { '|' });
            string ssend = "conn|" + temp[1];
            ClientItem e;
            for (int i = 0; i < Clients.Count; i++)
            {
                //遍历所有客户连接
                e = (ClientItem)(Clients[i]);
                if (e != ci) ssend += "|" + e.name;//如果e不是当前接收到conn连接信//号的实例
                else e.name = temp[1]; //如果e是当前接收到信号的实例，
                //则将用户名域填上
            }

            ci.Send(ssend);//向当前实例对应的客户连接端发送所有客户名信息
            textBox1.AppendText("用户:"+temp[1] + " 已登录"); //向文本框显示登录信息
            textBox1.AppendText("\r\n\r\n");
            foreach (ClientItem clientItem in Clients)
            {
                if (clientItem != ci)
                    clientItem.Send("conn|" + temp[1]); //向非当前发信号客户发送当前上线的用户名
            }
        }

        private void chat(ClientItem ci,string msg)
        {
            string[] temp = msg.Split(new char[]{'|'});
            textBox1.AppendText(temp[1]);
            textBox1.AppendText("\r\n\r\n");
            foreach(ClientItem e in Clients)
            {
                 e.Send("chat|"+temp[1]); //向所有客户发送聊天信息
            }
        }

        private void priv(ClientItem ci,string smg)
        {
            string[] temp = smg.Split(new char[] { '|' });
            foreach (ClientItem e in Clients)
                if (e.name == temp[1])
                    e.Send("priv|" + ci.name + "|" + temp[2]);
            textBox1.AppendText(temp[2]);
            textBox1.AppendText("\r\n\r\n");
        }

        private void gone(ClientItem ci)
        {
            foreach(ClientItem e in Clients)
            if(e!=ci) e.Send("gone|"+ci.name); //向非当前用户发当前用
            //户己下线
            textBox1.AppendText("BYB: "+ci.name);
            textBox1.AppendText("\r\n\r\n");
            ci.close(); //关闭同该客户的连接
            Clients.Remove(ci); //从链表中移除客户连接
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server.Stop(); //关闭监听套接字
            stop=true; //停止线程循环
            thread.Abort(); //终止线程
            foreach(ClientItem elem in Clients)
            {
            elem.Send("end"); //向所有客户端发止服务信号
            elem.close(); //关闭同所有客户端的套接字连接
            }
            Clients.Clear(); //清空客户连接链表
            button1.Enabled=false; 
            button2.Enabled=true; 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text.Trim(), "^\\d+$"))
            {
                textBox3.Text = "";
            }
            else if (textBox3.Text != String.Empty) 
            {
                button2.Enabled = true;
            }
            
        }
    }
}
