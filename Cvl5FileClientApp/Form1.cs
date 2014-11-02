using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cvl5FileClientApp
{
    public partial class Form1 : Form
    {
        string ipAddress;
        short PortNumber;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Send
        {
            button1.Enabled = false;
            IPAddressTextBox.Enabled = false;
            PortNumberTextBox.Enabled = false;
            var length = 3;
            var recvData = new byte[100];
            byte[] sendBuf;
            if (AddBtn.Checked)
            {
                length = length + KeyTextBox.Text.Length + ValueTextBox.Text.Length;
                sendBuf = new byte[length];
                sendBuf[0] = (byte)1;
            }
            else if (DeleteBtn.Checked)
            {
                length = length + KeyTextBox.Text.Length;
                sendBuf = new byte[length];
                sendBuf[0] = (byte)2;
            }
            else if (EditBtn.Checked)
            {
                length = length + KeyTextBox.Text.Length + ValueTextBox.Text.Length;
                sendBuf = new byte[length];
                sendBuf[0] = (byte)3;
            }
            else
            {
                length = length + KeyTextBox.Text.Length;
                sendBuf = new byte[length];
                sendBuf[0] = (byte)4;
            }
            var position = 0;
            foreach (var c in KeyTextBox.Text.ToCharArray())
            {
                position++;
                sendBuf[position] = (byte)c;
            }
            position ++;
            sendBuf[position] = (byte)0;
            foreach (var c in ValueTextBox.Text.ToCharArray())
            {
                position++;
                sendBuf[position] += (byte)c;
            }
            position ++;
            sendBuf[position] = (byte)0;

            int val = 29999 ;
            val = IPAddress.HostToNetworkOrder( val );
    
            byte[] bytes = BitConverter.GetBytes( val );

            var ep = new IPEndPoint(IPAddress.Parse(ipAddress), PortNumber);
            
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 2000);

            try
            {
                socket.SendTo(sendBuf, sendBuf.Length, SocketFlags.None, ep);
                EndPoint tmpRemote = new IPEndPoint(IPAddress.Parse(ipAddress), PortNumber);
                socket.ReceiveFrom(recvData, SocketFlags.None, ref tmpRemote);
            }
            catch (SocketException ex)
            {

                ResponseTextBox.Text = "No data received, trying again, again...";
                try
                {
                    socket.SendTo(sendBuf, sendBuf.Length, SocketFlags.None, ep);
                    EndPoint tmpRemote = new IPEndPoint(IPAddress.Parse(ipAddress), PortNumber);
                    socket.ReceiveFrom(recvData, SocketFlags.None, ref tmpRemote);
                }
                catch (SocketException ex1)
                {
                    ResponseTextBox.Text = "No data received, trying again...";
                    try
                    {
                        socket.SendTo(sendBuf, sendBuf.Length, SocketFlags.None, ep);
                        EndPoint tmpRemote = new IPEndPoint(IPAddress.Parse(ipAddress), PortNumber);
                        socket.ReceiveFrom(recvData, SocketFlags.None, ref tmpRemote);
                    }
                    catch (SocketException ex2)
                    {
                        ResponseTextBox.Text = "No data received, I give up.";
                    }
                }
            }
            ResponseTextBox.Text = System.Text.Encoding.Default.GetString(recvData);
            button1.Enabled = true;
            IPAddressTextBox.Enabled = true;
            PortNumberTextBox.Enabled = true;
        }

        private void AddBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValueTextBox.Enabled = true;
        }

        private void DeleteBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValueTextBox.Enabled = false;
            ValueTextBox.Text = "";
        }

        private void EditBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValueTextBox.Enabled = true;
        }

        private void GetBtn_CheckedChanged(object sender, EventArgs e)
        {
            ValueTextBox.Enabled = false;
            ValueTextBox.Text = "";
        }

        private void IPAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            ipAddress = IPAddressTextBox.Text;
        }

        private void PortNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                PortNumber = (short)Int16.Parse(PortNumberTextBox.Text);
                ResponseTextBox.Text = "";
            }
            catch(Exception ex)
            {
                ResponseTextBox.Text = "Error: Please enter a valid port number";
            }
        }
    }
}
