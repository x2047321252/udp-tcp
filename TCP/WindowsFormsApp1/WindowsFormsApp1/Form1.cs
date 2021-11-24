using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str;
                //做连接准备,ip地址为服务器ip地址
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.43.236"), 8080);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //开始连接
                str = "连接服务器中...";
                textBox1.AppendText(str + Environment.NewLine);
                socket.Connect(iPEndPoint);//连接到服务器

                //发送消息
                string sendmessage = textBox2.Text;
                str = "消息内容: " + sendmessage;
                textBox1.AppendText(str + Environment.NewLine);
                byte[] bs = Encoding.UTF8.GetBytes(sendmessage);
                str = "正在发送给服务器...";
                textBox1.AppendText(str + Environment.NewLine);
                socket.Send(bs, bs.Length, 0);//发送信息

                //接收服务器端反馈信息
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = socket.Receive(recvBytes, recvBytes.Length, 0);//接收服务器端返回的信息
                recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
                str = "服务器返回信息：" + recvStr;
                textBox1.AppendText(str + Environment.NewLine);

                //关闭socket
                socket.Close();
            }
            //异常处理
            catch (ArgumentNullException f)
            {
                string str = "ArgumentNullException: " + f.ToString();
                textBox1.AppendText(str + Environment.NewLine);
            }
            catch (SocketException f)
            {
                string str = "ArgumentNullException: " + f.ToString();
                textBox1.AppendText(str + Environment.NewLine);
            }
            textBox1.AppendText("" + Environment.NewLine);
            textBox2.Text = "";
        }
    }
}
