using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace ComputerNet
{
   public class ClientItem
    {
        public string name; //连接客户的用户名
        public Socket client; //同客户方建立连接的套接字
        public Thread thread; //接收信息的线程
        public FrmSever fk;      //引用主窗体
        delegate void f(ClientItem ci,string msg); //定义委托类型，其实例将用以
        //封装主窗体的void Receive (ClientItem ci,string msg)方法。       
        public ClientItem(FrmSever fk,string name,Socket client)
        {
            this.fk = fk; //创建时保存主窗体引用
            this.name=name; //对应客户名
            this.client=client; //保存同客户端连接的套接字引用
            thread = new Thread(new ThreadStart(Run));//创建接收信息的线程
            thread.Start(); //线程开始工作
        }

        void Run()
        {
            try
            {
                while (true)
                {
                    Byte[] buff = new Byte[256];
                    string str = "";
                    int i;
                    i = client.Receive(buff);
                    str += System.Text.Encoding.Unicode.GetString(buff, 0, i);
                    f fhandle = new f(fk.Receive);//利用委托f的实例封装主窗体方法Receive
                    fk.Invoke(fhandle, new object[] { this, str });//调用主窗体的Receive
                    //以处理接收到的数据。
                }
            }
            catch { }
        }

        public void Send(string smg)
        {
            Byte[] bytes=Encoding.Unicode.GetBytes(smg);
            client.Send(bytes);
        } 

        public void close()
        {
            client.Close();
            thread.Abort();
        }

    }
}
