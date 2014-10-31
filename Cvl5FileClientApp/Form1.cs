using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cvl5FileClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Send
        {
            var sendBuf = new char[100];
            if (AddBtn.Checked)
            {
                sendBuf[0] = '1';
            }
            else if (DeleteBtn.Checked)
            {
                sendBuf[0] = '2';
            }
            else if (EditBtn.Checked)
            {
                sendBuf[0] = '3';
            }
            else if (GetBtn.Checked)
            {
                sendBuf[0] = '4';
            }
            var position = 0;
            foreach (var c in KeyTextBox.Text.ToCharArray())
            {
                position++;
                sendBuf[position] = c;
            }
            position ++;
            sendBuf[position] = (char)0;
            foreach (var c in ValueTextBox.Text.ToCharArray())
            {
                position++;
                sendBuf[position] += c;
            }
            position ++;
            sendBuf[position] = (char)0;
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
            
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
    }
}
