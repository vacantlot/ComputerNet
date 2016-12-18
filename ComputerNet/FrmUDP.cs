using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
namespace ComputerNet
{
    public partial class Form1 : Form
    {
        private delegate  void setText(string str1 , string str2); //定义一个委托用于跨线程调用控件
        Socket udp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //构造函数的三个参数的意义：
        //第一个参数代表地址族为InterNetwork
        //第二个参数代表套接字类型为Dgram
        //第三个参数代表套接字所用协议为Udp
        Thread th; //定义一个线程用接收到达的信息
        
        public Form1()
        {
            InitializeComponent();
        }
        
        
        public void f()
        {
            try
            {

            
            IPHostEntry ipinfo = Dns.Resolve(Dns.GetHostName());
            //Dns的方法GetHostName()获取本地主机名
            //Resolve()方法由主机名或IP地址得到主机IP列表(IPHostEntry)对象ipinfo
            IPAddress addr = ipinfo.AddressList[0];
            //由ipinfo中取得第一个IP地址（IPAddress）对象
            IPEndPoint endp = new IPEndPoint(addr,11000);
            //由IP地址和端口号生成本地通信终端结点对象
            udp.Bind(endp);
            //将本终端结点对象同Socket对象绑定起来
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);//定义任意远程端点
            Byte[] msg = new Byte[256]; //定义用以接收数据的字节数组
            while(true)
            {
            EndPoint senderRemote = (EndPoint)sender;
            int c = udp.ReceiveFrom(msg,ref senderRemote);
            string str1 = Encoding.Unicode.GetString(msg,0,c)+"\r\n";
            string str2 = ((IPEndPoint)senderRemote).Address.ToString();
            setText st = new setText(settext);
            this.Invoke(st, str1, str2);                                      
            }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        void settext(string str1,string str2)
        {
            textBox1.Text+=str1;
            textBox2.Text=str2;           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            th = new Thread(new ThreadStart(f));
            //利用方法f创建接收进程
            th.Start();
            //进程开始工作，等待接收信息

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim()!="")
        {
            IPHostEntry ipinfo = Dns.Resolve(textBox3.Text);
            IPEndPoint endp = new IPEndPoint(ipinfo.AddressList[0], 11000);
            EndPoint remote = (EndPoint)endp;
            Socket s = new Socket(endp.Address.AddressFamily,
            SocketType.Dgram,
            ProtocolType.Udp);
            //创建无连接套接字，用它发送数据
            Byte[] date = Encoding.Unicode.GetBytes(textBox4.Text);
            s.SendTo(date, remote); //向远程端点发送信息
            s.Close(); //关闭发送套接字
            textBox4.Text=""; //清除发送输入框
            textBox4.Focus(); //重先定位输入焦点
        }

        }

    }
}
