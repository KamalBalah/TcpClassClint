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
namespace TcpClassClint
{
    public partial class Clint : Form
    {
        TcpClient clint;
        public Clint()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clint = new TcpClient();
            clint.Connect(new IPEndPoint(IPAddress.Loopback, 3090));
            new System.Threading.Thread(
                () =>
                {
                        while (clint.GetStream().DataAvailable == false) ;

                    //while (true)
                    //{

                    //}
                    byte[] data = new byte[clint.Available];
                    clint.GetStream().Read(data, 0, data.Length);
                    string d = Encoding.Unicode.GetString(data);
                    listBox1.Items.Add(d.Trim());
                    UdpClient udp = new UdpClient();
                    data = Encoding.Unicode.GetBytes(d.Trim().Length.ToString());
                    udp.Send(data,data.Length, 
                        new IPEndPoint(IPAddress.Loopback, 3050));
                }
                ).Start();
        }
    }
}
