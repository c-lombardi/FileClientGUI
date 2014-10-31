using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void button1_Click(object sender, EventArgs e)//Send
        {

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
