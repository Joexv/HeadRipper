using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeadRipper
{
    public partial class Cookie : Form
    {

        public Form1 main { get; set; }
        public Cookie()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            main.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.Enabled = true;
        }

        string BearerID;
        private void button1_Click(object sender, EventArgs e)
        {
            if (CookieMonster.GetCookie_Chrome("headspace", "hsngjwt", ref BearerID) && !String.IsNullOrEmpty(BearerID))
            {
                textBox1.Text = BearerID;
                Console.WriteLine(BearerID);
            }
            else
            {
                MessageBox.Show("Failed. Make sure that you are signed into Headspace. If it still wont work try FireFox.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CookieMonster.GetCookie_FireFox("headspace", "hsngjwt", ref BearerID) && !String.IsNullOrEmpty(BearerID))
            {
                textBox1.Text = BearerID;
                Console.WriteLine(BearerID);
            }
            else
            {
                MessageBox.Show("Failed. Make sure that you are signed into Headspace. If it still wont work try Chrome.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
