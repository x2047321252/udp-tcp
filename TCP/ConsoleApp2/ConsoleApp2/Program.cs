using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            //连接准备
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 2000);
            //创建端口对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iPEndPoint);//绑定端口

            //循环监听并处理消息
            while (true)
            {
                i++;
                try
                {
                    Console.WriteLine("Perform operations {0} :", i);
                    Console.WriteLine("************************************");
                    socket.Listen(0);//开始监听
                    Console.WriteLine("等待连接...");

                    //实例化一个新的端口
                    Socket temp = socket.Accept();//为新建连接创建新的Socket
                    Console.WriteLine("连接成功！");

                    //接收客户端发来的消息
                    string recvStr = "";
                    byte[] recvBytes = new byte[1024];
                    int bytes;
                    bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//接收客户端信息
                    recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
                    Console.WriteLine("消息内容:{0}", recvStr);//显示客户端传来的信息

                    //返回给客户端连接成功的信息
                    string sendmessage = "成功发送消息，客户端！";
                    byte[] bs = Encoding.UTF8.GetBytes(sendmessage);
                    temp.Send(bs, bs.Length, 0);

                    //关闭端口
                    temp.Close();
                    Console.WriteLine("Completed...");
                    Console.WriteLine("-----------------------------------");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException: {0}", e);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
            }
        }
    }
}