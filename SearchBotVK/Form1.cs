using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;

namespace SearchBotVK
{
    // Developer : Falwee
    //My first programm for VK
    public partial class authForm : Form
    {
        public authForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Auth auth = new Auth();
            if (auth.Authorize(textBox1.Text, textBox2.Text)) // auth in VK
            {
                Form2 ifrm = new Form2(); 
                ifrm.Show(); //open main form
                this.Hide(); // hide auth form
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Перезапустите приложение"); // some info for users
        }
    }
}
