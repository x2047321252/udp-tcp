using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            int receive;
            byte[] data = new byte[1024];

            //得到本机IP
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 8080);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //绑定网络地址
            socket.Bind(ip);

            Console.WriteLine("This is a Server, host name is {0}", Dns.GetHostName());

            //等待客户机连接
            Console.WriteLine("正在连接客户机...");

            //得到客户机的IP
            IPEndPoint send = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(send);
            receive = socket.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Message received from {0}: ", Remote.ToString());
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, receive));

            //与客户机连接成功，返回连接成功信息
            string message = "你好！客户端";

            //字符串与字节数组相互转换
            data = Encoding.UTF8.GetBytes(message);

            //发送信息
            socket.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                //接收信息
                receive = socket.ReceiveFrom(data, ref Remote);
                Console.WriteLine(Encoding.UTF8.GetString(data, 0, receive));
            }
        }
    }
}