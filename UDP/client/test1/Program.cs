using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];

            //构建TCP 服务器
            Console.WriteLine("This is a Client, host name is {0}", Dns.GetHostName());

            //设置服务IP（这个IP地址是服务器的IP），设置端口号
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.43.236"), 8080);

            //定义网络类型，数据连接类型和网络协议UDP
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //与服务器端连接成功，服务器端接收到消息
            string welcome = "你好! 服务器端";
            data = Encoding.UTF8.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ip);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            //对于不存在的IP地址，加入此行代码后，可以在指定时间内解除阻塞模式限制
            int recv = server.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Message received from {0}: ", Remote.ToString());
            Console.WriteLine(Encoding.UTF8.GetString(data, 0, recv));

            Console.WriteLine("按下任意按键开始发送...");
            Console.ReadKey();
            int i = 0;
            while (i<=50)
            {
                string s = "hello cqjtu!重交物联2019级" + i;
                Console.WriteLine(s);
                server.SendTo(Encoding.UTF8.GetBytes(s), Remote);
                i++;
            }
            Console.WriteLine("发送完毕！");
            server.Close();
            Console.WriteLine("按下任意按键退出发送...");
            Console.ReadKey();
        }

    }
}
